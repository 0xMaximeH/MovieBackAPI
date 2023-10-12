namespace MovieBackAPI.Models
{
    public class Actor : Person
    {
        public ICollection<ActorMovie> ActedMovies { get; set; } = new List<ActorMovie>();
    }
}