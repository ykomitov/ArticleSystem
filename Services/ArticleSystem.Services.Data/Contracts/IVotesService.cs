namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;
    using ArticleSystem.Data.Models;

    public interface IVotesService
    {
        IQueryable<Vote> GetAll();

        void Add(Vote newVote);

        void SaveChanges();
    }
}
