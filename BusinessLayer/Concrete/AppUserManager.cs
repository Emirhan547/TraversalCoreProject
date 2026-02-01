using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AppUserManager : IAppUserService
    {
       private readonly IAppUserDal _appUserDal;
        private readonly IUowDal _uowDal;

        public AppUserManager(IUowDal uowDal, IAppUserDal appUserDal)
        {
            _uowDal = uowDal;
            _appUserDal = appUserDal;
        }

        public async Task AddAsync(CreateAppUserDto dto)
        {
            var entity = dto.Adapt<AppUser>();
            await _appUserDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var values=await _appUserDal.GetByIdAsync(id);
            _appUserDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultAppUserDto?> GetByIdAsync(int id)
        {
            var values = await _appUserDal.GetByIdAsync(id);
            return values?.Adapt<ResultAppUserDto?>();
        }

        public async Task<IReadOnlyList<ResultAppUserDto>> GetListAsync()
        {
            var values = await _appUserDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultAppUserDto>>();
        }

        public async Task UpdateAsync(UpdateAppUserDto dto)
        {
            var values=await _appUserDal.GetByIdAsync(dto.Id);
            dto.Adapt(values);
            _appUserDal.UpdateAsync(values);
            await _uowDal.SaveChangesAsync();
        }
    }
}
