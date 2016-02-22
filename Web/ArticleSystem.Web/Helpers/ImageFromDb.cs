namespace ArticleSystem.Web.Helpers
{
    using System;

    public class ImageFromDb
    {
        public static string Image(byte[] imageAsByteArr)
        {
            const string DefaultHeaderImage = "~/images/default-header-img.jpg";

            if (imageAsByteArr != null)
            {
                string imageBase64 = Convert.ToBase64String(imageAsByteArr);
                return string.Format("data:image/jpeg;base64,{0}", imageBase64);
            }
            else
            {
                return DefaultHeaderImage;
            }
        }
    }
}