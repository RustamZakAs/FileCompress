using FileCompress.Services;
using iText.Layout.Splitting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCompress
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.tbFolder.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnStartJob_Click(object sender, EventArgs e)
        {
            string[] patterns = this.tbExtensions.Text.Split(",");
            List<string> files = new List<string>();
            foreach (string pattern in patterns)
            {
                files.AddRange(Directory.GetFiles(this.tbFolder.Text, pattern.Trim()));
            }
            files = files.Distinct().ToList();
            this.pbFiles.Maximum = files?.Count ?? 0;
            foreach (string file in files ?? new List<string>())
            {
                if (this.pbFiles.Value < this.pbFiles.Maximum)
                    this.pbFiles.Value++;
                string inputPdfPath = file;
                string folderPath = Path.GetDirectoryName(file);
                string newFolderName = "New";
                string newFolderPath = folderPath;
                if (!this.cbInOldFolder.Checked)
                {
                    newFolderPath = Path.Combine(folderPath, newFolderName);
                    if (!Directory.Exists(newFolderPath))
                        Directory.CreateDirectory(newFolderPath);
                }

                string fileName = Path.GetFileName(file);
                string outputPdfPath = Path.Combine(newFolderPath, fileName);

                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".pdf":
                        PdfCompressionService pdfCompressionService = new PdfCompressionService();
                        //pdfCompressionService.CompressPdf(inputPdfPath, outputPdfPath);
                        pdfCompressionService.CompressImagesInPdf(inputPdfPath, outputPdfPath);
                        break;
                    case ".jpg":
                        ImageCompressionService imageCompressionService = new ImageCompressionService();
                        imageCompressionService.CompressImage(inputPdfPath, outputPdfPath, 80); // 80% качество (оптимально)
                        break;
                    default:
                        break;
                }
            }
            this.pbFiles.Value = 0;
        }
    }
}