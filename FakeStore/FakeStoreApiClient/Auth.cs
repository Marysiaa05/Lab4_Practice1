using System.Text.Json.Serialization;

namespace FakeStoreApiClient
{
    public class Auth
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
    }
}
