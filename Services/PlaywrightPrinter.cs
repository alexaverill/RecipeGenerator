using RecipeGenerator.Abstractions;
using Microsoft.Playwright;
using System.Web;
namespace RecipeGenerator.Services{
    public class PlaywrightPrinter:IPrinter{
        string Endpoint{get;set;}
        public PlaywrightPrinter(string _endpoint){
            Endpoint = _endpoint;
        }
        public async Task<byte[]> print(RecipePage recipe){
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();
            await page.GotoAsync(Endpoint);
            var content = HttpUtility.JavaScriptStringEncode(recipe.HTMLContent);
            var loadScript = $"window.dispatchEvent(new CustomEvent('loadRecipe', {{ detail:{{ data:\"{content}\" }} }}));";
            await page.EvaluateAsync(loadScript);
            return await page.PdfAsync(new PagePdfOptions(){PreferCSSPageSize=true});
        }
    }
}