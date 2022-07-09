using Newtonsoft.Json;
namespace Auth0.ManagementApi.Api.Model {
    public class Auth0TokenVM {

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string tokenType { get; set; }
    }
}