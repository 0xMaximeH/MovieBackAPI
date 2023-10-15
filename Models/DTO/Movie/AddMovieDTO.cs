using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class AddMovieDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public int RealeaseYear { get; set; }

        public string? Description { get; set; }

        public int? Rating { get; set; }

        [Required]
        public int DirectorId { get; set; }

    }
}