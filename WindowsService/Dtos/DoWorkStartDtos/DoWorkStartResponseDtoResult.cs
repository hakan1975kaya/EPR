using System.Text.Json.Serialization;

namespace WindowsService.Dtos.DoWorkStartDtos
{
    public class DoWorkStartResponseDtoResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
