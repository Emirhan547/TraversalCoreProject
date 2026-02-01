using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DTOLayer.DTOs.DestinatonDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly IAuthService _authService;

        
        public DestinationController(IAuthService authService, IDestinationService destinationService)
        {
            _authService = authService;
            _destinationService = destinationService;
        }
        public async Task<IActionResult> Index()
        {
            var values =await _destinationService.GetListAsync();
            return View(values);
        }

        //[HttpGet]
        public async Task<IActionResult> DestinationDetails(int id)
        {
            ViewBag.i = id;
            ViewBag.destID = id;
            if (User.Identity?.Name is not null)
            {
                var value = await _authService.GetByUserNameAsync(User.Identity.Name);
                if (value != null)
                {
                    ViewBag.userID = value.Id;
                }
            }
            var values = await _destinationService.GetDestinationWithGuideAsync(id);
            return View(values);
        }
        //[HttpPost]
        //public IActionResult DestinationDetails(Destination p)
        //{
        //    return View();
        //}
    }
}
