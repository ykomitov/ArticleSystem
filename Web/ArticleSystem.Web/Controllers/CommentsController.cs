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
        public ActionResult Post(string textInput, string articleId)
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
    }
}