namespace ArticleSystem.Services.Data
{
    using System;
    using System.Linq;
    using ArticleSystem.Data.Common;
    using ArticleSystem.Data.Models;
    using Contracts;

    public class VotesService : IVotesService
    {
        private readonly IDbRepository<Vote> votes;

        public VotesService(IDbRepository<Vote> votes)
        {
            this.votes = votes;
        }

        public void Add(Vote newVote)
        {
            this.votes.Add(newVote);
            this.votes.Save();
        }

        public IQueryable<Vote> GetAll()
        {
            return this.votes.All();
        }

        public void SaveChanges()
        {
            this.votes.Save();
        }
    }
}
