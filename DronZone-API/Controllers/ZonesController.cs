using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models;
using Common.Models.Identity;
using DronZone_API.ViewModels.Zone;
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

        //[HttpGet]
        //public async Task<IActionResult> GetConfirmedUserZones()
        //{
        //    var currentIdentityUser = await _userManager.GetUserAsync(User);
        //    var currentPersonId = currentIdentityUser.PersonId;

        //    var zones = _zoneService.GetZonesByPersonId(currentPersonId);
        //    return Json(zones);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllUserZones()
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var zones = _zoneService.GetZonesByPersonId(currentPersonId);
            return Json(zones);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddZoneViewModel model)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            if (ModelState.IsValid)
            {
                var zone = Mapper.Map<Zone>(model);

                zone.OwnerId = currentPersonId;

                //TODO: Change to 'false' when Admin confirmation is implemented
                zone.IsConfirmed = true;

                _zoneService.Add(zone);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _zoneService.Delete(id);

            return Ok();
        }
    }
}