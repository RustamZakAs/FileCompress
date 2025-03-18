using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using System;

namespace FileCompress.Services
{
    public class PdfCompressionService
    {
        public void CompressPdf(string inputFilePath, string outputFilePath)
        {
            try
            {
                using (PdfReader pdfReader = new PdfReader(inputFilePath))
                {
                    pdfReader.SetUnethicalReading(true); // Разрешаем обработку защищенных файлов

                    using (PdfWriter pdfWriter = new PdfWriter(outputFilePath).SetSmartMode(true).SetCompressionLevel(1)) // Smart Copy Mode
                    using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                    {
                        pdfDocument.SetFlushUnusedObjects(false); // Оптимизация

                        // Пройтись по страницам и уменьшить размер изображений
                        for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
                        {
                            PdfPage page = pdfDocument.GetPage(pageNum);
                            PdfResources resources = page.GetResources();
                            PdfDictionary xObjects = resources.GetResource(PdfName.XObject);

                            if (xObjects != null)
                            {
                                foreach (var key in xObjects.KeySet())
                                {
                                    PdfStream stream = xObjects.GetAsStream(key);
                                    if (stream != null)
                                    {
                                        // Пример простой компрессии (можно улучшить с использованием ImageDataFactory)
                                        stream.SetCompressionLevel(1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сжатии PDF: {ex.Message}");
            }
        }

        public void CompressImagesInPdf(string inputFilePath, string outputFilePath)
        {
            try
            {
                using (PdfReader pdfReader = new PdfReader(inputFilePath))
                using (PdfWriter pdfWriter = new PdfWriter(outputFilePath).SetSmartMode(true).SetCompressionLevel(9)) // Максимальная компрессия без потери качества
                using (PdfDocument pdfDoc = new PdfDocument(pdfReader, pdfWriter))
                {
                    pdfDoc.SetFlushUnusedObjects(false); // Оптимизация

                    for (int pageNum = 1; pageNum <= pdfDoc.GetNumberOfPages(); pageNum++)
                    {
                        PdfPage page = pdfDoc.GetPage(pageNum);
                        PdfResources resources = page.GetResources();
                        PdfDictionary xObjects = resources.GetResource(PdfName.XObject);

                        if (xObjects != null)
                        {
                            foreach (PdfName name in xObjects.KeySet())
                            {
                                PdfStream stream = xObjects.GetAsStream(name);
                                if (stream != null && stream.Get(PdfName.Subtype).Equals(PdfName.Image))
                                {
                                    byte[] bytes = stream.GetBytes();
                                    ImageData img = ImageDataFactory.Create(bytes);

                                    PdfImageXObject imgObject = new PdfImageXObject(img);
                                    xObjects.Put(name, imgObject.GetPdfObject());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке изображений: {ex.Message}");
            }
        }
    }
}