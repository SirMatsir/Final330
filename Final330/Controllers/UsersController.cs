using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final330.Models;

namespace Final330.Controllers
{
    [Route("api/[controller]")]
    //[Header("My-Api", "2")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private static int currentId = 101;

        private readonly ILogger<UsersController> logger;

        public UsersController(ILogger<UsersController> logger)
        {
            this.logger = logger;
        }

        //GET All
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(users);
        }

        //GET Specific
        [HttpGet("{id}")]
        public IActionResult GetSpecific(int id)
        {
            var user = users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound(null);
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {

            if (string.IsNullOrEmpty(value.Name))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Name"
                    });
            }

            value.Id = currentId++;
            value.DateAdded = DateTime.Now;
            users.Add(value);

            return CreatedAtAction(
                nameof(GetSpecific),
                new { id = value.Id },
                value
                );
        }
    }
}
