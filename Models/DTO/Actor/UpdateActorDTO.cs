namespace MovieBackAPI.Models
{
    public class UpdateActorDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<ActorMovieDTO> Movies { get; set; } = new List<ActorMovieDTO>();
    }
}