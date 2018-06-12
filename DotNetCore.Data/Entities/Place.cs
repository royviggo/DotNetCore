using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Data.Entities
{
    public class Place : Entity
    {
        [Key]
        public int PlaceId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [ForeignKey("PlaceId")]
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
