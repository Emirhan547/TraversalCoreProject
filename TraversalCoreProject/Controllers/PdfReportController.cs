using iTextSharp.text;          // Document, PageSize, Paragraph buradan gelir
using iTextSharp.text.pdf;

using Microsoft.AspNetCore.Mvc;

namespace TraversalCoreProject.Controllers
{
    public class PdfReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StaticPdfReport()
        {
             string path=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/"+ "dosya1.pdf");
            var stream= new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();
            Paragraph paragraph = new Paragraph("Traversal Core Pdf Raporlama");
            document.Add(paragraph);
            document.Close();
            return File("/pdfreports/dosya1.pdf", "application/pdf","dosya1.pdf" );

        }
        public IActionResult StaticPdfReport2()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/" + "dosya2.pdf");
            var stream = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();
            Paragraph paragraph = new Paragraph("Traversal Core Pdf Raporlama 2");
            document.Add(paragraph);
            PdfPTable pdfPTable = new PdfPTable(3);
            pdfPTable.AddCell("Misafir Adı");
            pdfPTable.AddCell("Misafir SoyAdı");
            pdfPTable.AddCell("Misafir TC");

            pdfPTable.AddCell("Emirhan");
            pdfPTable.AddCell("Hacıoğlu");
            pdfPTable.AddCell("11111111111");

            pdfPTable.AddCell("Ahmet");
            pdfPTable.AddCell("Yıldız");
            pdfPTable.AddCell("11111111111");

            pdfPTable.AddCell("Ayşe");
            pdfPTable.AddCell("Bal");
            pdfPTable.AddCell("11111111111");

            document.Add(pdfPTable);
            document.Close();
            return File("/pdfreports/dosya2.pdf", "application/pdf", "dosya2.pdf");
        }

    }
}
