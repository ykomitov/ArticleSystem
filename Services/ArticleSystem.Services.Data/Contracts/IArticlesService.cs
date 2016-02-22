namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface IArticlesService
    {
        IQueryable<Article> GetAll();

        IQueryable<Article> GetById(int id);
    }
}
