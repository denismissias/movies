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
            CreateMap<UpdateCinemaRequest, Cinema>().ReverseMap();
            CreateMap<Cinema, GetCinemaResponse>();
        }
    }
}
