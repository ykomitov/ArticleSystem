namespace ArticleSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;

    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentsService comments;

        public CommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(string textInput, string articleId)
        {
            if (textInput == null)
            {
                this.Redirect("~/Articles/Details/" + articleId);
            }

            var author = this.User.Identity.GetUserId();
            var article = int.Parse(articleId);

            var newComment = new Comment()
            {
                ArticleId = article,
                AuthorId = author,
                CommentText = textInput
            };

            this.comments.AddComment(newComment);

            return this.RedirectToAction("Details", "Articles", new { id = article });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostReply(string textInput, string articleId, string replyToCommentWithId)
        {
            if (textInput == null || replyToCommentWithId == null)
            {
                this.Redirect("~/Articles/Details/" + articleId);
            }

            var author = this.User.Identity.GetUserId();
            var article = int.Parse(articleId);
            int parentCommentId;
            var commentParseSuccessful = int.TryParse(replyToCommentWithId, out parentCommentId);

            var newComment = new Comment()
            {
                ArticleId = article,
                AuthorId = author,
                CommentText = textInput
            };

            if (commentParseSuccessful)
            {
                newComment.ParentCommentID = parentCommentId;
            }

            this.comments.AddComment(newComment);

            return this.RedirectToAction("Details", "Articles", new { id = article });
        }
    }
}
