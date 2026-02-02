using BusinessLayer.Abstract;
using DTOLayer.DTOs.DestinatonDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly IDestinationService _destinationService;
        public CityController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }
        public async Task<IActionResult> Index()
        {
            var values=await _destinationService.GetListAsync();
            return View(values);
        }
        public async Task<IActionResult> CityList()
        {
            var values = await _destinationService.GetListAsync();
            return Json(values);
        }
        [HttpPost]
        public async Task<IActionResult> AddCityDestination(DestinationInputDto destination)
        {
          await  _destinationService.AddAsync(destination);
            return Json(destination); ;
        }
        public async Task<IActionResult> GetById(int DestinationId)
        {
            var values = await _destinationService.GetByIdAsync(DestinationId);
            return Json(values);
        }
        public async Task<IActionResult> DeleteCity(int id)
        {
           await _destinationService.DeleteAsync(id);
            return NoContent();
        }
        public async Task<IActionResult> UpdateCity(DestinationInputDto destination)
        { 
           await _destinationService.UpdateAsync(destination);
            return Json(destination);
        }

    }
}
