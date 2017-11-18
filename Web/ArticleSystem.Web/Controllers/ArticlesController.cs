namespace ArticleSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        private const int ItemsPerPage = 2;
        private const int NumberOfArticlesInAsideList = 6;

        private readonly IArticlesService articles;
        private readonly ICommentsService comments;
        private readonly ICategoriesService categories;

        public ArticlesController(IArticlesService articles, ICommentsService comments, ICategoriesService categories)
        {
            this.articles = articles;
            this.comments = comments;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult News([Bind(Prefix = "id")]int page = 1)
        {
            this.ViewBag.Title = GlobalConstants.NewsPageTitle;
            this.ViewBag.Subtitle = GlobalConstants.NewsPageSubtitle;

            var viewModel = this.GetPagedArticlesByCategory("News", page, ItemsPerPage);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Tech([Bind(Prefix = "id")]int page = 1)
        {
            this.ViewBag.Title = GlobalConstants.TechPageTitle;
            this.ViewBag.Subtitle = GlobalConstants.TechPageSubtitle;

            var viewModel = this.GetPagedArticlesByCategory("Tech", page, ItemsPerPage);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Devices([Bind(Prefix = "id")]int page = 1)
        {
            this.ViewBag.Title = GlobalConstants.DevicesPageTitle;
            this.ViewBag.Subtitle = GlobalConstants.DevicesPageSubtitle;

            var viewModel = this.GetPagedArticlesByCategory("Devices", page, ItemsPerPage);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Soft([Bind(Prefix = "id")]int page = 1)
        {
            this.ViewBag.Title = GlobalConstants.SoftwarePageTitle;
            this.ViewBag.Subtitle = GlobalConstants.SoftwarePageSubtitle;

            var viewModel = this.GetPagedArticlesByCategory("Soft", page, ItemsPerPage);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Science([Bind(Prefix = "id")]int page = 1)
        {
            this.ViewBag.Title = GlobalConstants.SciencePageTitle;
            this.ViewBag.Subtitle = GlobalConstants.SciencePageSubtitle;

            var viewModel = this.GetPagedArticlesByCategory("Science", page, ItemsPerPage);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var article = this.articles
                .GetById(id)
                .To<ArticleDetailsViewModel>()
                .FirstOrDefault();

            this.GetAsideArticles();

            return this.View(article);
        }

        private PagedArticlesViewModel GetPagedArticlesByCategory(string category, int page, int pageSize)
        {
            PagedArticlesViewModel viewModel;
            var categoryId = this.categories.GetCategoryId(category);
            var allArticlesCount = this.articles.GetAll().Where(a => a.Category.Name == category).Count();
            var totalPages = (int)Math.Ceiling(allArticlesCount / (decimal)pageSize);
            var cacheKey = category + "_page_" + page;

            var articlesOnPage = this.Cache.Get(
                cacheKey,
                () => this.articles
                  .GetPagedArticles(categoryId, page, pageSize)
                  .To<ArticleDetailsViewModel>()
                  .ToList(),
                10 * 60);

            viewModel = new PagedArticlesViewModel()
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Articles = articlesOnPage,
                CategoryName = category
            };

            this.GetAsideArticles();

            return viewModel;
        }

        private void GetAsideArticles()
        {
            var latestArticles = this.Cache.Get(
                "latestArticles",
               () => this.articles
                .GetLatestArticles(NumberOfArticlesInAsideList)
                .To<ListedArticlesViewModel>()
                .ToList(),
               30 * 60);

            var topRatedArticles = this.Cache.Get(
                "topRatedArticles",
                () => this.articles
                .GetTopArticlesByVote(NumberOfArticlesInAsideList)
                .To<ListedArticlesViewModel>()
                .ToList(),
                30 * 60);

            AsideArticlesViewModel asideArticles = new AsideArticlesViewModel
            {
                HotArticles = latestArticles,
                ArchiveArticles = topRatedArticles
            };

            this.ViewBag.AsideArticles = asideArticles;
        }
    }
}
