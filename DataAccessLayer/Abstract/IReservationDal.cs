using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal : IGenericDal<Reservation>
    {
        Task<IReadOnlyList<Reservation>> GetListWithReservationByWaitApprovalAsync(int userId);
        Task<IReadOnlyList<Reservation>> GetListWithReservationByAcceptedAsync(int userId);
        Task<IReadOnlyList<Reservation>> GetListWithReservationByPreviousAsync(int userId);
    }
}
