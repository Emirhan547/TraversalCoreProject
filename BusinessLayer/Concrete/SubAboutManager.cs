using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.SubAboutDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class SubAboutManager: ISubAboutService
    {
        private readonly ISubAboutDal _subAboutDal;
        private readonly IUowDal _uowDal;

        public SubAboutManager(IUowDal uowDal, ISubAboutDal subAboutDal)
        {
            _uowDal = uowDal;
            _subAboutDal = subAboutDal;
        }

        public async Task AddAsync(CreateSubAboutDto dto)
        {
            var entity = dto.Adapt<SubAbout>();
            await _subAboutDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
           var values=await _subAboutDal.GetByIdAsync(id);
           _subAboutDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultSubAboutDto?> GetByIdAsync(int id)
        {
            var values = await _subAboutDal.GetByIdAsync(id);
            return values?.Adapt<ResultSubAboutDto>();
        }

        public async Task<IReadOnlyList<ResultSubAboutDto>> GetListAsync()
        {
            var values=await _subAboutDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultSubAboutDto>>();
        }

        public async Task UpdateAsync(UpdateSubAboutDto dto)
        {
            var values=await _subAboutDal.GetByIdAsync(dto.Id);
            dto.Adapt(values);
           _subAboutDal.UpdateAsync(values);
            await _uowDal.SaveChangesAsync();
        }
    }
}
