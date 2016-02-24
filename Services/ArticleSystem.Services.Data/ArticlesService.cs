namespace ArticleSystem.Services.Data
{
    using System;
    using System.Linq;
    using ArticleSystem.Data.Common;
    using ArticleSystem.Data.Models;
    using Contracts;

    public class ArticlesService : IArticlesService
    {
        private readonly IDbRepository<Article> articles;

        public ArticlesService(IDbRepository<Article> articles)
        {
            this.articles = articles;
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }

        public IQueryable<Article> GetById(int id)
        {
            return this.articles
                .All()
                .Where(a => a.Id == id);
        }

        public IQueryable<Article> GetPagedArticles(int categoryId, int page, int pageSize)
        {
            return this.articles
                .All()
                .Where(a => a.CategoryId == categoryId)
                .OrderByDescending(a => a.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IQueryable<Article> GetSliderArticles()
        {
            return this.articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(4);
        }

        public IQueryable<Article> GetTopArticlesByVote(int numberOfArticles)
        {
            return this.articles
                .All()
                .OrderByDescending(a => a.Votes.Sum(v => (int)v.VoteType))
                .Take(numberOfArticles);
        }

        public IQueryable<Article> GetLatestArticles(int numberOfArticles)
        {
            return this.articles
                .All()
                .OrderByDescending(a => a.CreatedOn)
                .Take(numberOfArticles);
        }

        public IQueryable<Article> SearchInTitleAndText(string search)
        {
            return this.articles
                .All()
                .Where(
                a => a.Title.IndexOf(search) >= 0 ||
                a.TextHtml.IndexOf(search) >= 0);
        }

        public void SaveShanges()
        {
            this.articles.Save();
        }

        public void DeleteById(int id)
        {
            var articleToDelete = this.articles.GetById(id);
            this.articles.Delete(articleToDelete);
            this.articles.Save();
        }

        public IQueryable<Article> GetWithDeleted()
        {
            return this.articles.AllWithDeleted();
        }
    }
}
