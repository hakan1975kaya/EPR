using System.Text.Json.Serialization;

namespace WindowsService.Dtos.LoginDtos
{
    public class AccessTokenDataResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public AccessToken AccessToken { get; set; }
    }
}
