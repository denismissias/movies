using API.Data;
using API.Models.Cinema;
using AutoMapper;

namespace API.Mapper
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaRequest, Cinema>();
            CreateMap<UpdateCinemaRequest, Cinema>();
            CreateMap<Cinema, GetCinemaResponse>()
                .ForMember(cinemaResponse => cinemaResponse.Address,
                opt => opt.MapFrom(cinema => cinema.Address));
            CreateMap<Cinema, CreateCinemaResponse>();
        }
    }
}
