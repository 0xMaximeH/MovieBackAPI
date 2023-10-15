using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class RoleInMovieDTO
    {
        [Required]
        public int MovieId { get; set; }

        public string Role { get; set; }
    }
}