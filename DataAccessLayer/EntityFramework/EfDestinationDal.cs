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
    public class EfDestinationDal : GenericRepository<Destination>, IDestinationDal
    {
        private readonly Context _context;

        public EfDestinationDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Destination?> GetDestinationWithGuideAsync(int id)
        {
            return await _context.Destinations
                .Include(x => x.Guide)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Destination>> GetLast4DestinationsAsync()
        {
            return await _context.Destinations
                .OrderByDescending(x => x.Id)
                .Take(4)
                .ToListAsync();
        }
    }
}
