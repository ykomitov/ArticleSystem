namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class AboutController : Controller
    {
        public ActionResult Contacts()
        {
            return this.View();
        }
    }
}