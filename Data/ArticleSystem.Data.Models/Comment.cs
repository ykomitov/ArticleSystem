namespace ArticleSystem.Data.Models
{
    using System.Collections.Generic;
    using ArticleSystem.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public Comment()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string CommentText { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
