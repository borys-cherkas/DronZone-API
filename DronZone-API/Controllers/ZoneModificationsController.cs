using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models;
using Common.Models.Additional;
using Common.Models.Identity;
using DronZone_API.ViewModels.Zone;
using DronZone_API.ViewModels.ZoneValidationRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    public class ZoneModificationsController : Controller
    {
        private readonly IZoneService _zoneService;
        private readonly IPersonService _personService;
        private readonly IZoneValidationRequestService _zoneValidationRequestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ZoneModificationsController(
            IZoneValidationRequestService zoneValidationRequestService,
            IZoneService zoneService,
            IPersonService personService,
            UserManager<ApplicationUser> userManager)
        {
            _zoneValidationRequestService = zoneValidationRequestService;
            _userManager = userManager;
            _personService = personService;
            _zoneService = zoneService;
        }
        
        [Authorize(Roles = AppRoles.UserAndAdministrator)]
        [HttpGet]
        public async Task<IActionResult> GetRequestById(string id)
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(currentIdentityUser, AppRoles.Administrator);
            var currentPersonId = isAdmin ? "admin" : currentIdentityUser.PersonId;

            var userRequest = _zoneValidationRequestService.GetRequestById(id, currentPersonId);

            var viewModel = Mapper.Map<ZoneValidationRequestDetailedViewModel>(userRequest);

            viewModel.CanConfirmReject =
                isAdmin
                && userRequest.ResponsiblePersonId == currentIdentityUser.PersonId
                && userRequest.Status == ZoneValidationStatus.InProgress;
            
            return Json(viewModel);
        }

        [Authorize(Roles = AppRoles.User)]
        [HttpGet]
        public async Task<IActionResult> GetUserRequests()
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var userRequests = _zoneValidationRequestService.GetUserZoneRequests(currentPersonId);

            var userRequestsListItems = Mapper.Map<ICollection<ZoneValidationRequestListItemViewModel>>(userRequests);

            foreach (var vm in userRequestsListItems.Where(x => !string.IsNullOrEmpty(x.TargetZoneId)))
            {
                var zone = _zoneService.GetZoneById(vm.TargetZoneId);
                vm.ZoneName = zone.Name;
            }

            return Json(userRequestsListItems);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetUntakenRequests()
        {
            var untakenRequests = _zoneValidationRequestService.GetUntakenZoneRequests();

            var untakenRequestsListItems = Mapper.Map<ICollection<AdminRequestListItemViewModel>>(untakenRequests);
            foreach (var listItem in untakenRequestsListItems)
            {
                var person = await _personService.GetByIdAsync(listItem.RequesterId);
                listItem.RequesterName = $"{person.FirstName} {person.LastName}";
            }

            return Json(untakenRequestsListItems);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpGet]
        public async Task<IActionResult> GetTakenByMeActiveRequests()
        {
            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            var adminRequests = _zoneValidationRequestService.GetTakenByUserActiveZoneRequests(currentPersonId);

            var adminRequestsListItems = Mapper.Map<ICollection<AdminRequestListItemViewModel>>(adminRequests);
            foreach (var listItem in adminRequestsListItems)
            {
                var person = await _personService.GetByIdAsync(listItem.RequesterId);
                listItem.RequesterName = $"{person.FirstName} {person.LastName}";
            }

            return Json(adminRequestsListItems);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> AssignRequestToCurrentUser(AssignZoneValidationRequestViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var currentIdentityUser = await _userManager.GetUserAsync(User);
            var currentPersonId = currentIdentityUser.PersonId;

            _zoneValidationRequestService.AssignToUser(model.RequestId, currentPersonId);

            return Ok();
        }

        [Authorize(Roles = AppRoles.User)]
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

        [Authorize(Roles = AppRoles.User)]
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

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> ConfirmRequest(AssignZoneValidationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentIdentityUser = await _userManager.GetUserAsync(User);
                var currentPersonId = currentIdentityUser.PersonId;

                _zoneValidationRequestService.ConfirmZoneRequest(model.RequestId, currentPersonId);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = AppRoles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> DeclineRequest(AssignZoneValidationRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentIdentityUser = await _userManager.GetUserAsync(User);
                var currentPersonId = currentIdentityUser.PersonId;

                _zoneValidationRequestService.DeclineZoneRequest(model.RequestId, currentPersonId);

                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = AppRoles.User)]
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
