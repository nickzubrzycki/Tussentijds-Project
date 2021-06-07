using System;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tussentijds_Project
{
    class CreatePDF
    {
        public void NewPDF(int orderId)
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Calibri", 12, XFontStyle.Bold);
        }
    }
}
