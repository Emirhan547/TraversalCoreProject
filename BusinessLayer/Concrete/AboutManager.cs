using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.AboutDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AboutManager: IAboutService
    {
        private readonly IAboutDal _aboutDal;
        private readonly IUowDal _uowDal;

        public AboutManager(IUowDal uowDal, IAboutDal aboutDal)
        {
            _uowDal = uowDal;
            _aboutDal = aboutDal;
        }

        public async Task AddAsync(CreateAboutDto dto)
        {
            var entity = dto.Adapt<About>();
            await _aboutDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity=await _aboutDal.GetByIdAsync(id);
             _aboutDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultAboutDto?> GetByIdAsync(int id)
        {
            var entity = await _aboutDal.GetByIdAsync(id);
            return entity?.Adapt<ResultAboutDto>();
        }

        public async Task<IReadOnlyList<ResultAboutDto>> GetListAsync()
        {
            var entity = await _aboutDal.GetListAsync();
            return entity.Adapt<IReadOnlyList<ResultAboutDto>>();
        }

        public async Task UpdateAsync(UpdateAboutDto dto)
        {
            var entity = await _aboutDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new Exception("About bulunamadı");

            dto.Adapt(entity);
             _aboutDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }
    }
}
