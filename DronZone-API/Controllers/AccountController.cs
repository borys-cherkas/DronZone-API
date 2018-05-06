using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using BusinessLayer.Services.Abstractions;
using Common.Constants;
using Common.Models;
using Common.Models.Additional;
using Common.Models.Identity;
using DronZone_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPersonService _personService;

        public AccountController(UserManager<ApplicationUser> userManager, IPersonService personService)
        {
            _userManager = userManager;
            _personService = personService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(appUser);
            var rolesPresentation = string.Join(',', roles);

            var person = await _personService.GetByIdAsync(appUser.PersonId);

            return Json(new
            {
                IdentityId = appUser.Id,
                appUser.UserName,
                PersonType = person.Type,
                Roles = rolesPresentation,
                person.FirstName,
                person.LastName,
                appUser.PersonId
            });
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict);
                }

                var person = new Person
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Type = PersonType.Individual
                };
                person = await _personService.CreatePersonAsync(person);
                if (string.IsNullOrEmpty(person.Id))
                {
                    return BadRequest(ModelState);
                }

                user = new ApplicationUser { UserName = model.Email, Email = model.Email, PersonId = person.Id };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AppRoles.User);
                    return Ok();
                }

                await _personService.DeletePersonAsync(person.Id);
                AddErrors(result);
            }

            // If we got this far, something failed.
            return BadRequest(ModelState);
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
