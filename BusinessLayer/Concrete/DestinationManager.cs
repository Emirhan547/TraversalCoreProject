using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.DestinatonDtos;
using EntityLayer.Concrete;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DestinationManager : IDestinationService
    {
        private readonly IDestinationDal _destinationDal;
        private readonly IUowDal _uowDal;

        public DestinationManager(IDestinationDal destinationDal, IUowDal uowDal)
        {
            _destinationDal = destinationDal;
            _uowDal = uowDal;
        }

        // -------------------- CREATE --------------------

        public async Task AddAsync(CreateDestinationDto dto)
        {
            var entity = dto.Adapt<Destination>();
            await _destinationDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        // -------------------- UPDATE --------------------

        public async Task UpdateAsync(UpdateDestinationDto dto)
        {
            var entity = await _destinationDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Destination not found");

            dto.Adapt(entity);
             _destinationDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        // -------------------- DELETE --------------------

        public async Task DeleteAsync(int id)
        {
            var entity = await _destinationDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Destination not found");

             _destinationDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        // -------------------- GET --------------------

        public async Task<ResultDestinationDto?> GetByIdAsync(int id)
        {
            var entity = await _destinationDal.GetByIdAsync(id);
            return entity?.Adapt<ResultDestinationDto>();
        }

        public async Task<IReadOnlyList<ResultDestinationDto>> GetListAsync()
        {
            var entities = await _destinationDal.GetListAsync();
            return entities.Adapt<IReadOnlyList<ResultDestinationDto>>();
        }

        // -------------------- CUSTOM --------------------

        public async Task<ResultDestinationDto?> GetDestinationWithGuideAsync(int id)
        {
            var entity = await _destinationDal.GetDestinationWithGuideAsync(id);
            return entity?.Adapt<ResultDestinationDto>();
        }

        public async Task<IReadOnlyList<ResultDestinationDto>> GetLast4DestinationsAsync()
        {
            var entities = await _destinationDal.GetLast4DestinationsAsync();
            return entities.Adapt<IReadOnlyList<ResultDestinationDto>>();
        }
    }
}
