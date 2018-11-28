using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Filters;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models.Identity;
using DronZone_API.ViewModels.Filter.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DronZone_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]/{id?}")]
    public class ZonesController : Controller
    {
        private readonly IZoneService _zoneService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ZonesController(IZoneService zoneService, UserManager<ApplicationUser> userManager)
        {
            _zoneService = zoneService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var zone = _zoneService.GetZoneById(id, q => q.Include(x => x.MapRectangle));

            // TODO: Try to replace with this line and pass it to the Json(): 
            // new JsonSerializerSettings().ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            zone.MapRectangle.Zone = null;
            return Json(zone);
        }

        [HttpGet]
        [Authorize(Roles = AppRoles.Administrator)]
        public IActionResult GetUnconfirmedZones()
        {
            var zones = _zoneService.GetAllUnconfirmedZones();
            return Json(zones);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserZones(ZoneListFilterViewModel filterViewModel)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var filter = Mapper.Map<ZoneListFilter>(filterViewModel);
            var zones = _zoneService.GetZonesByPersonId(currentPersonId, filter);
            return Json(zones);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _zoneService.DeleteWithValidationRequests(id);

            return Ok();
        }
    }
}