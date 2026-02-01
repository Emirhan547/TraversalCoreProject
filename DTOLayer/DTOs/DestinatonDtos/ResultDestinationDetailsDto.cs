using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.DestinatonDtos
{
    public class ResultDestinationDetailsDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string City { get; set; }
        public string DayNight { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
        public string CoverImage { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string Image2 { get; set; }
        public DateTime Date { get; set; }
        public int GuideId { get; set; }
        public string GuideName { get; set; }
        public string GuideDescription { get; set; }
        public string GuideDescription2 { get; set; }
        public string GuideImage { get; set; }
    }
}
