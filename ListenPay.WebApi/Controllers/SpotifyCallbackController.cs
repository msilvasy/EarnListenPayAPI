using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ListenPay.WebApi.Models.Spotify;
using ListenPay.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ListenPay.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpotifyCallbackController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IOptions<SpotifyOptions> _spotifyOptions;

        public SpotifyCallbackController(ISpotifyService spotifyService, IOptions<SpotifyOptions> spotifyOptions)
        {
            _spotifyService = spotifyService;
            _spotifyOptions = spotifyOptions;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string code, string state, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                return Redirect($"{_spotifyOptions.Value.LoginCallback}callback?errpr={error}&state={state}");
            }
            else
            {
                var token = await _spotifyService.Login(code, $"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}");
                return Redirect($"{_spotifyOptions.Value.LoginCallback}spotifylogin?access_token={token.access_token}&refresh_token={token.refresh_token}&scope={token.scope}&expires_in={token.expires_in}");
            }
        }

        [HttpGet]
        [Route("GetAccessForIO")]
        public async Task<IActionResult> GetSpotifyAccess(string code, string state, string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                return Redirect($"{_spotifyOptions.Value.LoginCallback}callback?errpr={error}&state={state}");
            }
            else
            {
                var token = await _spotifyService.Login(code, $"{Request.Scheme}://{Request.Host.Value}{Request.Path.Value}");
                return Redirect($"{_spotifyOptions.Value.LoginCallback}products-suite?access_token={token.access_token}&refresh_token={token.refresh_token}&scope={token.scope}&expires_in={token.expires_in}");
            }
        }
    }
}