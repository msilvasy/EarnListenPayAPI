using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models.Spotify
{
    public class SpotifyOptions
    {
        public string AccountBaseUrl { get; set; }
        public string ApiBaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string LoginCallback { get; set; }
    }
}
