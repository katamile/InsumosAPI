using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Services.ClienteService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var cliente = await _clienteService.GetById(id);
            return Ok(cliente);
        }

        [HttpGet("identificacion/{identificacion}")]
        public async Task<IActionResult> GetByIdentificacion(string identificacion)
        {
            var cliente = await _clienteService.GetByIdentificacion(identificacion);
            return Ok(cliente);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteDTO request)
        {
            var result = await _clienteService.CrearClienteAsync(request);
            return Ok(result);
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarCliente([FromBody] ClienteDTO request)
        {
            var result = await _clienteService.ModificarClienteAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var result = await _clienteService.EliminarClienteAsync(id);
            return Ok(result);
        }
    }
}
