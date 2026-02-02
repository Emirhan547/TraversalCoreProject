using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.ReservationDtos;
using EntityLayer.Concrete;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ReservationManager : IReservationService
    {
        private readonly IReservationDal _reservationDal;
        private readonly IUowDal _uowDal;

        public ReservationManager(IUowDal uowDal, IReservationDal reservationDal)
        {
            _uowDal = uowDal;
            _reservationDal = reservationDal;
        }


        public async Task AddAsync(CreateReservationDto dto)
        {
            var entity = dto.Adapt<Reservation>();
            await _reservationDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }


        public async Task UpdateAsync(UpdateReservationDto dto)
        {
            var entity = await _reservationDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Reservation not found");

            dto.Adapt(entity);
             _reservationDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _reservationDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Reservation not found");

             _reservationDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultReservationDto?> GetByIdAsync(int id)
        {
            var entity = await _reservationDal.GetByIdAsync(id);
            return entity?.Adapt<ResultReservationDto>();
        }

        public async Task<IReadOnlyList<ResultReservationDto>> GetListAsync()
        {
            var entities = await _reservationDal.GetListAsync();
            return entities.Adapt<IReadOnlyList<ResultReservationDto>>();
        }


        public async Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByWaitApprovalAsync(int userId)
        {
          
            return await GetReservationDtosByStatusAsync(userId, "Onay Bekliyor");
        }

        public async Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByAcceptedAsync(int userId)
        {
            return await GetReservationDtosByStatusAsync(userId, "Onaylandı");
        }

        public async Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetListWithReservationByPreviousAsync(int userId)
        {
            return await GetReservationDtosByStatusAsync(userId, "Geçmiş Rezervasyon");
        }
        private async Task<IReadOnlyList<ResultReservationWithDestinationDto>> GetReservationDtosByStatusAsync(int userId, string status)
        {
            var entities = await _reservationDal.GetListWithReservationByStatusAsync(userId, status);
            return entities.Select(MapToReservationWithDestination).ToList();
        }

        private static ResultReservationWithDestinationDto MapToReservationWithDestination(Reservation entity)
        {
            var dto = entity.Adapt<ResultReservationWithDestinationDto>();
            dto.DestinationCity = entity.Destination?.City;
            return dto;
        }
    }
}
