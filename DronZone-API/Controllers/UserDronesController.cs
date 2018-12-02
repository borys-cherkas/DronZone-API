﻿using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Filters;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models.Identity;
using DronZone_API.ViewModels.Drone.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize(Roles = AppRoles.User)]
    [Route("api/[controller]/[action]")]
    public class UserDronesController : Controller
    {
        private readonly IDroneService _droneService;
        private readonly IPersonService _personService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserDronesController(
            IDroneService droneService,
            IPersonService personService,
            UserManager<ApplicationUser> userManager)
        {
            _droneService = droneService;
            _personService = personService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDrones(DroneListFilterViewModel filterViewModel)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var person = await _personService.GetByIdAsync(identityUser.PersonId);

            var filter = Mapper.Map<DroneListFilter>(filterViewModel);
            var drones = _droneService.GetDronesByPersonId(person.Id, filter);

            foreach (var drone in drones)
            {
                drone.Owner = null;
            }

            return Json(drones);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterByCode(string code)
        {
            code = code?.Trim();

            if (string.IsNullOrEmpty(code) || !_droneService.IsCodeValid(code))
            {
                return BadRequest("invalid_data");
            }

            var identityUser = await _userManager.GetUserAsync(User);
            var person = await _personService.GetByIdAsync(identityUser.PersonId);

            _droneService.AttachDroneToPerson(person.Id, code);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromPerson(string droneId)
        {
            if (string.IsNullOrEmpty(droneId))
            {
                return BadRequest("invalid_data");
            }

            var identityUser = await _userManager.GetUserAsync(User);
            var person = await _personService.GetByIdAsync(identityUser.PersonId);

            _droneService.DetachDroneToPerson(person.Id, droneId);

            return Ok();
        }
    }
}