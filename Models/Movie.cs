using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBackAPI.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int RealeaseYear { get; set; }

        public string? Description { get; set; }

        public int? Rating { get; set; }

        [ForeignKey("Director")]
        public int DirectorId { get; set; }

        public Director? Director { get; set; }

        public ICollection<ActorMovie> Actors { get; set; } = new List<ActorMovie>();
    }
}