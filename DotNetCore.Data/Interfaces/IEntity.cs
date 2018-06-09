using System;

namespace DotNetCore.Data.Interfaces
{
    public interface IEntity
    {
        void Dispose();
        int Id { get; set; }
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}