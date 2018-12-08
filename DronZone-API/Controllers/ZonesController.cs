using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Filters;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models.Identity;
using DronZone_API.ViewModels.Filter.List;
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
        private readonly IZoneValidationRequestService _zoneValidationRequestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ZonesController(
            IZoneService zoneService,
            IZoneValidationRequestService zoneValidationRequestService,
            UserManager<ApplicationUser> userManager)
        {
            _zoneService = zoneService;
            _userManager = userManager;
            _zoneValidationRequestService = zoneValidationRequestService;
        }

        [HttpGet]
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
            var viewModel = Mapper.Map<ZoneDetailedViewModel>(zone);

            var zoneValidationRequest = _zoneValidationRequestService.GetActiveZoneRequest(zone.Id);
            viewModel.ValidationRequestId = zoneValidationRequest?.Id;

            return Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserZones(ZoneListFilterViewModel filterViewModel)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var filter = Mapper.Map<ZoneListFilter>(filterViewModel);
            var zones = _zoneService.GetZonesByPersonId(currentPersonId, filter);

            var viewModels = Mapper.Map<ICollection<ZoneListItemViewModel>>(zones);
            foreach (var vm in viewModels)
            {
                var activeRequest = _zoneValidationRequestService.GetActiveZoneRequest(vm.Id);
                vm.ValidationRequestId = activeRequest?.Id;
            }

            return Json(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfNameAvailable(UpdateZoneNameViewModel model)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var isAvailable = _zoneService.ValidateName(model.ZoneId, model.ZoneName, currentPersonId);

            return Json(new { isAvailable });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateZoneName(UpdateZoneNameViewModel model)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            _zoneService.UpdateZoneName(model.ZoneId, model.ZoneName, currentPersonId);

            return Ok();
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            _zoneService.DeleteWithValidationRequests(id);

            return Ok();
        }
    }
}