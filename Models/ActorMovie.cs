using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MovieBackAPI.Models
{
    [PrimaryKey(nameof(ActorId), nameof(MovieId))]

    public class ActorMovie
    {
        [Key, ForeignKey("Actor"), Column(Order = 0)]
        public int ActorId { get; set; }

        [Key, ForeignKey("Movie"), Column(Order = 1)]
        public int MovieId { get; set; }

        public Actor Actor { get; set; }

        public Movie Movie { get; set; }

        public string? Role { get; set; }
    }
}