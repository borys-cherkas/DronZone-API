using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDroneFilterService
    {
        Task<ICollection<DroneFilter>> GetDroneFiltersAsync();

        Task<DroneFilter> CreateAsync(DroneFilter entity);

        Task<DroneFilter> UpdateAsync(DroneFilter entity);

        Task DeleteAsync(int id);
    }
}
