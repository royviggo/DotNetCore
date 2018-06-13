using System;
using DotNetCore.Data.Database;

namespace DotNetCore.Data.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        DotNetCoreContext Context();
    }
}   