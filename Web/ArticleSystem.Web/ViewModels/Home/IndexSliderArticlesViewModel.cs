﻿namespace ArticleSystem.Web.ViewModels.Home
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class IndexSliderArticlesViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextHtml { get; set; }

        public byte[] SliderImage { get; set; }
    }
}
