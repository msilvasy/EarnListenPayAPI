using ListenPay.WebApi.Models.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SpotifyService> _logger;
        private readonly IOptionsMonitor<SpotifyOptions> _spotifyOptions;
        public SpotifyService(
            HttpClient httpClient,
            ILogger<SpotifyService> logger,
            IOptionsMonitor<SpotifyOptions> spotifyOptions)
        {
            _httpClient = httpClient;
            _logger = logger;
            _spotifyOptions = spotifyOptions;
        }

        public async Task<Token> Login()
        {
            string uri = $"api/token";
            try
            {
                var content = new FormUrlEncodedContent(new[]
                 {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                 });

                _httpClient.BaseAddress = new Uri(_spotifyOptions.CurrentValue.AccountBaseUrl);
                _logger.LogInformation($"Loading {uri} ...");
                var authorization = Encoding.ASCII.GetBytes($"{_spotifyOptions.CurrentValue.ClientId}:{_spotifyOptions.CurrentValue.ClientSecret}");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(authorization)}");
                var res = await _httpClient.PostAsync(new Uri(uri, UriKind.Relative), content);
                res.EnsureSuccessStatusCode();
                var token = await res.Content.ReadAsAsync<Token>();
                token.access_token = "BQCUGCDG5pFHEGw2T-Xv074bcvDASuoemD_hFJUA9gK6mexTm9LlAMfmMrQh1noLnsfzoiXpYKqgsDVYRhJykdL_5WzOOD5LobGVAGp5bjI54uYa1T4I7c-MWL-NnjDkwIVuEtdYp4XUDLToA7yiVTctV1hgMXfmWj5c6pDjZi5aGYkjJdDXKH8";
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

        public async Task<Token> Login(string code, string host)
        {
            string uri = $"api/token";
            try
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", host)
                 });

                _httpClient.BaseAddress = new Uri(_spotifyOptions.CurrentValue.AccountBaseUrl);
                var authorization = Encoding.ASCII.GetBytes($"{_spotifyOptions.CurrentValue.ClientId}:{_spotifyOptions.CurrentValue.ClientSecret}");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(authorization)}");
                var res = await _httpClient.PostAsync(new Uri(uri, UriKind.Relative), content);
                res.EnsureSuccessStatusCode();
                var token = await res.Content.ReadAsAsync<Token>();
               return token;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

        public async Task<Token> RefreshToken(string refreshToken)
        {
            string uri = $"api/token";
            try
            {

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                 });

                _httpClient.BaseAddress = new Uri(_spotifyOptions.CurrentValue.AccountBaseUrl);
                var authorization = Encoding.ASCII.GetBytes($"{_spotifyOptions.CurrentValue.ClientId}:{_spotifyOptions.CurrentValue.ClientSecret}");
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(authorization)}");
                var res = await _httpClient.PostAsync(new Uri(uri, UriKind.Relative), content);
                res.EnsureSuccessStatusCode();
                var token = await res.Content.ReadAsAsync<Token>();
                token.refresh_token = refreshToken;
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

        public async Task<Playlists> GetPlaylists()
        {
            const string uri = "v1/me/playlists";
            try
            {
                _logger.LogInformation($"Loading {uri} ...");
                var res = await _httpClient.GetAsync(new Uri(uri, UriKind.Relative));
                res.EnsureSuccessStatusCode();
                //var r = await res.Content.ReadAsStringAsync();
                return await res.Content.ReadAsAsync<Playlists>();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

        public async Task<PlaylistTracks> GetPlaylistTracks(string playListId)
        {
            const string uri = "v1/playlists";
            try
            {
                _logger.LogInformation($"Loading {uri} ...");
                var res = await _httpClient.GetAsync(new Uri($"{uri}/{playListId}", UriKind.Relative));
                res.EnsureSuccessStatusCode();
                //var r = await res.Content.ReadAsStringAsync();
                var playlist = await res.Content.ReadAsAsync<Playlist>();

                return playlist.tracks;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

        public async Task Play(string deviceId, string trackUri)
        {
            const string uri = "v1/me/player/play";
            System.Net.HttpStatusCode statusCode = System.Net.HttpStatusCode.InternalServerError;
            try
            {
                _logger.LogInformation($"Loading {uri} ...");

                var res = await _httpClient.PutAsync(new Uri($"{uri}?device_id={deviceId}", UriKind.Relative), new
                {
                    uris = new[]
                    {
                        trackUri
                    }
                }, new JsonMediaTypeFormatter());
                statusCode = res.StatusCode;
                res.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                if (statusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException(ex.Message);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Pause(string deviceId)
        {
            const string uri = "v1/me/player/pause";
            try
            {
                _logger.LogInformation($"Loading {uri} ...");

                var res = await _httpClient.PutAsync(new Uri($"{uri}?device_id={deviceId}", UriKind.Relative), new
                {
                }, new JsonMediaTypeFormatter());
                res.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured calling API {uri}: {ex}");
                throw;
            }
        }

    }
}
