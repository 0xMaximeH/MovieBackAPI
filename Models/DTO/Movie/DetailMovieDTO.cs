using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class DetailMovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RealeaseYear { get; set; }

        public string? Description { get; set; }

        public int? Rating { get; set; }

        public DirectorOfMovieDTO? Director { get; set; }

        public ICollection<ActorInMovieDTO> Actors { get; set; } = new List<ActorInMovieDTO>();

    }
}