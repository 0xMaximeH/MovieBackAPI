using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Biography { get; set; }
        
    }
}