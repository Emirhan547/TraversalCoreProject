using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.GuideDtos;
using EntityLayer.Concrete;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GuideManager : IGuideService
    {
        private readonly IGuideDal _guideDal;
        private readonly IUowDal _uowDal;

        public GuideManager(IUowDal uowDal, IGuideDal guideDal)
        {
            _uowDal = uowDal;
            _guideDal = guideDal;
        }

       

        public async Task AddAsync(CreateGuideDto dto)
        {
            var entity = dto.Adapt<Guide>();
            await _guideDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

      

        public async Task UpdateAsync(UpdateGuideDto dto)
        {
            var entity = await _guideDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Guide not found");

            dto.Adapt(entity);
             _guideDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }
   
        public async Task DeleteAsync(int id)
        {
            var entity = await _guideDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Guide not found");

             _guideDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }
        

        public async Task<ResultGuideDto?> GetByIdAsync(int id)
        {
            var entity = await _guideDal.GetByIdAsync(id);
            return entity?.Adapt<ResultGuideDto>();
        }

        public async Task<IReadOnlyList<ResultGuideDto>> GetListAsync()
        {
            var entities = await _guideDal.GetListAsync();
            return entities.Adapt<IReadOnlyList<ResultGuideDto>>();
        }

       

        public async Task ChangeGuideStatusToTrueAsync(int id)
        {
            var entity = await _guideDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Guide not found");

            entity.Status = true;
             _guideDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task ChangeGuideStatusToFalseAsync(int id)
        {
            var entity = await _guideDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Guide not found");

            entity.Status = false;
             _guideDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }
    }
}
