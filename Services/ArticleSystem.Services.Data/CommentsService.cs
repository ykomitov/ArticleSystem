﻿namespace ArticleSystem.Services.Data
{
    using System.Linq;
    using ArticleSystem.Data.Common;
    using ArticleSystem.Data.Models;
    using Contracts;

    public class CommentsService : ICommentsService
    {
        private readonly IDbRepository<Comment> comments;

        public CommentsService(IDbRepository<Comment> comments)
        {
            this.comments = comments;
        }

        public IQueryable<Comment> GetAll()
        {
            return this.comments.All();
        }

        public Comment GetById(int id)
        {
            return this.comments.GetById(id);
        }
    }
}
