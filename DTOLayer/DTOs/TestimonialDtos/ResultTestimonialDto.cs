using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.TestimonialDtos
{
    public class ResultTestimonialDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Client { get; set; }
        public string Comment { get; set; }
        public string ClientImage { get; set; }
        public bool Status { get; set; }
    }
}
