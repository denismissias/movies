using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
