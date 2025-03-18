using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace FileCompress.Services
{
    public class ImageCompressionService
    {
        public void CompressImage(string inputPath, string outputPath, long quality)
        {
            using (var image = new Bitmap(inputPath))
            {
                ImageCodecInfo jpgEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
                if (jpgEncoder == null) return;

                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                image.Save(outputPath, jpgEncoder, encoderParams);
            }
        }
    }
}