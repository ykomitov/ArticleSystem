namespace ArticleSystem.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using ArticleSystem.Common;
    using ArticleSystem.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}
