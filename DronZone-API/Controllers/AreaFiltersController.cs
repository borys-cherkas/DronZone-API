using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DronZone_API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DronZone_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]/{id?}")]
    public class AreaFiltersController : Controller
    {
        private readonly IAreaFilterService _areaFilterService;

        public AreaFiltersController(IAreaFilterService areaFilterService)
        {
            _areaFilterService = areaFilterService;
        }

        public async Task<IActionResult> GetAreaFilters(string id)
        {
            var filters = await _areaFilterService.GetAreaFiltersAsync(id);
            return Json(filters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddAreaFilterViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var area = Mapper.Map<AreaFilter>(entity);
            var created = await _areaFilterService.CreateAsync(area, entity.AreaId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(AreaFilter entity)
        {
            var updated = await _areaFilterService.UpdateAsync(entity);
            return Json(updated);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _areaFilterService.DeleteAsync(id);
            return Ok();
        }
    }
}
