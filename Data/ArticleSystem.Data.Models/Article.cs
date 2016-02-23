namespace ArticleSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(87)]
        public string Title { get; set; }

        [Required]
        public string TextHtml { get; set; }

        public byte[] HeaderImage { get; set; }

        [MaxLength(100)]
        public string HeaderImageFileName { get; set; }

        public byte[] SliderImage { get; set; }

        [MaxLength(100)]
        public string SliderImageFileName { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        public virtual int CategoryId { get; set; }

        [Required]
        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
