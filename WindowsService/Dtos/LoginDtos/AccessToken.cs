using System.Text.Json.Serialization;

namespace WindowsService.Dtos.LoginDtos
{
    public class AccessToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("tokenExpiration")]
        public DateTime TokenExpiration { get; set; }
    }
}
