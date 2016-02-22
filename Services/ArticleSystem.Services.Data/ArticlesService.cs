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

        public Article GetById(int id)
        {
            var article = this.articles.GetById(id);
            return article;
        }

        public IQueryable<Article> GetAll()
        {
            return this.articles.All();
        }
    }
}
