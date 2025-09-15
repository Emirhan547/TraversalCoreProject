using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var valuesList = _reservationService.GetListWithReservationByAccepted(values.Id);
            return View(valuesList);
        }
    public async Task <IActionResult> MyOldReservation()
    {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList = _reservationService.GetListWithReservationByPrevious(values.Id);
            return View(valuesList);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var values =await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList= _reservationService.GetListWithReservationByWaitApproval(values.Id);
            return View(valuesList);
        }   
        [HttpGet] 
        public IActionResult NewReservation()
        {
            List<SelectListItem> values = (from x in _destinationService.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.City,
                                              Value = x.DestinationID.ToString()
                                          }).ToList();
            ViewBag.v =values;
            return View();
        }
    [HttpPost]
    public IActionResult NewReservation(Reservation p)
    {
            p.AppUserId = 1;
            p.Status = "Onay Bekliyor";
            _reservationService.TAdd(p);
            return RedirectToAction("MyCurrentReservation");
    }
}
}
