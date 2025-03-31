using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Tiff;
using iText.Forms.Fields;
using iText.IO.Font;

namespace FileCompress.Services
{
    public class PngCompressionService
    {
        public void CompressPng(string inputPath, string outputPath)
        {
            using (Image image = Image.Load(inputPath))
            {
                var encoder = new PngEncoder()
                {
                    CompressionLevel = PngCompressionLevel.Level9, // Максимальное сжатие без потерь
                    FilterMethod = PngFilterMethod.Paeth // Оптимальный PNG-фильтр
                };

                image.Save(outputPath, encoder);
            }
        }

        public void ResizeAndCompressImage(string inputPath, string outputPath, int quality, int maxWidth, int maxHeight)
        {
            IImageFormat format;
            using (Image image = Image.Load<Rgba32>(inputPath))
            {
                format = image.Metadata.DecodedImageFormat;
                // Определяем, нужно ли уменьшать размер
                if ((maxWidth != 0 && maxWidth != -1 && maxHeight != 0 && maxHeight != -1) && (image.Width > maxWidth || image.Height > maxHeight))
                {
                    float scale = Math.Min((float)maxWidth / image.Width, (float)maxHeight / image.Height);
                    int newWidth = (int)(image.Width * scale);
                    int newHeight = (int)(image.Height * scale);

                    image.Mutate(x => x.Resize(newWidth, newHeight)); // Изменение размера
                }

                // Определяем настройки кодирования в зависимости от формата
                IImageEncoder encoder = format switch
                {
                    JpegFormat => new JpegEncoder { Quality = quality }, // Сжатие JPEG
                    PngFormat => new PngEncoder { CompressionLevel = PngCompressionLevel.Level9 }, // Без потерь для PNG
                    GifFormat => new GifEncoder(), // GIF без сжатия (можно добавить оптимизацию)
                    TiffFormat => new TiffEncoder { CompressionLevel = SixLabors.ImageSharp.Compression.Zlib.DeflateCompressionLevel.Level9 },
                    _ => throw new NotSupportedException($"Формат {format.Name} не поддерживается")
                };

                image.Save(outputPath, encoder); // Сохраняем с новыми параметрами
            }
        }
    }
}