using RecipeGenerator.Abstractions;
using Markdig;
namespace RecipeGenerator.Services
{
    public class MarkdownService:IMarkdownConverter    {
        public string Convert(string content){
            //parse out images and replace with base64 strings
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(content,pipeline);
        }
    }
}