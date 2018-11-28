using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    // [Authorize(Roles = AppRoles.Administrator)]
    [Route("api/[controller]/[action]")]
    public class AreaValidationController : Controller
    {

    }
}
