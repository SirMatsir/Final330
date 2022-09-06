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
        //Feeding a List for GetAll, GetSpecific, Delete, and Update to work off the bat
        private static List<User> users = new List<User>()
        {
            new User() { Id = 1, Email = "suzanne@email.com", Password = "suzanne_pw", DateAdded = DateTime.Now },
            new User() { Id = 2, Email = "abdul@email.com", Password = "abdul_pw", DateAdded = DateTime.Now },
            new User() { Id = 3, Email = "morgan@email.com", Password = "morgan_pw", DateAdded = DateTime.Now },
            new User() { Id = 4, Email = "horatio@email.com", Password = "horatio_pw", DateAdded = DateTime.Now }
        };
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

            value.Id = currentId++;
            value.DateAdded = DateTime.Now;
            users.Add(value);

            return CreatedAtAction(
                nameof(GetSpecific),
                new { id = value.Id },
                value
                );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User updatedUser)
        {
            var user = users.FirstOrDefault(t => t.Id == id);

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(t => t.Id == id);

            if (user == null)
            {
                return NotFound(null);
            }
            //else
            users.Remove(user);
            return Ok(user.Email + " was removed from the list");
        }
    }
}
