using API.Data;
using API.Models.Cinema;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [ProducesResponseType(typeof(CreateCinemaResponse), StatusCodes.Status201Created)]
        public IActionResult AddCinema([FromBody] CreateCinemaRequest request)
        {
            Cinema cinema = _mapper.Map<Cinema>(request);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();

            var createCinemaResponse = _mapper.Map<CreateCinemaResponse>(cinema);

            return CreatedAtAction(nameof(GetCinema), new { id = cinema.Id }, createCinemaResponse);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetCinemaResponse>), StatusCodes.Status200OK)]
        public IActionResult GetCinemas([FromQuery] int? addressId, int page = 0, int offset = 5)
        {
            IQueryable<Cinema> cinemas;

            if (addressId.HasValue)
            {
                string sql = "SELECT * FROM Cinemas WHERE AddressId = {0} LIMIT {1} OFFSET {2}";

                cinemas = _context.Cinemas.FromSqlRaw(sql, addressId, offset, page * offset);

                return Ok(_mapper.Map<List<GetCinemaResponse>>(cinemas.ToList()));
            }
            
            cinemas = _context.Cinemas.Skip(page * offset).Take(offset);

            return Ok(_mapper.Map<List<GetCinemaResponse>>(cinemas.ToList()));
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
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaRequest request)
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
