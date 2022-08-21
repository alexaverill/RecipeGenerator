// See https://aka.ms/new-console-template for more information
using RecipeGenerator.Abstractions;
using RecipeGenerator.Services;
using System.Net.Http;
var markdownLocation = "/Markdown";
var htmlOutput = "/HTML";
var outputLocation = "/PDF";
var styles = "/Styles";
var endpoint = "http://webapp:3000";
//wait for web-app to be ready
var client = new HttpClient();
bool isConnected = false;
var retryCount = 0;
while(!isConnected){
    try{
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        isConnected = response.StatusCode == System.Net.HttpStatusCode.OK;
    }catch(Exception e){
        Console.WriteLine($"Unable to connect to {endpoint}");
        Thread.Sleep(500);
        retryCount++;
        if(retryCount>10){
            Console.WriteLine("Unable to connect to web-app. Terminating");
            return;
        }
    }
}
Directory.CreateDirectory(markdownLocation);
Directory.CreateDirectory(htmlOutput);
Directory.CreateDirectory(outputLocation);
var files = Directory.GetFiles(markdownLocation);
var recipesToConvert = new List<RecipePage>();
var converter = new MarkdownService();
foreach(var file in files){
    Console.WriteLine(file);
    var fileContent = await File.ReadAllTextAsync(file);
    var fileInfo = new FileInfo(file);
    var name = Path.GetFileNameWithoutExtension(fileInfo.Name);
    recipesToConvert.Add(new RecipePage(){HTMLContent =converter.Convert(fileContent),Name=name});
}
foreach(var recipe in recipesToConvert){
    File.WriteAllText($"{htmlOutput}/{recipe.Name}.html",recipe.HTMLContent);
}
var printer = new PlaywrightPrinter(endpoint);
var stylesheet = await File.ReadAllTextAsync(Path.Combine(styles,"index.css"));
foreach(var recipe in recipesToConvert){

    var bytes = await printer.print(recipe,stylesheet);
    File.WriteAllBytes($"{outputLocation}/{recipe.Name}.pdf",bytes);
}