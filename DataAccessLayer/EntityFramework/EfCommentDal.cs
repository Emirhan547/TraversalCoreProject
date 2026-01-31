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
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly Context _context;

        public EfCommentDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Comment>> GetByDestinationIdAsync(int destinationId)
        {
            return await _context.Comments
                .Where(x => x.DestinationId == destinationId)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsWithDestinationAsync()
        {
            return await _context.Comments
                .Include(x => x.Destination)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsWithDestinationAndUserAsync(int userId)
        {
            return await _context.Comments
                .Where(x => x.AppUserId == userId)   // ✅ user filtre doğru alan olmalı
                .Include(x => x.Destination)
                .Include(x => x.AppUser)
                .ToListAsync();
        }
    }
}
