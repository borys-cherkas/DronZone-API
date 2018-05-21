using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class ZoneSettingsRepository : RepositoryBase<ZoneSettings, int>, IZoneSettingsRepository
    {
        public ZoneSettingsRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.ZoneSettingsSet)
        {
        }
    }
}
