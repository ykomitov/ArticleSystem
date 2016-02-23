namespace ArticleSystem.Web.ViewModels.Home
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class FooterArticlesViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Comments { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, FooterArticlesViewModel>()
                .ForMember(
                    v => v.Comments,
                    opt => opt.MapFrom(a => a.Comments.Where(c => c.ArticleId == a.Id).Any() ? a.Comments.Where(c => c.ArticleId == a.Id).Count() : 0));
        }
    }
}