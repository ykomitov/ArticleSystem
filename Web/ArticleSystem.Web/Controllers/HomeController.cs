namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;

    public class HomeController : BaseController
    {
        private readonly IArticlesService jokes;
        private readonly ICategoriesService jokeCategories;

        public HomeController(
            IArticlesService jokes,
            ICategoriesService jokeCategories)
        {
            this.jokes = jokes;
            this.jokeCategories = jokeCategories;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
