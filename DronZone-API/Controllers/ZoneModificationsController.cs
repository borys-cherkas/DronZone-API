using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models;
using Common.Models.Identity;
using DronZone_API.ViewModels.Zone;
using DronZone_API.ViewModels.ZoneValidationRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize(Roles = AppRoles.User)]
    [Route("api/[controller]/[action]")]
    public class ZoneModificationsController : Controller
    {
        private readonly IZoneValidationRequestService _zoneValidationRequestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ZoneModificationsController(
            IZoneValidationRequestService zoneValidationRequestService,
            UserManager<ApplicationUser> userManager)
        {
            _zoneValidationRequestService = zoneValidationRequestService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequestById(string requestId)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var userRequest = _zoneValidationRequestService.GetRequestById(requestId, currentPersonId);

            var viewModel = Mapper.Map<ZoneValidationRequestDetailedViewModel>(userRequest);
            return Json(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRequests()
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var userRequests = _zoneValidationRequestService.GetUserZoneRequests(currentPersonId);

            var userRequestsListItems = Mapper.Map<ICollection<ZoneValidationRequestListItemViewModel>>(userRequests);
            return Json(userRequestsListItems);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetUnresolvedRequests()
        {
            // TODO: Implement
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddingZoneRequest(AddZoneValidationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentIdentityUser = await _userManager.GetUserAsync(User);
                var currentPersonId = currentIdentityUser.PersonId;
       
                var zoneValidationRequest = Mapper.Map<ZoneValidationRequest>(model);

                _zoneValidationRequestService.CreateNewZoneRequest(zoneValidationRequest, currentPersonId);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateModifyingZoneRequest(ModifyZoneValidationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentIdentityUser = await _userManager.GetUserAsync(User);
                var currentPersonId = currentIdentityUser.PersonId;
                
                var zoneValidationRequest = Mapper.Map<ZoneValidationRequest>(model);

                _zoneValidationRequestService.CreateModifyZoneRequest(zoneValidationRequest, currentPersonId);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CancelRequest(CancelZoneValidationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentIdentityUser = await _userManager.GetUserAsync(User);
                var currentPersonId = currentIdentityUser.PersonId;

                _zoneValidationRequestService.CancelZoneRequest(model.RequestId, currentPersonId);

                return Ok();
            }

            return BadRequest();
        }
    }
}
