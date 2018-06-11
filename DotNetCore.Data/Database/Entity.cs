using DotNetCore.Data.Interfaces;
using System;

namespace DotNetCore.Data.Database
{
    public class Entity : IEntity, IDisposable
    {
        public void Dispose() { }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
