using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Services.UsuarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAll();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var usuarios = await _usuarioService.GetById(id);
            return Ok(usuarios);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO request)
        {
            var result = await _usuarioService.CrearUsuarioAsync(request);
            return Ok(result);
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarUsuario([FromBody] UsuarioDTO request)
        {
            var result = await _usuarioService.ModificarUsuarioAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var result = await _usuarioService.EliminarUsuarioAsync(id);
            return Ok(result);
        }
    }
}
