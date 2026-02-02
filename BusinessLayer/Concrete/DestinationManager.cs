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

        public async Task AddAsync(DestinationInputDto dto)
        {
            var entity = dto.Adapt<Destination>();
            entity.Status = true;
            entity.Date = DateTime.Now;
            await _destinationDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task UpdateAsync(DestinationInputDto dto)
        {
            var entity = await _destinationDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Destination not found");

            dto.Adapt(entity);
             _destinationDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _destinationDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Destination not found");

             _destinationDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

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
