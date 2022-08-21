// See https://aka.ms/new-console-template for more information
using RecipeGenerator.Abstractions;
using RecipeGenerator.Services;
var markdownLocation = "/Markdown";
var htmlOutput = "/HTML";
var outputLocation = "/PDF";
Thread.Sleep(30000);// lazy wait for webapp to start. 
// SHould throw together a healthcheck
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
var printer = new PlaywrightPrinter("http://webapp:3000");

foreach(var recipe in recipesToConvert){
    var bytes = await printer.print(recipe);
    File.WriteAllBytes($"{outputLocation}/{recipe.Name}.pdf",bytes);
}