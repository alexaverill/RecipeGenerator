using RecipeGenerator.Abstractions;
using Markdig;
namespace RecipeGenerator.Services
{
    public class MarkdownService:IMarkdownConverter    {
        public string Convert(string content){
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(content,pipeline);
        }
    }
}