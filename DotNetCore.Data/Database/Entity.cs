using DotNetCore.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Data.Database
{
    public class Entity : IEntity, IDisposable
    {
        public void Dispose() { }

        [Key]
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
