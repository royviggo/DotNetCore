using DotNetCore.Data.Entities;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Repositories
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
