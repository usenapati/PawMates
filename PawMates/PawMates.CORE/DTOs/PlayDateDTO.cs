using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class PlayDateDTO : IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PetParentId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (StartTime >= EndTime)
            {
                results.Add(new ValidationResult("End Time can not come before Start Time.", new[] { nameof(EndTime) }));

            }

            return results;
        }
    }
}
