using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize]
    public class DroneCharacteristicsController : Controller
    {
        public async Task<IActionResult> GetDroneCharacteristics()
        {
            return Json(new string[] { });
        }

        [HttpPost]
        public async Task<IActionResult> Create(DroneCharacteristics entity)
        {
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DroneCharacteristics entity)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }
    }
}
