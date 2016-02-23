namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<ArticleCategory> GetAll();

        int GetCategoryId(string categoryName);
    }
}
