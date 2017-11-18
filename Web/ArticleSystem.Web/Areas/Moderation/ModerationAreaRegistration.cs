namespace ArticleSystem.Web.Areas.Moderation
{
    using System.Web.Mvc;

    public class ModerationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Moderation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Moderation_default",
                "Moderation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "ArticleSystem.Web.Areas.Moderation.Controllers" });
        }
    }
}
