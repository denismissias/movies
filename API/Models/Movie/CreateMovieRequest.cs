using System.ComponentModel.DataAnnotations;

namespace API.Models.Movie
{
    public class CreateMovieRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Range(70, 600)]
        public int Duration { get; set; }
    }
}
