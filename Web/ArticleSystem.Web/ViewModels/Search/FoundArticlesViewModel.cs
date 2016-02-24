namespace ArticleSystem.Web.ViewModels.Search
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class FoundArticlesViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}