using API.Data;
using API.Models.Movie;
using AutoMapper;

namespace API.Mapper
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<CreateMovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();
            CreateMap<Movie, GetMovieResponse>();
        }
    }
}
