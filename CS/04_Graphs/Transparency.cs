using System;
using System.Drawing;
using System.Windows.Forms;
using Spire.Pdf;
using Spire.Pdf.Graphics;


namespace Transparency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create a pdf document
            PdfDocument doc = new PdfDocument();

            //Create one section
            PdfSection section = doc.Sections.Add();

            //Load an image
            PdfImage image = PdfImage.FromFile(@"..\..\..\..\..\..\Data\ChartImage.png");
            float imageWidth = image.PhysicalDimension.Width / 2;
            float imageHeight = image.PhysicalDimension.Height / 2;
            foreach (PdfBlendMode mode in Enum.GetValues(typeof(PdfBlendMode)))
            {
                PdfPageBase page = section.Pages.Add();
                float pageWidth = page.Canvas.ClientSize.Width;
                float y = 0;

                //Title
                y = y + 15;
                PdfBrush brush = new PdfSolidBrush(Color.OrangeRed);
                PdfTrueTypeFont font = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
                PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Center);
                String text = String.Format("Transparency Blend Mode: {0}", mode);
                page.Canvas.DrawString(text, font, brush, pageWidth / 2, y, format);
                SizeF size = font.MeasureString(text, format);
                y = y + size.Height + 25;

                page.Canvas.DrawImage(image, 0, y, imageWidth, imageHeight);
                page.Canvas.Save();
                float d = (page.Canvas.ClientSize.Width - imageWidth) / 5;
                float x = d;
                y = y + d / 2+40;
                for (int i = 0; i < 5; i++)
                {
                    float alpha = 1.0f / 6 * (5 - i);
                    page.Canvas.SetTransparency(alpha, alpha, mode);
                    page.Canvas.DrawImage(image, x, y, imageWidth, imageHeight);
                    x = x + d;
                    y = y + d / 2+ 40;
                }
                page.Canvas.Restore();
            }

            //Save the document
            doc.SaveToFile("Transparency.pdf");
            doc.Close();

            //Launch the Pdf file
            PDFDocumentViewer("Transparency.pdf");
        }

        private void PDFDocumentViewer(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch { }
        }

    }
}
