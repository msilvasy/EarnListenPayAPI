using ListenPay.WebApi.Models.Spotify;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public interface ISpotifyService
    {
        Task<Playlists> GetPlaylists();
        Task<PlaylistTracks> GetPlaylistTracks(string playListId);
        Task<Token> Login();
        Task<Token> Login(string code, string host);
        Task Pause(string deviceId);
        Task Play(string deviceId, string trackUri);
        Task<Token> RefreshToken(string refreshToken);
    }
}