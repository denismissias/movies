using API.Data;
using API.Models.Movie;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public MoviesController(MovieContext movieContext, IMapper mapper)
        {
            _context = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMovie([FromBody] CreateMovieRequest request)
        {
            Movie movie = _mapper.Map<Movie>(request);

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetMovieResponse>), StatusCodes.Status200OK)]
        public IActionResult GetMovies([FromQuery] int page = 0, int offset = 5)
        {
            var movies = _context.Movies.Skip(page * offset).Take(offset);

            return Ok(_mapper.Map<List<GetMovieResponse>>(movies));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMovieResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetMovieResponse>(movie));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieRequest request)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            _mapper.Map(request, movie);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PartialUpdateMovie(int id, [FromBody] JsonPatchDocument<UpdateMovieRequest> request)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            var movieToPatch = _mapper.Map<UpdateMovieRequest>(movie);

            request.ApplyTo(movieToPatch, ModelState);

            if (!TryValidateModel(movieToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(movieToPatch, movie);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
