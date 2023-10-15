using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class UpdateActorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}