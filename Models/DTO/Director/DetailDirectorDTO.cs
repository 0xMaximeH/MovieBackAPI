namespace MovieBackAPI.Models
{
    public class DetailDirectorDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<DirectedMovieDTO> Movies { get; set; } = new List<DirectedMovieDTO>();
    }
}