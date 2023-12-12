using API.Models.Address;
using API.Models.Session;

namespace API.Models.Cinema
{
    public class GetCinemaResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GetAddressResponse Address { get; set; }
        public ICollection<GetSessionResponse> Sessions { get; set; }
    }
}
