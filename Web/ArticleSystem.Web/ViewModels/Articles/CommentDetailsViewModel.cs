namespace ArticleSystem.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class CommentDetailsViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string CommentText { get; set; }

        public string Author { get; set; }

        public byte[] AuthorAvatar { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<CommentReplyViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentDetailsViewModel>()
               .ForMember(
                   c => c.Author,
                   opt => opt.MapFrom(c => c.Author.UserName));

            configuration.CreateMap<Comment, CommentDetailsViewModel>()
               .ForMember(
                   c => c.AuthorAvatar,
                   opt => opt.MapFrom(c => c.Author.Avatar));
        }
    }
}