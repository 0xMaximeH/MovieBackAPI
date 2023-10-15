namespace MovieBackAPI.Models
{
    public class ReviewDTO
    {

        public int MovieId { get; set; }

        public int? Rate { get; set; }

        public string? Comment {  get; set; } 

    }
}