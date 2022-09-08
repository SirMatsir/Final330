using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Final330.Models;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Final330.Controllers
{
    public class TokenRequest
    {
        [JsonPropertyName("email_address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [JsonPropertyName("password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    [Route("api/login")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPost]
        public dynamic Post([FromBody] TokenRequest tokenRequest)
        {
            var token = TokenHelper.GetToken(tokenRequest.EmailAddress, tokenRequest.Password);
            return new { Token = token };
        }

        [HttpGet]
        [Route("{email}/{password}")]
        public dynamic Get(string email, string password)
        {
            var token = TokenHelper.GetToken(email, password);
            return new { Token = token };
        }

        //tried creating tokens from UsersRepo, but I couldn't eliminate request body fields from the equation
        //this resulted in SwaggerUI using the request body but requiring an id/user data
        //[HttpPost("{id}")]
        //public IActionResult Post(int id, TokenRequest tokenRequest)
        //{
        //    var user = userRepo.Users.FirstOrDefault(t => t.Id == id);
        //    if (user == null)
        //    {
        //        return BadRequest("You suck.");
        //    }
        //    tokenRequest.EmailAddress = user.Email;
        //    tokenRequest.Password = user.Password;
        //    var token = TokenHelper.GetToken(tokenRequest.EmailAddress, tokenRequest.Password);
        //    return Ok(token);
        //}
    }
}
