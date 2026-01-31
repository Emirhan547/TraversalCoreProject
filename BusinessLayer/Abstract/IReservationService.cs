using DTOLayer.DTOs.ReservationDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReservationService
        : IGenericService<ResultReservationDto, CreateReservationDto, UpdateReservationDto>
    {
        Task<IReadOnlyList<ResultReservationDto>> GetListWithReservationByWaitApprovalAsync(int userId);
        Task<IReadOnlyList<ResultReservationDto>> GetListWithReservationByAcceptedAsync(int userId);
        Task<IReadOnlyList<ResultReservationDto>> GetListWithReservationByPreviousAsync(int userId);
    }
}
