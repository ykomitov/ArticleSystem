namespace ArticleSystem.Services.Data.Contracts
{
    using System.Linq;
    using ArticleSystem.Data.Models;

    public interface ICommentsService
    {
        IQueryable<Comment> GetAll();

        IQueryable<Comment> GetArticleComments(int articleId);

        Comment GetById(int id);

        void AddComment(Comment newComment);
    }
}
