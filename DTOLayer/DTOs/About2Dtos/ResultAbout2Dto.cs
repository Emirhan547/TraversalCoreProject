using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.About2Dtos
{
    public class ResultAbout2Dto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Description1 { get; set; }
        public string Image { get; set; }
    }
}
