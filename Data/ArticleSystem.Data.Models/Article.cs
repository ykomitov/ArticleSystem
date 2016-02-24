namespace ArticleSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ArticleSystem.Common;
    using Common.Models;

    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [MaxLength(GlobalConstants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        public string TextHtml { get; set; }

        public byte[] HeaderImage { get; set; }

        [MaxLength(100)]
        public string HeaderImageFileName { get; set; }

        public byte[] SliderImage { get; set; }

        [MaxLength(100)]
        public string SliderImageFileName { get; set; }

        public byte[] IndexImage { get; set; }

        [MaxLength(100)]
        public string IndexImageFileName { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual int CategoryId { get; set; }

        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
