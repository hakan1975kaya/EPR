using System.Text.Json.Serialization;

namespace WindowsService.Dtos.LoginDtos
{
    public class LoginRequestDto
    {
        [JsonPropertyName("registrationNumber")]
        public int RegistrationNumber { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
