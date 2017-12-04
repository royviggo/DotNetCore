using DotNetCore.Data.Database;

namespace DotNetCore.Data.Interfaces
{
    public interface IDbFactory
    {
        DotNetCoreContext GetDbContext();
    }
}   