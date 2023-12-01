using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class GetMovieResponse
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime ReadTime { get; set; } = DateTime.Now;
    }
}
