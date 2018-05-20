using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class ZoneSettingsRepository : RepositoryBase<ZoneSettings>, IZoneSettingsRepository
    {
        public ZoneSettingsRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.ZoneSettingsSet)
        {
        }
    }
}
