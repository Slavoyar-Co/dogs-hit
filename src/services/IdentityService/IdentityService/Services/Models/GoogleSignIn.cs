using Newtonsoft.Json;

namespace IdentityService.Services.Models
{
    public class GoogleSignInModel
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("authuser")]
        public int AuthUser { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }
    }
}
