using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Common.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("~/connect/userinfo"), Produces("application/json")]
        public async Task<IActionResult> UserInfo()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(appUser);
            var rolesPresentation = string.Join(',', roles);

            return Json(new
            {
                IdentityId = appUser.Id,
                appUser.UserName,
                PersonType = appUser.Person.Type,
                Roles = rolesPresentation,
                appUser.Person.FirstName,
                appUser.Person.LastName,
                appUser.PersonId
            });
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            if (!request.IsPasswordGrantType())
            {
                throw new InvalidOperationException("The specified grant type is not supported.");
            }

            var appUser = await _userManager.FindByEmailAsync(request.Username);
            if (appUser == null)
            {
                return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            var isPassCorrect = await _userManager.CheckPasswordAsync(appUser, request.Password);
            if (!isPassCorrect)
            {
                return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
            }

            var userRoles = await _userManager.GetRolesAsync(appUser);

            // Create a new ClaimsIdentity holding the user identity.
            var identity = new ClaimsIdentity(
                OpenIdConnectServerDefaults.AuthenticationScheme,
                OpenIdConnectConstants.Claims.Name,
                OpenIdConnectConstants.Claims.Role);

            // Add a "sub" claim containing the user identifier, and attach
            // the "access_token" destination to allow OpenIddict to store it
            // in the access token, so it can be retrieved from your controllers.
            identity.AddClaim(OpenIdConnectConstants.Claims.Subject,
                appUser.Id,
                OpenIdConnectConstants.Destinations.AccessToken);

            identity.AddClaim(OpenIdConnectConstants.Claims.Name, request.Username,
                OpenIdConnectConstants.Destinations.AccessToken);

            if (userRoles.Any())
            {
                var roles = string.Join(',', userRoles);
                identity.AddClaim(OpenIdConnectConstants.Claims.Role, roles,
                    OpenIdConnectConstants.Destinations.AccessToken);
            }

            var principal = new ClaimsPrincipal(identity);
            // Ask OpenIddict to generate a new token and return an OAuth2 token response.
            return SignIn(principal, OpenIdConnectServerDefaults.AuthenticationScheme);
        }
    }
}
