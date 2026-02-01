using DTOLayer.DTOs.AccountDtos;


namespace BusinessLayer.Abstract.AbstractUow
{
    public interface IAccountService : IGenericUowService<CreateAccountDto, UpdateAccountDto, ResultAccountDto>
    {
    }
}
