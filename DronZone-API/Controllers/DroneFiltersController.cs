using System.Threading.Tasks;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class DroneFiltersController : Controller
    {
        private readonly IDroneFilterService _droneFilterService;

        public DroneFiltersController(IDroneFilterService droneFilterService)
        {
            _droneFilterService = droneFilterService;
        }

        public async Task<IActionResult> GetDroneFilters()
        {
            var filters = await _droneFilterService.GetDroneFiltersAsync();
            return Json(filters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DroneFilter entity)
        {
            var created = await _droneFilterService.CreateAsync(entity);
            return Json(created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DroneFilter entity)
        {
            var updated = await _droneFilterService.UpdateAsync(entity);
            return Json(updated);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _droneFilterService.DeleteAsync(id);
            return Ok();
        }
    }
}
