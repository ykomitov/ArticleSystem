namespace ArticleSystem.Web.Areas.Administration.Models
{
    using System;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextHtml { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}