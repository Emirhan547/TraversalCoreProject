using BusinessLayer.Abstract;
using DTOLayer.DTOs.AnnouncementDtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class AnnouncementController : Controller
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _announcementService.GetListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(CreateAnnouncementDto model)
        {
            model.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            await _announcementService.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            await _announcementService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAnnouncement(int id)
        {
            var values = await _announcementService.GetByIdAsync(id);
            if (values is null)
            {
                return NotFound();
            }
            var model = values.Adapt<UpdateAnnouncementDto>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncementDto model)
        {
            if (ModelState.IsValid)
            {
                model.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                await _announcementService.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
