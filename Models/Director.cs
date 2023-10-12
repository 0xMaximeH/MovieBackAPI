namespace MovieBackAPI.Models
{
    public class Director : Person
    {
        public ICollection<Movie> DirectedMovies { get; set; }
    }
}