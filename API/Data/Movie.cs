using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        [Range(70, 600)]
        public int Duration { get; set; }
    }
}