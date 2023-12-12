using API.Data;
using API.Models.Session;
using AutoMapper;

namespace API.Mapper
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<CreateSessionRequest, Session>();
            CreateMap<Session, CreateSessionResponse>();
            CreateMap<Session, GetSessionResponse>();
        }
    }
}
