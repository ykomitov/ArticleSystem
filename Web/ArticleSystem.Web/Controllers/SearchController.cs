namespace ArticleSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;
    using ViewModels.Search;

    public class SearchController : BaseController
    {
        private readonly IArticlesService articles;

        public SearchController(IArticlesService articles)
        {
            this.articles = articles;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchResult(string searchInput)
        {
            this.ViewBag.Title = GlobalConstants.SearchPageTitle;
            this.ViewBag.Subtitle = GlobalConstants.SearchPageSubtitle + searchInput + "\" returned";

            var foundArticles = this.articles
                .SearchInTitleAndText(searchInput)
                .To<FoundArticlesViewModel>()
                .ToList();

            return this.View(foundArticles);
        }
    }
}