using API.Data;
using API.Models.Address;
using AutoMapper;

namespace API.Mapper
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressRequest, Address>();
            CreateMap<UpdateAddressRequest, Address>();
            CreateMap<Address, GetAddressResponse>();
            CreateMap<Address, CreateAddressResponse>();
        }
    }
}
