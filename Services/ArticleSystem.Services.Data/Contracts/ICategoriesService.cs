namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<ArticleCategory> GetAll();

        IQueryable<ArticleCategory> GetCategorySortedById();

        int GetCategoryId(string categoryName);
    }
}
