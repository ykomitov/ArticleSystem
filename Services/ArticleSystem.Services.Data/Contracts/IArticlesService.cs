namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;

    using ArticleSystem.Data.Models;

    public interface IArticlesService
    {
        IQueryable<Article> GetAll();

        IQueryable<Article> GetById(int id);

        IQueryable<Article> GetPagedArticles(int categoryId, int page, int pageSize);

        IQueryable<Article> GetSliderArticles();

        IQueryable<Article> GetTopArticlesByVote(int numberOfArticles);

        IQueryable<Article> GetLatestArticles(int numberOfArticles);

        IQueryable<Article> SearchInTitleAndText(string search);

        void SaveShanges();

        void DeleteById(int id);

        IQueryable<Article> GetWithDeleted();
    }
}
