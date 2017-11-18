namespace ArticleSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [Authorize]
    public class VotesController : BaseController
    {
        private readonly IVotesService votes;

        public VotesController(IVotesService votes)
        {
            this.votes = votes;
        }

        [HttpPost]
        public ActionResult Vote(int articleId, int voteType)
        {
            if (voteType > 1)
            {
                voteType = 1;
            }

            if (voteType < -1)
            {
                voteType = -1;
            }

            var userId = this.User.Identity.GetUserId();

            var vote = this.votes
                .GetAll()
                .Where(v => v.VoterId == userId && v.ArticleId == articleId)
                .FirstOrDefault();

            if (vote == null)
            {
                vote = new Vote()
                {
                    VoterId = userId,
                    ArticleId = articleId,
                    VoteType = (VoteType)voteType
                };

                this.votes.Add(vote);
            }
            else
            {
                if (vote.VoteType == VoteType.Negative && voteType > 0)
                {
                    vote.VoteType = VoteType.Neutral;
                }
                else if (vote.VoteType == VoteType.Positive && voteType < 0)
                {
                    vote.VoteType = VoteType.Neutral;
                }
                else
                {
                    vote.VoteType = (VoteType)voteType;
                }
            }

            this.votes.SaveChanges();

            var postVotes = this.votes
                .GetAll()
                .Where(v => v.ArticleId == articleId)
                .Sum(v => (int)v.VoteType);

            return this.Json(new { Count = postVotes });
        }
    }
}
