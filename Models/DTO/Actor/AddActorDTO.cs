using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class AddActorDTO
    {
        [Required]
        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}