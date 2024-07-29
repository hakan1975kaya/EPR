using System.Text.Json.Serialization;

namespace WindowsService.Dtos.DoWorkDtos
{
    public class DoWorkResponseDtoResult
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
