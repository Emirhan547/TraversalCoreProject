using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.ReservationDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Member.Controllers
{ 
    [Area("Member")]
public class ReservationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(UserManager<AppUser> userManager, IReservationService reservationService, IDestinationService destinationService)
        {
            _userManager = userManager;
            _reservationService = reservationService;
            _destinationService = destinationService;
        }

        public async Task <IActionResult> MyCurrentReservation()
    {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList =await _reservationService.GetListWithReservationByAcceptedAsync(values.Id);
            return View(valuesList);
        }
    public async Task <IActionResult> MyOldReservation()
    {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList = _reservationService.GetListWithReservationByPreviousAsync(values.Id);
            return View(valuesList);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var values =await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList= _reservationService.GetListWithReservationByWaitApprovalAsync(values.Id);
            return View(valuesList);
        }   
        [HttpGet] 
        public async Task<IActionResult> NewReservation()
        {
            var destinations = await _destinationService.GetListAsync();
            List<SelectListItem> values = (from x in destinations
                                           select new SelectListItem
                                          {
                                              Text = x.City,
                                              Value = x.Id.ToString()
                                          }).ToList();
            ViewBag.v =values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewReservation(CreateReservationDto reservation)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            reservation.AppUserId = user.Id;
            reservation.Status = "Onay Bekliyor";
            reservation.ReservationDate = DateTime.Now;

            await _reservationService.AddAsync(reservation);
            return RedirectToAction("MyCurrentReservation");
        }
    }
}
