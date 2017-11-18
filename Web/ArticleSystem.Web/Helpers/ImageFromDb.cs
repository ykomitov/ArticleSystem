namespace ArticleSystem.Web.Helpers
{
    using System;

    public class ImageFromDb
    {
        private const string DefaultHeaderImage = "/images/default-header-img.jpg";
        private const string DefaultCommentImage = "/images/avatar.jpg";
        private const string DefaultSliderImage = "/images/default-slider.jpg";
        private const string DefaultIndexImage = "/images/default-front-page.jpg";

        public static string Image(byte[] imageAsByteArr, string type)
        {
            if (imageAsByteArr != null)
            {
                string imageBase64 = Convert.ToBase64String(imageAsByteArr);
                return string.Format("data:image/jpeg;base64,{0}", imageBase64);
            }
            else
            {
                if (type == "header")
                {
                    return DefaultHeaderImage;
                }
                else if (type == "comment")
                {
                    return DefaultCommentImage;
                }
                else if (type == "slider")
                {
                    return DefaultSliderImage;
                }
                else if (type == "index")
                {
                    return DefaultIndexImage;
                }
                else
                {
                    throw new Exception("Could not get image from database. Check your custom helper!");
                }
            }
        }
    }
}
