using API.Models.Address;

namespace API.Models.Cinema
{
    public class GetCinemaResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetAddressResponse Address { get; set; }
    }
}
