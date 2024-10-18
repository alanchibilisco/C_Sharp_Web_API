using System.Text.Json;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleController : ControllerBase
    {

        private readonly string googleClientId = "654705958554-6fpgbimh2b0o3sng96gn6eset8nkmjkr.apps.googleusercontent.com";
        private readonly ILogger<GoogleController> _logger;


        public GoogleController(ILogger<GoogleController> logger)
        {
            _logger = logger;
        }

        #region Endpoints
        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] GoogleTokenDto googleTokenDto)
        {
            try
            {
                GoogleJsonWebSignature.Payload? payload = await VerifyGoogleToken(googleTokenDto.Token);

                if (payload == null)
                {
                    return BadRequest();
                }
                string strPayload = JsonSerializer.Serialize(payload);
                _logger.LogInformation($"PAYLOAD--> {strPayload}");
                string appToken = "JOSUE-APP-TOKEN";
                return Ok(new { Token = appToken });
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest();
            }
        }
        #endregion

        #region PrivateMethods
        // Método para verificar el token de Google
        private async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string token)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { googleClientId } // Define el Client ID como audiencia permitida
            };

            try
            {
                // Verifica el ID token con el método de Google
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
                return payload;
            }
            catch (InvalidJwtException)
            {
                Console.WriteLine("Invalid Google Token");
                return null;
            }
        }
        #endregion
    }

    public class GoogleTokenDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
