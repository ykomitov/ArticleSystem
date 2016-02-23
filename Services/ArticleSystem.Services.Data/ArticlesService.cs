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

        public IQueryable<Article> GetById(int id)
        {
            return this.articles
                .All()
                .Where(a => a.Id == id);
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }

        public IQueryable<Article> GetPagedArticles(int categoryId, int page, int pageSize)
        {
            return this.articles
                .All()
                .Where(a => a.CategoryId == categoryId)
                .OrderBy(a => a.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
