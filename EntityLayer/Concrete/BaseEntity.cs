using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public abstract class BaseEntity
    {
            public int Id { get; set; }

            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedDate { get; set; }

            public bool IsDeleted { get; set; }
        }
    }
}
