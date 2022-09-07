using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
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
            this.userRepo = userRepo;
            this.logger = logger;
        }

        //GET All
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(userRepo.Users);
        }

        //GET Specific
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

        //Add
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {

            if (string.IsNullOrEmpty(value.Email))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Email"
                    });
            }

            if (string.IsNullOrEmpty(value.Password))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Password"
                    });
            }

            value.DateAdded = DateTime.Now;
            userRepo.Add(value);

            return CreatedAtAction(
                nameof(GetSpecific),
                new { id = value.Id },
                value
                );
        }

        //Update
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User updatedUser)
        {
            var user = userRepo.Users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound(null);
            }

            if (string.IsNullOrEmpty(updatedUser.Email))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Email"
                    });
            }

            if (string.IsNullOrEmpty(updatedUser.Password))
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Message = "Null or Empty Field",
                        Field = "Password"
                    });
            }

            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            return Ok(user);
        }

        //Delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = userRepo.Users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound(null);
            }
            //else
            userRepo.Delete(user.Id);
            return Ok(user.Email + " was removed from the list");
        }
    }

}
