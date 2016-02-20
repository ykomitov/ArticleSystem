namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface IArticlesService
    {
        Article GetById(int id);
    }
}
