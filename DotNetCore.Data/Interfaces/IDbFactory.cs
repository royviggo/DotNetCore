using DotNetCore.Data.Database;

namespace DotNetCore.Data.Interfaces
{
    public interface IDbFactory
    {
        void Dispose();
        DotNetCoreContext GetDbContext();
    }
}   