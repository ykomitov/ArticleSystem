namespace ArticleSystem.Tools.ImageConverter
{
    using System.Drawing;
    using System.IO;

    public class ImageConverter
    {
        public byte[] ConvertImageToByteArray(string pathToImage)
        {
            Image inputImage = Image.FromFile(pathToImage);

            using (MemoryStream ms = new MemoryStream())
            {
                inputImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        public Image ConvertByteArrayToImage(byte[] imageAsByteArr)
        {
            using (MemoryStream ms = new MemoryStream(imageAsByteArr))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
    }
}
