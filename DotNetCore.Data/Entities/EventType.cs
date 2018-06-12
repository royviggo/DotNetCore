using System.ComponentModel.DataAnnotations;

namespace DotNetCore.Data.Entities
{
    public class EventType : Entity
    {
        [Key]
        public int EventTypeId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string GedcomTag { get; set; }

        [MaxLength(255)]
        public string Sentence { get; set; }

        public bool IsFamilyEvent { get; set; }

        public bool UseDate { get; set; }

        public bool UsePlace { get; set; }

        public bool UseDescription { get; set; }
    }
}
