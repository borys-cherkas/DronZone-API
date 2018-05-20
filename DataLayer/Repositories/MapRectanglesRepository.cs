using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories
{
    public class MapRectanglesRepository : RepositoryBase<MapRectangle>, IMapRectanglesRepository
    {
        public MapRectanglesRepository(AppDbContext appDbContext)
            : base(appDbContext, appDbContext.MapRectangles)
        {
        }
    }
}
