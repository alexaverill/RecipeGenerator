namespace RecipeGenerator.Abstractions{
    public interface IMarkdownConverter{
       public string Convert(string htmlContent);
    }
}