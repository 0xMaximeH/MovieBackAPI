namespace MovieBackAPI.Models
{
    public class AddActorDTO
    {
        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<RoleInMovieDTO> Movies { get; set; } = new List<RoleInMovieDTO>();
    }
}