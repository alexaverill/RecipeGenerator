// See https://aka.ms/new-console-template for more information
using RecipeGenerator.Abstractions;
using RecipeGenerator.Services;
Console.WriteLine("Hello, World!");
var files = Directory.GetFiles("./TestMarkdown"); //TODO swap to arguement 
var recipesToConvert = new List<RecipePage>();
var converter = new MarkdownService();
foreach(var file in files){
    Console.WriteLine(file);
    var fileContent = await File.ReadAllTextAsync(file);
    recipesToConvert.Add(new RecipePage(){HTMLContent =converter.Convert(fileContent)});
}
int x =0;
foreach(var recipe in recipesToConvert){
    File.WriteAllText($"TestMarkdown/HTML/{x}.html",recipe.HTMLContent);
    x++;
}
var printer = new PlaywrightPrinter("http://localhost:3000");
int y = 0;
foreach(var recipe in recipesToConvert){
    var bytes = await printer.print(recipe);
    File.WriteAllBytes($"TestMarkdown/PDF/{y}.pdf",bytes);
    y++;
}