namespace ArticleSystem.Web.ViewModels.Articles
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ListedArticlesViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
