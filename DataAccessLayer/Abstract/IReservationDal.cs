using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal : IGenericDal<Reservation>
    {
        Task<IReadOnlyList<Reservation>> GetListWithReservationByStatusAsync(int userId, string status);
    }
}
