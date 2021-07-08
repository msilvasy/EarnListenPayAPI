using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models.Spotify
{
    public class Token
    {
        public string access_token { get; set; }

        [JsonPropertyName("token_type")]
        public string token_type { get; set; }

        [JsonPropertyName("expires_in")]
        public int expires_in { get; set; }

        public string scope { get; set; }

        public string refresh_token { get; set; }
    }
}
