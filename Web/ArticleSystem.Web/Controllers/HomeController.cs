namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;
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
            this.ViewBag.Title = "Welcome to our technology blog!";
            this.ViewBag.Subtitle = "Enjoy the latest news about technology, gadgets, science and software";

            return this.View();
        }
    }
}
