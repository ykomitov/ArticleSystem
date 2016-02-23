namespace ArticleSystem.Web.Helpers
{
    using System;
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

        public static string ShortenHtml(string originalText, int length)
        {
            var textWithLineBreaks = Regex.Replace(originalText, "</p>", Environment.NewLine);
            var plainText = Regex.Replace(textWithLineBreaks, "<.*?>", string.Empty);

            if (plainText.Length <= length)
            {
                return plainText.Replace(Environment.NewLine, "<br/><br/>");
            }

            var cutText = plainText.Substring(0, length);
            var properlyCutText = cutText.Substring(0, cutText.LastIndexOf(" ")) + "...";

            return properlyCutText.Replace(Environment.NewLine, "<br/><br/>");
        }
    }
}