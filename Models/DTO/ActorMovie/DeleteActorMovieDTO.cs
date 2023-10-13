using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class DeleteActorMovieDTO
    {
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int ActorId { get; set; }
    }
}