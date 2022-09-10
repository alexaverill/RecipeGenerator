using RecipeGenerator.Abstractions;
using Microsoft.Playwright;
using System.Web;
namespace RecipeGenerator.Services{
    public class PlaywrightPrinter:IPrinter{
        string Endpoint{get;set;}
        public PlaywrightPrinter(string _endpoint){
            Endpoint = _endpoint;
        }
        public async Task<byte[]> print(RecipePage recipe, string stylesheet){
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync(Endpoint);
            await page.AddStyleTagAsync(new PageAddStyleTagOptions(){Content=stylesheet});
            var content = HttpUtility.JavaScriptStringEncode(recipe.HTMLContent);
            var loadScript = $"window.dispatchEvent(new CustomEvent('loadRecipe', {{ detail:{{ data:\"{content}\" }} }}));";
            await page.EvaluateAsync(loadScript);
            await page.EvaluateAsync("document.body.classList.contains(\"ready\")");
            return await page.PdfAsync(new PagePdfOptions(){PreferCSSPageSize=true,Width="5.5in",Height="8.5in",PrintBackground=true});
        }
    }
}