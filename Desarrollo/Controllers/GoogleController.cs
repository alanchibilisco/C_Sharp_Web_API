using System.Text.Json;
using Desarrollo.Utils;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Desarrollo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleController : ControllerBase
    {

        private readonly string googleClientId = "654705958554-e05vliro90t1sfhoonnrem53hgojucgg.apps.googleusercontent.com";
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

                if (payload == null || !payload.EmailVerified)
                {
                    return Unauthorized();
                }
                // string strPayload = JsonSerializer.Serialize(payload);
                // _logger.LogInformation($"PAYLOAD--> {strPayload}");

                GoogleUser gu = new GoogleUser
                {
                    Id = payload.Subject,
                    Name = payload.Name,
                    GivenName = payload.GivenName,
                    FamilyName = payload.FamilyName,
                    Email = payload.Email
                };

                Usuario u=new Usuario
                {
                    IdProveedor=gu.Id,
                    NombreCompleto=gu.Name,
                    Nombre=gu.GivenName,
                    Apellido=gu.FamilyName,
                    CorreoElectronico=gu.Email,
                    TipoRegistro=TipoUsuarioRegistro.GOOGLE,
                    TipoUsuario=TipoUsuario.USUARIO,
                };

                _logger.LogInformation($"Usuario--> {JsonSerializer.Serialize(u)}");

                string appToken = "JOSUE-APP-TOKEN";
                return Ok(u);
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

    public class GoogleUser
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class GoogleTokenData
    {
        public string? Scope { get; set; } = null;
        public string? Prn { get; set; } = null;
        public string? HostedDomain { get; set; } = null;
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty;
        public string FamilyName { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public object? Locale { get; set; } = null;
        public string Issuer { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public object TargetAudience { get; set; } = string.Empty;
        public int ExpirationTimeSeconds { get; set; }
        public int NotBeforeTimeSeconds { get; set; }
        public int IssuedAtTimeSeconds { get; set; }
        public string JwtId { get; set; } = string.Empty;
        public object? Nonce { get; set; } = null;
        public object? Type { get; set; } = null;
        public IEnumerable<string> AudienceAsList { get; set; } = new List<string>();
    }
    public class Root
    {
        public object Scope { get; set; }
        public object Prn { get; set; }
        public object HostedDomain { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Picture { get; set; }
        public object Locale { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public object TargetAudience { get; set; }
        public int ExpirationTimeSeconds { get; set; }
        public int NotBeforeTimeSeconds { get; set; }
        public int IssuedAtTimeSeconds { get; set; }
        public string JwtId { get; set; }
        public object Nonce { get; set; }
        public object Type { get; set; }
        public List<string> AudienceAsList { get; set; }
    }
}
