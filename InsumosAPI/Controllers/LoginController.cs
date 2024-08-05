using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Services.LoginService;
using InsumosAPI.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InsumosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUsuarioService _usuarioService;

        public LoginController(ILoginService loginService, IUsuarioService usuarioService)
        {
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginRequest loginRequest)
        {
            if (loginRequest == null)
                return BadRequest("Datos de inicio de sesión inválidos.");

            try
            {
                var jwtRequest = await _loginService.LoginUsuario(loginRequest);
                return Ok(jwtRequest);
            }
            catch (InvalidaUserOrPasswordException)
            {
                return Unauthorized("Nombre de usuario o contraseña inválidos.");
            }
            catch (InvalidSintaxisException)
            {
                return BadRequest("Datos de inicio de sesión inválidos.");
            }
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token no proporcionado.");

            try
            {
                var validationResult = await _loginService.ValidarToken(token);
                return Ok(validationResult);
            }
            catch (InvalidTokenException)
            {
                return Unauthorized("Token inválido.");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token no proporcionado.");

            try
            {
                var jwtRequest = await _loginService.RefrescarToken(token);
                return Ok(jwtRequest);
            }
            catch (InvalidTokenException)
            {
                return Unauthorized("Token inválido.");
            }
        }

        [HttpPost("cambiar-contraseña")]
        public async Task<IActionResult> CambiarContraseña([FromBody] CambiarContraseñaRequest request)
        {
            var resultado = await _usuarioService.CambiarContraseñaAsync(request);
            return StatusCode((int)resultado.Status, resultado);
        }

    }
}
