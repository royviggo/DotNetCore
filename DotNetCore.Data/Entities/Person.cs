using System;
using System.ComponentModel.DataAnnotations;
using DotNetCore.Data.Enums;
using DotNetCore.Data.Interfaces;

namespace DotNetCore.Data.Entities
{
    public class Person : IEntity, IDisposable
    {
        public Person()
        {
        }

        public void Dispose()
        {
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string FatherName { get; set; }

        [MaxLength(255)]
        public string Patronym { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public int? BornYear { get; set; }

        public int? DeathYear { get; set; }

        public Status Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
