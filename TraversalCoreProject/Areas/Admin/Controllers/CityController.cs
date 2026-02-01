using BusinessLayer.Abstract;
using DTOLayer.DTOs.DestinatonDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TraversalCoreProject.Models;

namespace TraversalCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var jsonCity = JsonConvert.SerializeObject(values);
            return Json(jsonCity);
        }
        [HttpPost]
        public async Task<IActionResult> AddCityDestination(CreateDestinationDto destination)
        {
          await  _destinationService.AddAsync(destination);
            var values = JsonConvert.SerializeObject(destination);
            return Json(values);
        }
        public IActionResult GetById(int DestinationId)
        { 
            var values = _destinationService.GetByIdAsync(DestinationId);
            var jsonValues = JsonConvert.SerializeObject(values);
            return Json(jsonValues);
        }
        public async Task<IActionResult> DeleteCity(int id)
        {
           await _destinationService.DeleteAsync(id);
            return NoContent();
        }
        public async Task<IActionResult> UpdateCity(UpdateDestinationDto destination)
        { 
           await _destinationService.UpdateAsync(destination);
            var v = JsonConvert.SerializeObject(destination);
            return Json(v);
        }

    }
}
