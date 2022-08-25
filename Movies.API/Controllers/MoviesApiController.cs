


namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesApiController : ControllerBase
    {
        public MoviesContext _context;
        public MoviesApiController(MoviesContext context)
        {
            _context = context;
        }
        



        [HttpGet("")]
        public async Task<IEnumerable<Movie>> GetMovies(){
            return await _context.Movies.ToListAsync();
        }


        [HttpPost("")]
        public async Task<IActionResult> PostMovie([FromBody] Movie movie){

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return Ok(movie);

            return Ok();
        }
    }
}