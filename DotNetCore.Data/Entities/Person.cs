using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore.Data.Database;
using DotNetCore.Data.Enums;

namespace DotNetCore.Data.Entities
{
    public class Person : Entity
    {
        [Key]
        public int PersonId { get; set; }

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

        [ForeignKey("PersonId")]
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
