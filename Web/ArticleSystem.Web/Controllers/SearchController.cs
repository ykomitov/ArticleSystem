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
            var foundArticles = this.articles
                .SearchInTitleAndText(searchInput)
                .To<FoundArticlesViewModel>()
                .ToList();

            var numberOfResults = foundArticles.Count;
            var endWord = string.Format("{0}", foundArticles.Count > 1 ? " results" : " result");
            var subtitle = GlobalConstants.SearchPageSubtitle + searchInput + "\" returned " + numberOfResults + endWord;

            this.ViewBag.Title = GlobalConstants.SearchPageTitle;
            this.ViewBag.Subtitle = subtitle;

            return this.View(foundArticles);
        }
    }
}