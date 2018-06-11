using System;

namespace DotNetCore.Data.Interfaces
{
    public interface IEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}