using System.ComponentModel.DataAnnotations;

namespace API.Models.Cinema
{
    public class UpdateCinemaRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
