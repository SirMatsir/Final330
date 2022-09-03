using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final330.Models;

namespace Final330.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepo userRepo;
        private readonly ILogger<UsersController> logger;

        public UsersController(ILogger<UsersController> logger, IUserRepo userRepo)
        {
            this.logger = logger;
            this.userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            logger.LogInformation("This is informational");
            logger.LogWarning("This is a warning");
            logger.LogError("This is an ERROR");

            return Ok(userRepo);
        }

        [HttpGet("{id}")]
        public IActionResult GetSpecific(int id)
        {
            var user = userRepo.Users.FirstOrDefault(t => t.Id == id);

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

            try
            {
                userRepo.Add(value);
            }
            catch
            {
                return BadRequest("You suck.");
            }

            return CreatedAtAction(
                nameof(GetSpecific),
                new { id = value.Id },
                value
                );
        }
    }
}
