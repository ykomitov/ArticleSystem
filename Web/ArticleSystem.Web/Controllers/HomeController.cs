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
        private readonly IArticlesService articles;
        private readonly ICategoriesService categories;

        public HomeController(IArticlesService articles, ICategoriesService categories)
        {
            this.articles = articles;
            this.categories = categories;
        }

        public ActionResult Index()
        {
            this.ViewBag.Title = GlobalConstants.HomePageTitle;
            this.ViewBag.Subtitle = GlobalConstants.HomePageSubtitle;

            var sliderArticles = this.articles
                .GetSliderArticles()
                .To<IndexSliderArticlesViewModel>()
                .ToList();

            this.ViewBag.Articles = sliderArticles;

            var viewModel = this.articles
                .GetMainContentArticles()
                .To<IndexTopRatedArticlesViewModel>()
                .ToList();

            return this.View(viewModel);
        }
    }
}
