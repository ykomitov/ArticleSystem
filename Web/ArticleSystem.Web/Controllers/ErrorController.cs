namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
