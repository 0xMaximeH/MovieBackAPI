using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class ChangeFavoiteMoviesDTO
    {
        public ICollection<int> FavoriteMoviesIds { get; set; } = new List<int>(4);

    }
}