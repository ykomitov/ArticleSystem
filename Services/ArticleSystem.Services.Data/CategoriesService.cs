namespace ArticleSystem.Services.Data
{
    using System;
    using System.Linq;
    using ArticleSystem.Data.Common;
    using ArticleSystem.Data.Models;
    using Contracts;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDbRepository<ArticleCategory> categories;

        public CategoriesService(IDbRepository<ArticleCategory> categories)
        {
            this.categories = categories;
        }

        public IQueryable<ArticleCategory> GetAll()
        {
            return this.categories.All().OrderBy(x => x.Name);
        }

        public IQueryable<ArticleCategory> GetCategorySortedById()
        {
            return this.categories.All().OrderBy(x => x.Id);
        }

        public int GetCategoryId(string categoryName)
        {
            return this.categories
                .All()
                .Where(c => c.Name == categoryName)
                .FirstOrDefault()
                .Id;
        }
    }
}
