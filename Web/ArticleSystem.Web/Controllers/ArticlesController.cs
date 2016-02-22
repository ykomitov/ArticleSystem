namespace ArticleSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Caching;
    using System.Web.Mvc;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private const int ItemsPerPage = 2;

        private readonly IArticlesService articles;
        private readonly ICategoriesService categories;

        public ArticlesController(IArticlesService articles, ICategoriesService categories)
        {
            this.articles = articles;
            this.categories = categories;
        }

        public ActionResult News(int id = 1)
        {
            this.ViewBag.Title = "Latest news";
            this.ViewBag.Subtitle = "Enjoy our selection of breaking news from the world of coputing";

            PagedArticlesViewModel viewModel;

            var page = id;
            var allItemsCount = this.articles.GetAll().Where(a => a.Category.Id == 1).Count();
            var totalPages = (int)Math.Ceiling(allItemsCount / (decimal)ItemsPerPage);

            var articlesOnPage = this.Cache.Get(
                "News_page_" + id,
                () => this.articles
                  .GetAll()
                  .Where(a => a.Category.Id == 1)
                  .OrderBy(x => x.CreatedOn)
                  .Skip((page - 1) * ItemsPerPage)
                  .Take(ItemsPerPage)
                  .To<ArticleViewModel>()
                  .ToList(),
                10 * 60);

            viewModel = new PagedArticlesViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Articles = articlesOnPage
            };

            return this.View(viewModel);
        }

        public ActionResult NewsArticleDetails(string id)
        {
            int articleId = int.Parse(id);
            var article = this.articles
                .GetById(articleId)
                .To<ArticleViewModel>()
                .FirstOrDefault();

            return this.View(article);
        }

        public ActionResult Tech()
        {
            this.ViewBag.Title = "Technology developments";
            this.ViewBag.Subtitle = "Get the latest technology and engineering news and insight";

            return this.View();
        }

        public ActionResult Devices()
        {
            this.ViewBag.Title = "New devices on the horizon";
            this.ViewBag.Subtitle = "Read the articles about the gadgets that caught our attention";

            return this.View();
        }

        public ActionResult Soft()
        {
            this.ViewBag.Title = "Software++";
            this.ViewBag.Subtitle = "Recent trends and news in software development";

            return this.View();
        }

        public ActionResult Science()
        {
            this.ViewBag.Title = "Science";
            this.ViewBag.Subtitle = "Catch up on the developments in science...";

            return this.View();
        }
    }
}