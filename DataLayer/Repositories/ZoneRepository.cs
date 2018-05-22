using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class ZoneRepository : RepositoryBase<Zone, string>, IZoneRepository
    {
        private readonly IZoneSettingsRepository _zoneSettingsRepository;
        private readonly IAreaFilterRepository _areaFilterRepository;
        private readonly IMapRectanglesRepository _mapRectanglesRepository;

        public ZoneRepository(
            AppDbContext appDbContext, 
            IZoneSettingsRepository zoneSettingsRepository,
            IAreaFilterRepository areaFilterRepository,
            IMapRectanglesRepository mapRectanglesRepository)
            : base(appDbContext, appDbContext.RegisteredZones)
        {
            _zoneSettingsRepository = zoneSettingsRepository;
            _areaFilterRepository = areaFilterRepository;
            _mapRectanglesRepository = mapRectanglesRepository;
        }

        public override void Delete(Zone entity)
        {
            var settings = _zoneSettingsRepository.GetSingleByPredicate(x => x.ZoneId == entity.Id);
            var filters = _areaFilterRepository.GetAll(x => x.ZoneSettingsId == settings.Id);
            var mapRectangle = _mapRectanglesRepository.GetSingleByPredicate(x => x.ZoneId == entity.Id);

            using (var transaction = DbContext.Database.BeginTransaction())
            {
                DbContext.AreaFilters.RemoveRange(filters);
                DbContext.ZoneSettingsSet.Remove(settings);
                DbContext.MapRectangles.Remove(mapRectangle);
                DbContext.RegisteredZones.Remove(entity);
                DbContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
