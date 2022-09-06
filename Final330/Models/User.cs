using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Final330.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [JsonPropertyName("date_added")]
        public DateTime DateAdded { get; set; }

    }
}

