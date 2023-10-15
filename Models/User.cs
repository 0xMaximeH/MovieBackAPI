using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBackAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public ICollection<User> Followers { get; set; } = new List<User>();

        public ICollection<User> Following { get; set; } = new List<User>();

        public ICollection<Movie> FavoriteMovies { get; set; } = new List<Movie>(4);

    }
}