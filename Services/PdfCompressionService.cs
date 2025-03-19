using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Data;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Xobject;
using iText.Signatures;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileCompress.Services
{
    public class PdfCompressionService
    {
        public void CompressPdf(string inputFilePath, string outputFilePath)
        {
            if (PdfImageChecker.ContainsImages(inputFilePath))
            {
                CompressPdfv1(inputFilePath, outputFilePath);
            }
            else
            {
                CompressImagesInPdf(inputFilePath, outputFilePath);
            }
        }

        public void CompressPdfv1(string inputFilePath, string outputFilePath)
        {
            try
            {
                if (CheckSignatures(inputFilePath, outputFilePath)) return;

                using (PdfReader pdfReader = new PdfReader(inputFilePath))
                {
                    pdfReader.SetUnethicalReading(true); // Разрешаем обработку защищенных файлов

                    using (PdfWriter pdfWriter = new PdfWriter(outputFilePath).SetSmartMode(true).SetCompressionLevel(1)) // Smart Copy Mode
                    using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                    {
                        pdfDocument.SetFlushUnusedObjects(true); // Оптимизация

                        // Пройтись по страницам и уменьшить размер изображений
                        for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
                        {
                            PdfPage page = pdfDocument.GetPage(pageNum);
                            PdfResources resources = page.GetResources();
                            PdfDictionary xObjects = resources.GetResource(PdfName.XObject);

                            if (xObjects != null)
                            {
                                foreach (PdfName key in xObjects.KeySet())
                                {
                                    PdfStream stream = xObjects.GetAsStream(key);
                                    // Пример простой компрессии (можно улучшить с использованием ImageDataFactory)
                                    stream?.SetCompressionLevel(1);
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
                if (CheckSignatures(inputFilePath, outputFilePath)) return;

                using (PdfReader pdfReader = new PdfReader(inputFilePath))
                {
                    pdfReader.SetUnethicalReading(true); // Разрешаем обработку защищенных файлов

                    using (PdfWriter pdfWriter = new PdfWriter(outputFilePath).SetSmartMode(true).SetCompressionLevel(1)) // Максимальная компрессия без потери качества
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
                                foreach (PdfName key in xObjects.KeySet())
                                {
                                    PdfStream stream = xObjects.GetAsStream(key);
                                    if (stream != null && stream.Get(PdfName.Subtype).Equals(PdfName.Image))
                                    {
                                        byte[] bytes = stream.GetBytes();
                                        ImageData img = ImageDataFactory.Create(bytes);

                                        PdfImageXObject imgObject = new PdfImageXObject(img);
                                        xObjects.Put(key, imgObject.GetPdfObject());
                                    }
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

        private bool CheckSignatures(string inputFilePath, string outputFilePath)
        {
            using (PdfReader reader = new PdfReader(inputFilePath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                SignatureUtil signUtil = new SignatureUtil(pdfDoc);
                if (signUtil.GetSignatureNames().Count > 0) // Если есть подписи, вернуть true
                {
                    File.Copy(inputFilePath, outputFilePath, true); //Скопировать не изменяя
                    return true;
                }
                return false;
            }
        }
    }

    public class PdfImageChecker : IEventListener
    {
        private bool _hasImages = false;

        public bool HasImages => _hasImages;

        public void EventOccurred(IEventData data, EventType type)
        {
            if (type == EventType.RENDER_IMAGE)
            {
                _hasImages = true;
            }
        }

        public ICollection<EventType> GetSupportedEvents()
        {
            return new HashSet<EventType> { EventType.RENDER_IMAGE };
        }

        public static bool ContainsImages(string filePath)
        {
            using (PdfReader reader = new PdfReader(filePath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                PdfImageChecker listener = new PdfImageChecker();

                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    PdfCanvasProcessor parser = new PdfCanvasProcessor(listener);
                    parser.ProcessPageContent(pdfDoc.GetPage(i));

                    if (listener.HasImages)
                        return true; // Если нашли хотя бы одно изображение - выходим
                }
            }
            return false;
        }
    }
}