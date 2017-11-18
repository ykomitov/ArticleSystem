namespace ArticleSystem.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    public class AsideArticlesViewModel
    {
        public ICollection<ListedArticlesViewModel> HotArticles { get; set; }

        public ICollection<ListedArticlesViewModel> ArchiveArticles { get; set; }
    }
}
