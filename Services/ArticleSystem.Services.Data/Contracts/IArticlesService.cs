namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface IArticlesService
    {
        IQueryable<Article> GetAll();

        Article GetById(int id);
    }
}
