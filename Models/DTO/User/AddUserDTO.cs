using System.ComponentModel.DataAnnotations;

namespace MovieBackAPI.Models
{
    public class AddUserDTO
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

    }
}