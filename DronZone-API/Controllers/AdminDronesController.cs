using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize(Roles = AppRoles.Administrator)]
    [Route("api/[controller]/[action]")]
    public class AdminDronesController : Controller
    {
        private readonly IDroneService _droneService;

        public AdminDronesController(IDroneService droneService)
        {
            _droneService = droneService;
        }

        [HttpPost]
        public IActionResult GenerateDrones()
        {
            _droneService.GenerateDronesPack();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetDetachedDrones()
        {
            var drones = _droneService.GetDetachedDrones();
            return Json(drones);
        }
    }
}
