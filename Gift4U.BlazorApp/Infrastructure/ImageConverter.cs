using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Gift4U.BlazorApp.Infrastructure
{
    public class ImageConverter
    {
        public static string ImageToBase64(string filePath, int MaxWidth = 400, int MaxHeight = 300)
        {
            string base64String;
            using (var image = SixLabors.ImageSharp.Image.Load(filePath))
            {
                double scaleX = 1.0 * image.Width / MaxWidth;
                double scaleY = 1.0 * image.Height / MaxHeight;
                // If input image is 1600 x 300 then scales are 2 and 0.5
                // Take the larger scale 2 to get final image size 800 x 150 to keep aspect ratio
                double scale = Math.Max(scaleX, scaleY);
                int finalWidth = Convert.ToInt32(image.Width / scale);
                int finalHeight = Convert.ToInt32(image.Height / scale);

                image.Mutate(x => x.Resize(finalWidth, finalHeight));

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new PngEncoder());
                    image.Dispose();
                    byte[] imageBytes = ms.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    imageBytes = null;
                    ms.Dispose();
                }
            }
            GC.Collect();
            return base64String;
        }
    }
}
