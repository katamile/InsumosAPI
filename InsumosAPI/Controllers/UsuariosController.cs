using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Usuario>> GetAll() 
        { 
            return await _usuarioService.GetAll(); 
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(
            [FromBody] UsuarioLoginRequest request)
        {
            var token = await _usuarioService.ValidarCredencialesAsync(request);
            if (token == null)
                return Unauthorized("Nombre de usuario o contraseña incorrectos.");

            return Ok(new { token });
        }

        [HttpPost("cambiar-contraseña")]
        public async Task<ActionResult> CambiarContraseña(
            [FromBody] CambiarContraseñaRequest request)
        {
            var resultado = await _usuarioService.CambiarContraseñaAsync(request.Username, request.NuevaContraseña);
            if (!resultado)
                return BadRequest("No se pudo cambiar la contraseña.");

            return Ok("Contraseña cambiada exitosamente.");
        }

    }
}
