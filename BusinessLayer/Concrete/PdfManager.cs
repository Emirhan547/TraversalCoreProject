using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PdfManager : IPdfService
    {
        public byte[] GenerateDestinationPdfReport()
        {
            throw new NotImplementedException();
        }

        public byte[] GeneratePdfReport<T>(List<T> data) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GenerateReservationPdfReportAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
