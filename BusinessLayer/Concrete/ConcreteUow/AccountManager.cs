using BusinessLayer.Abstract.AbstractUow;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.AccountDtos;
using EntityLayer.Concrete;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.ConcreteUow
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDal _accountDal;
        private readonly IUowDal _uowDal;

        public AccountManager(IAccountDal accountDal, IUowDal uowDal)
        {
            _accountDal = accountDal;
            _uowDal = uowDal;
        }

        public async Task AddAsync(CreateAccountDto dto)
        {
            var entity = dto.Adapt<Account>();
            _accountDal.Insert(entity);
            await _uowDal.SaveChangesAsync();
        }

        public Task<ResultAccountDto?> GetByIdAsync(int id)
        {
            var entity = _accountDal.GetByID(id);
            return Task.FromResult(entity?.Adapt<ResultAccountDto?>());
        }

        public async Task UpdateAsync(UpdateAccountDto dto)
        {
            var entity = _accountDal.GetByID(dto.Id);
            if (entity == null)
            {
                return;
            }

            dto.Adapt(entity);
            _accountDal.Update(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<UpdateAccountDto> dtos)
        {
            var entities = new List<Account>();
            foreach (var dto in dtos)
            {
                var entity = _accountDal.GetByID(dto.Id);
                if (entity == null)
                {
                    continue;
                }

                dto.Adapt(entity);
                entities.Add(entity);
            }

            if (entities.Count == 0)
            {
                return;
            }

            _accountDal.MultiUpdate(entities);
            await _uowDal.SaveChangesAsync();
        }
    }
}
