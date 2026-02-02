using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfContactUsDal : GenericRepository<ContactUs>, IContactUsDal
    {
        private readonly Context _context;

        public EfContactUsDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ContactUs>> GetListByStatusAsync(bool status)
        {
            return await _context.ContactUses
               .Where(x => x.MessageStatus == status)
                .ToListAsync();
        }
    }
}
