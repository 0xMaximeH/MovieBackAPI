namespace MovieBackAPI.Models
{
    public class UpdateMovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RealeaseYear { get; set; }

        public string? Description { get; set; }

        public int? Rating { get; set; }

        public int DirectorId { get; set; }

        public ICollection<ActorInMovieDTO> Actors { get; set; } = new List<ActorInMovieDTO>();

    }
}