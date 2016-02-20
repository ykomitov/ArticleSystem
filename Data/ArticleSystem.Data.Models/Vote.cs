namespace ArticleSystem.Data.Models
{
    using ArticleSystem.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public VoteType VoteType { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
