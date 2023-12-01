using API.Data;
using API.Models.Cinema;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public CinemaController(MovieContext movieContext, IMapper mapper)
        {
            _context = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddCinema([FromBody] CreateCinemaRequest request)
        {
            Cinema cinema = _mapper.Map<Cinema>(request);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCinema), new { id = cinema.Id }, cinema);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetCinemaResponse>), StatusCodes.Status200OK)]
        public IActionResult GetCinemas([FromQuery]int page = 0, int offset = 5)
        {
            var cinemas = _context.Cinemas.Skip(page * offset).Take(offset);

            return Ok(_mapper.Map<List<GetCinemaResponse>>(cinemas));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetCinemaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetCinemaResponse>(cinema));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCinema(int id, [FromBody]UpdateCinemaRequest request)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema is null)
            {
                return NotFound();
            }

            _mapper.Map(request, cinema);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema is null)
            {
                return NotFound();
            }

            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();

            return NoContent();
        }   
    }
}
