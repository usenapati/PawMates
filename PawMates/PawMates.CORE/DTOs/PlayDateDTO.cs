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
        [Required]
        public int[] HostPets { get; set; }
        [Required]
        public int[] InvitedPets { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (StartTime >= EndTime)
            {
                results.Add(new ValidationResult("End Time can not come before Start Time.", new[] { nameof(EndTime) }));

            }

            if (HostPets.Length <= 0)
            {
                results.Add(new ValidationResult("Must have at least one pet from Host"));
            }

            return results;
        }
    }
}
