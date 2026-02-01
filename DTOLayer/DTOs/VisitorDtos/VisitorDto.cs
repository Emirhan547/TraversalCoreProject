using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.VisitorDtos
{
    public class VisitorDto
    {
        public int VisitorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
