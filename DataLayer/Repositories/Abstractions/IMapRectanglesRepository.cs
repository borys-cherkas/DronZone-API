using Common.Models;

namespace DataLayer.Repositories.Abstractions
{
    public interface IMapRectanglesRepository : IRepositoryBase<MapRectangle>
    {
        MapRectangle GetByZoneId(string zoneId);
    }
}
