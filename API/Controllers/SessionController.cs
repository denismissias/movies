using API.Data;
using API.Models.Session;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public SessionController(MovieContext movieContext, IMapper mapper)
        {
            _context = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateSessionResponse), StatusCodes.Status201Created)]
        public IActionResult AddSession([FromBody] CreateSessionRequest request)
        {
            Session session = _mapper.Map<Session>(request);

            _context.Sessions.Add(session);
            _context.SaveChanges();

            var createSessionResponse = _mapper.Map<CreateSessionResponse>(session);

            return CreatedAtAction(nameof(GetSession), new { movieId = session.MovieId, cinemaId = session.CinemaId }, createSessionResponse);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetSessionResponse>), StatusCodes.Status200OK)]
        public IActionResult GetSessions([FromQuery] int page = 0, int offset = 5)
        {
            var sessions = _context.Sessions.Skip(page * offset).Take(offset);

            return Ok(_mapper.Map<List<GetSessionResponse>>(sessions));
        }

        [HttpGet("{movieId}/{cinemaId}")]
        [ProducesResponseType(typeof(GetSessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSession(int movieId, int cinemaId)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.MovieId == movieId && session.CinemaId == cinemaId);

            if (session is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetSessionResponse>(session));
        }
    }
}
