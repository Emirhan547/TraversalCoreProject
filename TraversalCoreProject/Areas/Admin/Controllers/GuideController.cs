using BusinessLayer.Abstract;
using DTOLayer.DTOs.GuideDtos;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Route("Admin/Guide")]
    public class GuideController : Controller
    {
        private readonly IGuideService _guideService;

        public GuideController(IGuideService guideService)
        {
            _guideService = guideService;
        }
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values =await _guideService.GetListAsync();
            return View(values);
        }
        [Route("AddGuide")]
        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }
        [Route("AddGuide")]
        [HttpPost]
        public async Task<IActionResult> AddGuide(CreateGuideDto guide)
        {
            await _guideService.AddAsync(guide);
            return RedirectToAction("Index");
           
       }
            
        
        [Route("EditGuide")]
        [HttpGet]
        public async Task<IActionResult> EditGuide(int id)
        {
            var values = await _guideService.GetByIdAsync(id);
            if (values is null)
            {
                return NotFound();
            }

            var model = new UpdateGuideDto
            {
                Id = values.Id,
                Name = values.Name,
                Description = values.Description,
                Image = values.Image,
                TwitterUrl = values.TwitterUrl,
                Description2 = values.Description2,
                GuideListImage = values.GuideListImage,
                InstagramUrl = values.InstagramUrl,
                Status = values.Status
            };
            return View(model);
        }
        [Route("EditGuide")]
        [HttpPost]
        public async Task<IActionResult> EditGuide(UpdateGuideDto guide)
        {
           await _guideService.UpdateAsync(guide);
            return RedirectToAction("Index");
        }
        [Route("ChangeToTrue/{id}")]
        public async Task<IActionResult> ChangeToTrue(int id)
        {
           await _guideService.ChangeGuideStatusToTrueAsync(id);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
        [Route("ChangeToFalse/{id}")]
        public async Task<IActionResult> ChangeToFalse(int id)
        {
           await _guideService.ChangeGuideStatusToFalseAsync(id);
            return RedirectToAction("Index", "Guide", new { area = "Admin" });

        }
    }
}
