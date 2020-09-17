using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PictureBot.ImageUtils
{
    public class ImageConvert
    {
        public static Image GetGrayImage(Image Target)
        {
            var original = Target;
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
             new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
             new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
             new float[] {0, 0, 0, 0, 1}
                   });

                //create some image attributes
                using (ImageAttributes attributes = new ImageAttributes())
                {

                    //set the color matrix attribute
                    attributes.SetColorMatrix(colorMatrix);

                    //draw the original image on the new image
                    //using the grayscale color matrix
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        public static Image Get16Bit(Image Target)
        {
            Bitmap image = new Bitmap(Target.Width, Target.Height, PixelFormat.Format16bppRgb565);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.DrawImage(Target, 0, 0, Target.Width, Target.Height);
                return image;
            }
        }

        public static Image Get8Bit(Image Target)
        {
            var image = Target;
            using (var bitmap = new Bitmap(image))
            using (var stream = new MemoryStream())
            {
                var parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 8L);

                var info = GetEncoderInfo("image/tiff");
                bitmap.Save(stream, info, parameters);

                return Image.FromStream(stream);
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var imageEncoders = ImageCodecInfo.GetImageEncoders();
            return imageEncoders.FirstOrDefault(t => t.MimeType == mimeType);
        }
    }
}
