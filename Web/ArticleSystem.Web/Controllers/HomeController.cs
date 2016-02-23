namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Common;
    using Services.Data.Contracts;

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

            // select 4 latest articles from db
            // add them to the slider

            // select 6 more articles (top rating) from db
            // add them to the info

            return this.View();
        }
    }
}
