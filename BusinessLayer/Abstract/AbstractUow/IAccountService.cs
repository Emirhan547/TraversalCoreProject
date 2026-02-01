using DTOLayer.DTOs.AccountDtos;


namespace BusinessLayer.Abstract.AbstractUow
{
    public interface IAccountService : IGenericUowService<ResultAccountDto, CreateAccountDto, UpdateAccountDto>
    {
    }
}
