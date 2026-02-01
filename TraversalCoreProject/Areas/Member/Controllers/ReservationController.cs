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
        private readonly IAuthService _authService;

        public ReservationController(IAuthService authService, IReservationService reservationService, IDestinationService destinationService)
        {
            _authService = authService;
            _reservationService = reservationService;
            _destinationService = destinationService;
        }

        public async Task <IActionResult> MyCurrentReservation()
    {
            if (User.Identity?.Name is null)
            {
                return View();
            }

            var values = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (values == null)
            {
                return View();
            }

            var valuesList = await _reservationService.GetListWithReservationByAcceptedAsync(values.Id);
            return View(valuesList);
        }
    public async Task <IActionResult> MyOldReservation()
    {
            if (User.Identity?.Name is null)
            {
                return View();
            }

            var values = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (values == null)
            {
                return View();
            }

            var valuesList = await _reservationService.GetListWithReservationByPreviousAsync(values.Id);
            return View(valuesList);
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            if (User.Identity?.Name is null)
            {
                return View();
            }

            var values = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (values == null)
            {
                return View();
            }

            var valuesList = await _reservationService.GetListWithReservationByWaitApprovalAsync(values.Id);
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
            if (User.Identity?.Name is null)
            {
                return View(reservation);
            }

            var user = await _authService.GetByUserNameAsync(User.Identity.Name);
            if (user == null)
            {
                return View(reservation);
            }
            reservation.AppUserId = user.Id;
            reservation.Status = "Onay Bekliyor";
            reservation.ReservationDate = DateTime.Now;

            await _reservationService.AddAsync(reservation);
            return RedirectToAction("MyCurrentReservation");
        }
    }
}
