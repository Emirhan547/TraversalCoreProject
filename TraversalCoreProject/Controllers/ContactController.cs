using BusinessLayer.Abstract;

using DTOLayer.DTOs.ContactUsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TraversalCoreProje.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IContactUsService _contactUsService;

        public ContactController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ContactMessageInputDto model)
        {
            if (ModelState.IsValid)
            {
                await _contactUsService.AddAsync(model);

                return RedirectToAction("Index", "Default");
            }
            return View(model);
        }
    }
}
