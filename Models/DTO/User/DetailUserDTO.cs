 
namespace MovieBackAPI.Models
{
    public class DetailUserDTO
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Movie>? FavoriteMovies { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }

    }
}