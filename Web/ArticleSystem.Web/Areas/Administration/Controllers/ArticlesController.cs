namespace ArticleSystem.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Data.Models;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Models;
    using Services.Data.Contracts;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class ArticlesController : Controller
    {
        private readonly IArticlesService articles;

        public ArticlesController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Articles_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Article> articles = this.articles.GetWithDeleted();
            DataSourceResult result = articles.ToDataSourceResult(request, article => new
            {
                Id = article.Id,
                Title = article.Title,
                CreatedOn = article.CreatedOn,
                ModifiedOn = article.ModifiedOn,
                IsDeleted = article.IsDeleted,
                DeletedOn = article.DeletedOn
            });

            var jsonResult = this.Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Articles_Update([DataSourceRequest]DataSourceRequest request, ArticleInputModel article)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.articles.GetWithDeleted().Where(a => a.Id == article.Id).FirstOrDefault();
                entity.Title = article.Title;
                entity.IsDeleted = article.IsDeleted;

                this.articles.SaveShanges();
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Articles_Destroy([DataSourceRequest]DataSourceRequest request, Article article)
        {
            var articleToDelete = this.articles.GetById(article.Id).FirstOrDefault();

            if (articleToDelete != null)
            {
                this.articles.DeleteById(articleToDelete.Id);
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
