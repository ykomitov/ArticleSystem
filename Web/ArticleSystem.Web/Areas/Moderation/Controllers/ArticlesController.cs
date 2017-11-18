namespace ArticleSystem.Web.Areas.Moderation.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using Web.Controllers;

    public class ArticlesController : BaseController
    {
        private readonly ICategoriesService categories;

        public ArticlesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        public ActionResult Index()
        {
            // TODO: use caching here
            // TODO: Extract in service layer?
            var sortedCategories = this.categories.GetCategorySortedById().ToList();
            var categoriesList = new List<SelectListItem>();

            foreach (var category in sortedCategories)
            {
                var listItem = new SelectListItem();
                listItem.Text = category.Name;
                listItem.Value = category.Id.ToString();

                categoriesList.Add(listItem);
            }

            this.ViewBag.CategoriesList = categoriesList;

            return this.View();
        }

        public ActionResult ImageBrowser()
        {
            return this.View();
        }
    }
}
