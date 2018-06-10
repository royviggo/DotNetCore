using DotNetCore.Data.Database;
using DotNetCore.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore.Data.Entities
{
    public class Event : Entity
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public int EventTypeId { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }

        [Required]
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person Person { get; set; }

        public int PlaceId { get; set; }

        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

        public GenDate Date { get; set; }

        public string Description { get; set; }
    }
}
