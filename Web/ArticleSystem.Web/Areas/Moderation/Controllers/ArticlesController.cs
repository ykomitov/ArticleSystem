namespace ArticleSystem.Web.Areas.Moderation.Controllers
{
    using System.Web.Mvc;

    public class ArticlesController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult ImageBrowser()
        {
            return this.View();
        }
    }
}