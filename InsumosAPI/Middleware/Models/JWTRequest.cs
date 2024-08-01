using System.Text.Json.Serialization;

namespace InsumosAPI.Middleware.Models
{
    public class JWTRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
