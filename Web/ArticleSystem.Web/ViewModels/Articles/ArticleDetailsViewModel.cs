namespace ArticleSystem.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class ArticleDetailsViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public string TextHtml { get; set; }

        public byte[] HeaderImage { get; set; }

        public string Author { get; set; }

        public int Votes { get; set; }

        public int TotalComments { get; set; }

        public ICollection<CommentDetailsViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(
                    a => a.Author,
                    opt => opt.MapFrom(a => a.Author.UserName));

            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(
                    c => c.Category,
                    opt => opt.MapFrom(a => a.Category.Name));

            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(
                    v => v.Votes,
                    opt => opt.MapFrom(a => a.Votes.Where(v => v.ArticleId == a.Id).Any() ? a.Votes.Where(v => v.ArticleId == a.Id).Sum(v => (int)v.VoteType) : 0));

            configuration.CreateMap<Article, ArticleDetailsViewModel>()
                .ForMember(
                    c => c.TotalComments,
                    opt => opt.MapFrom(a => a.Comments.Where(c => c.ArticleId == a.Id).Any() ? a.Comments.Where(c => c.ArticleId == a.Id).Count() : 0));
        }
    }
}
