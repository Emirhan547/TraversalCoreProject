using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.FeatureDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FeatureManager:IFeatureService
    {
       private readonly IFeatureDal _featureDal;
        private readonly IUowDal _uowDal;

        public FeatureManager(IUowDal uowDal, IFeatureDal featureDal)
        {
            _uowDal = uowDal;
            _featureDal = featureDal;
        }

        public async Task AddAsync(CreateFeatureDto dto)
        {
            var entity = dto.Adapt<Feature>();
            await _featureDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var values=await _featureDal.GetByIdAsync(id);
            _featureDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultFeatureDto?> GetByIdAsync(int id)
        {
            var values = await _featureDal.GetByIdAsync(id);
            return  _featureDal.Adapt<ResultFeatureDto>();
        }

        public async Task<IReadOnlyList<ResultFeatureDto>> GetListAsync()
        {
            var values=await _featureDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultFeatureDto>>();
        }

        public async Task UpdateAsync(UpdateFeatureDto dto)
        {
           var values=await _featureDal.GetByIdAsync(dto.Id);
            dto.Adapt(values);
            _featureDal.UpdateAsync(values);
            await _uowDal.SaveChangesAsync();
        }
    }
}
