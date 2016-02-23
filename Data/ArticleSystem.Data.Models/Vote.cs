namespace ArticleSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class Vote : BaseModel<int>
    {
        [Required]
        public VoteType VoteType { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public string VoterId { get; set; }

        public virtual ApplicationUser Voter { get; set; }
    }
}
