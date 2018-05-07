using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstractions;
using Common.Models;
using DataLayer.Repositories.Abstractions;

namespace BusinessLayer.Services
{
    public class DroneFilterService  : IDroneFilterService
    {
        private readonly IDroneFilterRepository _droneFilterRepository;

        public DroneFilterService(IDroneFilterRepository droneFilterRepository)
        {
            _droneFilterRepository = droneFilterRepository;
        }

        public async Task<ICollection<DroneFilter>> GetDroneFiltersAsync()
        {
            var filters = _droneFilterRepository.GetAll();
            return await Task.FromResult(filters);
        }

        public Task<DroneFilter> GetByIdAsync(int id)
        {
            var filter = _droneFilterRepository.GetSingleByPredicate(x => x.Id == id);
            return Task.FromResult(filter);
        }

        public Task<DroneFilter> CreateAsync(DroneFilter entity)
        {

            var newFilter = _droneFilterRepository.Add(entity);
            return Task.FromResult(newFilter);
        }

        public Task<DroneFilter> UpdateAsync(DroneFilter entity)
        {
            var updatedFilter = _droneFilterRepository.Update(entity);
            return Task.FromResult(updatedFilter);
        }

        public async Task DeleteAsync(int id)
        {
            var filter = await GetByIdAsync(id);
            if (filter == null)
            {
                return;
            }

            _droneFilterRepository.Delete(filter);
        }
    }
}
