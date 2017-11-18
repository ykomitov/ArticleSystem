namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;

    public class AboutController : Controller
    {
        [HttpGet]
        public ActionResult Contacts()
        {
            this.ViewBag.Title = "MVC Sample Blog System";
            this.ViewBag.Subtitle = "Final Project for Telerik Academy";

            return this.View();
        }
    }
}
