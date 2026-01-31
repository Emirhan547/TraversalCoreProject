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
    public class EfReservationDal : GenericRepository<Reservation>, IReservationDal
    {
        private readonly Context _context;

        public EfReservationDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Reservation>> GetListWithReservationByWaitApprovalAsync(int userId)
        {
            return await _context.Reservations
                .Include(x => x.Destination)
                .Where(x => x.Status == "Onay Bekliyor" && x.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Reservation>> GetListWithReservationByAcceptedAsync(int userId)
        {
            return await _context.Reservations
                .Include(x => x.Destination)
                .Where(x => x.Status == "Onaylandı" && x.AppUserId == userId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Reservation>> GetListWithReservationByPreviousAsync(int userId)
        {
            return await _context.Reservations
                .Include(x => x.Destination)
                .Where(x => x.Status == "Geçmiş Rezervasyon" && x.AppUserId == userId)
                .ToListAsync();
        }
    }
}
