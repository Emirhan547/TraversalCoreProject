using BusinessLayer.Abstract;
using DTOLayer.DTOs.AppUserDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IReservationService _reservationService;

        public UserController(IAppUserService appUserService, IReservationService reservationService)
        {
            _appUserService = appUserService;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var values = _appUserService.GetListAsync();
            return View(values);
        }
        public async Task<IActionResult> DeleteUser(int id)
        {
           await _appUserService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var values =await _appUserService.GetByIdAsync(id);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UpdateAppUserDto appUser)
        {
          await _appUserService.UpdateAsync(appUser);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> CommentUser(int id)
        {
           await _appUserService.GetListAsync();
            return View();
        }
        public async Task<IActionResult> ReservationUser(int id)
        {
            var values =await _reservationService.GetListWithReservationByAcceptedAsync(id);
            return View(values);
        }
    }
}
