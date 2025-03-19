using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace FileCompress.Services
{
    public class ImageCompressionService
    {
        public void CompressImage(string inputPath, string outputPath, int quality)
        {
            using (var image = new Bitmap(inputPath))
            {
                ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                if (jpgEncoder == null) return;

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                if (image.Width >= 5000 || image.Height >= 5000)
                {
                    int newWidth = image.Width / 2;   // Уменьшаем в 2 раза
                    int newHeight = image.Height / 2;
                    using (var resized = new Bitmap(image, new Size(newWidth, newHeight)))
                    {
                        resized.Save(outputPath, jpgEncoder, encoderParams);
                    }
                }
                else
                {
                    image.Save(outputPath, jpgEncoder, encoderParams);
                }

            }
            var fileIn = new FileInfo(inputPath);
            var fileOut = new FileInfo(outputPath);
            if (fileIn != null && fileOut != null && fileIn.Length < fileOut.Length)
                File.Copy(inputPath, outputPath, true);
        }
    }
}