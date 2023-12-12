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

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
