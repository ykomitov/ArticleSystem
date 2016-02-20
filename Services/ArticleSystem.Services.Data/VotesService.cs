namespace ArticleSystem.Services.Data
{
    using System;

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

        public Vote GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
