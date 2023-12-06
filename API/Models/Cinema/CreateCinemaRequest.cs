using System.ComponentModel.DataAnnotations;

namespace API.Models.Cinema
{
    public class CreateCinemaRequest
    {
        [Required]
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}
