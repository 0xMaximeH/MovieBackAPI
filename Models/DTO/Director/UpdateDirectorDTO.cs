namespace MovieBackAPI.Models
{
    public class UpdateDirectorDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}