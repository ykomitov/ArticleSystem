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
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public byte[] HeaderImage { get; set; }

        public string HeaderImageFileName { get; set; }

        public byte[] SliderImage { get; set; }

        public string SliderImageFileName { get; set; }

        public virtual int CategoryId { get; set; }

        [Required]
        public virtual ArticleCategory Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
