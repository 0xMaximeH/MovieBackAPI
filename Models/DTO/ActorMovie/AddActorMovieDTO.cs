using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class AddActorMovieDTO
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int ActorId { get; set; }

        public string Role { get; set; }
    }
}