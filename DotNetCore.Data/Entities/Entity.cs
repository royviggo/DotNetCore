using DotNetCore.Data.Interfaces;
using System;

namespace DotNetCore.Data.Entities
{
    public class Entity : IEntity, IDisposable
    {
        public void Dispose() { }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
