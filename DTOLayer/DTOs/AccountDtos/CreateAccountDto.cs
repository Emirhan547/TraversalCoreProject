using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.AccountDtos
{
    public class CreateAccountDto
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
