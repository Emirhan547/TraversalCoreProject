using BusinessLayer.Abstract.AbstractUow;
using DTOLayer.DTOs.AccountDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AccountTransferDto model)
        {
            var valueSender = await _accountService.GetByIdAsync(model.SenderID);
            var valueReceiver = await _accountService.GetByIdAsync(model.ReceiverID);
            if (valueSender == null || valueReceiver == null)
            {
                return View(model);
            }
            var modifiedAccounts = new List<UpdateAccountDto>
            {
                new UpdateAccountDto
                {
                    Id = valueSender.Id,
                    Name = valueSender.Name,
                    Balance = valueSender.Balance - model.Amount
                },
                new UpdateAccountDto
                {
                    Id = valueReceiver.Id,
                    Name = valueReceiver.Name,
                    Balance = valueReceiver.Balance + model.Amount
                }
                };
            await _accountService.UpdateRangeAsync(modifiedAccounts);
            return View();
        }

    }
}