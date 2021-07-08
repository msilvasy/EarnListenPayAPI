using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListenPay.WebApi.Models.Spotify;
using ListenPay.WebApi.ResponseModel;
using ListenPay.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListenPay.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }
        [HttpGet("playlist")]
        public async Task<ResponseModel<object>> Playlists()
        {
            try
            {
                var result = _spotifyService.GetPlaylists();
                return await Task.FromResult(new ResponseModel<object>(result.Result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpGet("playlist/{playlistId}/tracks")]
        public async Task<ResponseModel<object>> PlaylistTracks(string playlistId)
        {

            try
            {
                var result = _spotifyService.GetPlaylistTracks(playlistId);
                return await Task.FromResult(new ResponseModel<object>(result.Result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPost("token")]
        public async Task<ResponseModel<object>> Token()
        {
            try
            {
                var result = _spotifyService.Login();
                return await Task.FromResult(new ResponseModel<object>(result.Result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPost("token/refresh/{refreshToken}")]
        public async Task<ResponseModel<object>> RefreshToken(string refreshToken)
        {
            try
            {
                var result = _spotifyService.RefreshToken(refreshToken);
                return await Task.FromResult(new ResponseModel<object>(result.Result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPut("play/{deviceId}/{trackUri}")]
        public async Task<ResponseModel<object>> Play(string deviceId, string trackUri)
        {
            try
            {
                var result = _spotifyService.Play(deviceId, trackUri);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }

        [HttpPut("pause/{deviceId}")]
        public async Task<ResponseModel<object>> Pause(string deviceId)
        {
            try
            {
                var result = _spotifyService.Pause(deviceId);
                return await Task.FromResult(new ResponseModel<object>(result));
            }
            catch (Exception ex)
            {
                return new ExceptionResponse<object>();
            }
        }
    }
}