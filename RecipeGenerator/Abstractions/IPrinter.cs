using System.IO.Compression;
namespace RecipeGenerator.Abstractions{
    public interface IPrinter{
        Task<byte[]> print(RecipePage recipes, string stylesheet);
    }
}