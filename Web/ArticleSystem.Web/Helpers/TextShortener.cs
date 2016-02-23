namespace ArticleSystem.Web.Helpers
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public class TextShortener
    {
        public static string ShortenText(string originalText, int length)
        {
            var plainText = Regex.Replace(originalText, "<.*?>", string.Empty);

            if (plainText.Length <= length)
            {
                return plainText;
            }

            var cutText = plainText.Substring(0, length);
            var properlyCutText = cutText.Substring(0, cutText.LastIndexOf(" ")) + "...";

            return properlyCutText;
        }
    }
}