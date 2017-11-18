namespace ArticleSystem.Web.Areas.Administration.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class ArticleInputModel
    {
        public int Id { get; set; }

        [MaxLength(GlobalConstants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }
    }
}
