using System;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
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
            XImage logo = XImage.FromFile("C:/Users/nickz/source/repos/Tussentijds Project/Tussentijds Project/images/logo.png");
            XPen pen = new XPen(XColors.Black, 2);

            List<OrderDetail> details = new List<OrderDetail>();
            Order order;
            Customer customer;

            using (var ctx = new OrderManagerContext())
            {
                details = ctx.OrderDetails.Where(od => od.Order.OrderId == orderId).ToList();
                order = ctx.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                customer = ctx.Customers.Where(c => c.CustomerId == order.Customer.CustomerId).FirstOrDefault();

                for (int i = 0; i <= details.Count - 1; i++)
                {
                    graph.DrawString($"{details[i].Product.Name}", font, XBrushes.Black, new XRect(10, 420 + (i * 20), 100, 0));
                    graph.DrawString($"{details[i].Quantity}", font, XBrushes.Black, new XRect(340, 420 + (i * 20), 100, 0));
                    graph.DrawString($"€ {string.Format("{0:0.00}", details[i].Product.UnitPrice * details[i].Quantity)}", font, XBrushes.Black, new XRect(440, 420 + (i * 20), 100, 0));
                    graph.DrawLine(pen, 10, 425 + (i * 20), 580, 425 + (i * 20));
                }
            }

            graph.DrawImage(logo, new XRect(5, 0, 100, 100));
            graph.DrawString("NZ Shipping BVBA", font, XBrushes.Black, new XRect(10, 105, 100, 0));
            graph.DrawString("Kazerneweg 71", font, XBrushes.Black, new XRect(10, 115, 100, 0));
            graph.DrawString("2950 Kapellen", font, XBrushes.Black, new XRect(10, 125, 100, 0));

            graph.DrawString($"Klant: {order.Customer.Name}", font, XBrushes.Black, new XRect(10, 200, 100, 0));
            graph.DrawString($"ID Bestelling: {order.OrderId}", font, XBrushes.Black, new XRect(10, 220, 100, 0));
            graph.DrawString($"Besteldatum: {order.OrderDate.ToString("dd/MM/yyyy")}", font, XBrushes.Black, new XRect(10, 240, 100, 0));

            graph.DrawString("Naam Product", font, XBrushes.Black, new XRect(10, 390, 100, 0));
            graph.DrawString("Aantal", font, XBrushes.Black, new XRect(340, 390, 100, 0));
            graph.DrawString("Prijs", font, XBrushes.Black, new XRect(440, 390, 100, 0));                       

            graph.DrawString($"Totale Prijs (excl. BTW): € {string.Format("{0:0.00}", TotalePrijs(details))}", font, XBrushes.Black, new XRect(10, 420 + (details.Count * 20), 100, 0));
            graph.DrawString("BTW: 21%", font, XBrushes.Black, new XRect(10, 420 + (details.Count * 20) + 20, 100, 0));
            graph.DrawString($"TOTALE PRIJS (incl. BTW): € {string.Format("{0:0.00}", TotalePrijsBtw(details))}", font, XBrushes.Black, new XRect(10, 420 + (details.Count * 20) + 40, 100, 0));

            string filename = "C:/Users/nickz/source/repos/Tussentijds Project/Tussentijds Project/docs/factuur.pdf";
            doc.Save(filename);
            Process.Start(filename);


        }
        private double TotalePrijs(List<OrderDetail> details)
        {
            double prijs = 0;

            foreach (var item in details)
            {
                prijs += item.Product.UnitPrice * item.Quantity;
            }

            return prijs;
        }

        private double TotalePrijsBtw(List<OrderDetail> details)
        {
            double prijs = 0;            

            foreach (var item in details)
            {
                double btw = item.Product.UnitPrice * 0.21;
                prijs += (item.Product.UnitPrice + btw) * item.Quantity;
            }

            return prijs;
        }
    }
}

