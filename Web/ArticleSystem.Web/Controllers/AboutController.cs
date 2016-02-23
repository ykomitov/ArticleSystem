namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class AboutController : Controller
    {
        [HttpGet]
        public ActionResult Contacts()
        {
            return this.View();
        }
    }
}