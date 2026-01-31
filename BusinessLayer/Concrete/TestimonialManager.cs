using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.TestimonialDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TestimonialManager: ITestimonialService
    {
       private readonly ITestimonialDal _testimonialDal;
        private readonly IUowDal _uowDal;

        public TestimonialManager(IUowDal uowDal, ITestimonialDal testimonialDal)
        {
            _uowDal = uowDal;
            _testimonialDal = testimonialDal;
        }

        public async Task AddAsync(CreateTestimonialDto dto)
        {
            var entities = dto.Adapt<Testimonial>();
            await _testimonialDal.AddAsync(entities);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var values=await _testimonialDal.GetByIdAsync(id);
             _testimonialDal.DeleteAsync(values);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultTestimonialDto?> GetByIdAsync(int id)
        {
            var values = await _testimonialDal.GetByIdAsync(id);
            return values?.Adapt<ResultTestimonialDto?>();
        }

        public async Task<IReadOnlyList<ResultTestimonialDto>> GetListAsync()
        {
            var values=await _testimonialDal.GetListAsync();
            return values.Adapt<IReadOnlyList<ResultTestimonialDto>>();
        }

        public async Task UpdateAsync(UpdateTestimonialDto dto)
        {
            var values=await _testimonialDal.GetByIdAsync(dto.Id);
            dto.Adapt(values);
            _testimonialDal.UpdateAsync(values);
            await _uowDal.SaveChangesAsync();
        }
    }
}
