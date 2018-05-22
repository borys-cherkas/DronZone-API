using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class AreaFilterService  : IAreaFilterService
    {
        private readonly IAreaFilterRepository _areaFilterRepository;
        private readonly IZoneService _zoneService;

        public AreaFilterService(IAreaFilterRepository areaFilterRepository, IZoneService zoneService)
        {
            _areaFilterRepository = areaFilterRepository;
            _zoneService = zoneService;
        }

        public async Task<ICollection<AreaFilter>> GetAreaFiltersAsync(string areaId)
        {
            var filters = _areaFilterRepository.GetAll(x => x.ZoneSettings.ZoneId == areaId);
            return await Task.FromResult(filters);
        }

        public Task<AreaFilter> GetByIdAsync(int id)
        {
            var filter = _areaFilterRepository.GetSingleByPredicate(x => x.Id == id);
            return Task.FromResult(filter);
        }

        public Task<AreaFilter> CreateAsync(AreaFilter entity, string areaId)
        {
            var area = _zoneService.GetZoneById(areaId, x => x.Include(z => z.Settings));
            if (area == null)
            {
                return null;
            }

            entity.ZoneSettingsId = area.Settings.Id;

            var newFilter = _areaFilterRepository.Add(entity);
            return Task.FromResult(newFilter);
        }

        public Task<AreaFilter> UpdateAsync(AreaFilter entity)
        {
            var updatedFilter = _areaFilterRepository.Update(entity);
            return Task.FromResult(updatedFilter);
        }

        public async Task DeleteAsync(int id)
        {
            var filter = await GetByIdAsync(id);
            if (filter == null)
            {
                return;
            }

            _areaFilterRepository.Delete(filter);
        }
    }
}
