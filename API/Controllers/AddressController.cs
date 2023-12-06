using API.Data;
using API.Models.Address;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly MovieContext _context;
        private readonly IMapper _mapper;

        public AddressController(MovieContext movieContext, IMapper mapper)
        {
            _context = movieContext;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateAddressResponse), StatusCodes.Status201Created)]
        public IActionResult AddAddress([FromBody] CreateAddressRequest request)
        {
            Address address = _mapper.Map<Address>(request);

            _context.Addresses.Add(address);
            _context.SaveChanges();

            var createAddressResponse = _mapper.Map<CreateAddressResponse>(address);

            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, createAddressResponse);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAddressResponse>), StatusCodes.Status200OK)]
        public IActionResult GetAddresses([FromQuery] int page = 0, int offset = 5)
        {
            var addresses = _context.Addresses.Skip(page * offset).Take(offset);

            return Ok(_mapper.Map<List<GetAddressResponse>>(addresses));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetAddressResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetAddressResponse>(address));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressRequest request)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address is null)
            {
                return NotFound();
            }

            _mapper.Map(request, address);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address is null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
