using DTOLayer.DTOs.ReservationDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReservationService
        : IGenericService<ResultReservationDto, CreateReservationDto, UpdateReservationDto>
    {
        Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByWaitApprovalAsync(int userId);
        Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByAcceptedAsync(int userId);
        Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByPreviousAsync(int userId);
    }
}
