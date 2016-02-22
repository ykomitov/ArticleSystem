namespace ArticleSystem.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    public class PagedArticlesViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}