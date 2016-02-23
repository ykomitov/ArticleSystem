namespace ArticleSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Home;

    public class HomeController : BaseController
    {
        private const int NumberOfTopArticles = 6;
        private const int SecondsToCacheIndexPage = 15 * 60;

        private readonly IArticlesService articles;
        private readonly ICategoriesService categories;

        public HomeController(IArticlesService articles, ICategoriesService categories)
        {
            this.articles = articles;
            this.categories = categories;
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.Title = GlobalConstants.HomePageTitle;
            this.ViewBag.Subtitle = GlobalConstants.HomePageSubtitle;

            var sliderArticles = this.Cache.Get(
                "indexPageSliderArticles",
                () => this.articles
                .GetSliderArticles()
                .To<IndexSliderArticlesViewModel>()
                .ToList(),
                SecondsToCacheIndexPage);

            this.ViewBag.Articles = sliderArticles;

            var viewModel = this.Cache.Get(
                "indexPageViewModel",
                () => this.articles
                .GetTopArticlesByVote(NumberOfTopArticles)
                .To<IndexTopRatedArticlesViewModel>()
                .ToList(),
                SecondsToCacheIndexPage);

            return this.View(viewModel);
        }
    }
}
