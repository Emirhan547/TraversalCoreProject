using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.NewsLetterDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsLetterManager:INewsLetterService
    {
        private readonly INewsLetterDal _newsletterDal;
        private readonly IUowDal _uowDal;

        public NewsLetterManager(IUowDal uowDal, INewsLetterDal newsletterDal)
        {
            _uowDal = uowDal;
            _newsletterDal = newsletterDal;
        }

        public async Task AddAsync(CreateNewsLetterDto dto)
        {
            var entity = dto.Adapt<NewsLetter>();
           await _newsletterDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var values=await _newsletterDal.GetByIdAsync(id);
            _newsletterDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultNewsLetterDto?> GetByIdAsync(int id)
        {
            var values = await _newsletterDal.GetByIdAsync(id);
            return values?.Adapt<ResultNewsLetterDto?>();
        }

        public async Task<IReadOnlyList<ResultNewsLetterDto>> GetListAsync()
        {
            var values=await _newsletterDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultNewsLetterDto>>();
        }

        public async Task UpdateAsync(UpdateNewsLetterDto dto)
        {
           var values=await _newsletterDal.GetByIdAsync(dto.Id);
            dto.Adapt(values);
             _newsletterDal.UpdateAsync(values);
            await _uowDal.SaveChangesAsync();
        }
    }
}
