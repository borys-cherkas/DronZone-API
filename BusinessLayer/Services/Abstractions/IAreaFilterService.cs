using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace BusinessLayer.Services.Abstractions
{
    public interface IAreaFilterService
    {
        Task<ICollection<AreaFilter>> GetAreaFiltersAsync(string areaId);

        Task<AreaFilter> CreateAsync(AreaFilter entity, string areaId);

        Task<AreaFilter> UpdateAsync(AreaFilter entity);

        Task DeleteAsync(int id);
    }
}
