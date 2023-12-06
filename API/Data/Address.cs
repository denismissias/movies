using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
