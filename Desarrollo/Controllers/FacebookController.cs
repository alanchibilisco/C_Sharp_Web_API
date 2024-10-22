using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly ILogger<FacebookController> _logger;
        private readonly HttpClient _httpClient;

        public FacebookController(ILogger<FacebookController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] FacebookTokenDto facebookTokenDto)
        {
            if (string.IsNullOrEmpty(facebookTokenDto.Token))
            {
                return BadRequest("Access token is required.");
            }

            // URL para obtener información del usuario con el token de acceso
            var userInfoUrl = $"https://graph.facebook.com/v21.0/me?fields=name,email,first_name,last_name&access_token={facebookTokenDto.Token}";

            try
            {
                // Realizar la solicitud a la API Graph de Facebook
                var response = await _httpClient.GetAsync(userInfoUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation(JsonSerializer.Serialize(response));
                    return Unauthorized("Invalid access token or request.");
                }

                // Leer la respuesta como JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta en un objeto
                var userInfo = JsonSerializer.Deserialize<FacebookUser>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                string strPayload = JsonSerializer.Serialize(userInfo);
                _logger.LogInformation(strPayload);
                string appToken = "JOSUE-APP-TOKEN";

                // Devolver los datos del usuario
                return Ok(new { Token = appToken });
            }
            catch (HttpRequestException e)
            {
                _logger.LogError("Error", e);
                // Manejo de errores en la solicitud HTTP
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                System.Console.WriteLine("###LOGOUT###");
                return Ok();
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest();
            }
        }

        [HttpGet("app-token")]
        public async Task<IActionResult> GetAuth()
        {
            try
            {
                FacebookAccessTokenApp? response = await GetAppToken();
                if (response==null)
                {
                    throw new Exception("No se pudo obtener el token de acceso a la API de Facebook");
                }

                System.Console.WriteLine($"RESPONSE-TOKEN--> {JsonSerializer.Serialize(response)}");

                return Ok(new { Success = true });
            }
            catch (System.Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest();
            }
        }

        #region PrivateMethods
        private async Task<FacebookAccessTokenApp?> GetAppToken()
        {
            try
            {   //TODO agregar a los appsettings            
                string appId = "1013386267230345";
                //TODO agregar a los appsettings
                string appSecret = "5e938468945fc83ab791e7e2c4be90c0";

                string urlToken = $"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials";

                HttpResponseMessage response = await _httpClient.GetAsync(urlToken);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogInformation(JsonSerializer.Serialize(response));
                    throw new Exception("Error al obtener el token de acceso a la API de Facebook.");
                }

                // Leer la respuesta como JSON
                string jsonResponse = await response.Content.ReadAsStringAsync();

                FacebookAccessTokenApp? appToken=JsonSerializer.Deserialize<FacebookAccessTokenApp>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return appToken;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion
    }

    public class FacebookTokenDto
    {
        public string Token { get; set; } = string.Empty;
    }
    public class FacebookUser
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string First_name { get; set; } = string.Empty;
        public string Last_name { get; set; } = string.Empty;
    }

    public class FacebookAccessTokenApp
    {
        public string Access_token { get; set; } = string.Empty;
        public string Token_type { get; set; } = string.Empty;
    }
}
