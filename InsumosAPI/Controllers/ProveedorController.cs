using InsumosAPI.DTOs;
using InsumosAPI.Services.ProveedorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _ProveedorService;

        public ProveedorController(IProveedorService ProveedorService)
        {
            _ProveedorService = ProveedorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Proveedors = await _ProveedorService.GetAll();
            return Ok(Proveedors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var Proveedors = await _ProveedorService.GetById(id);
                return Ok(Proveedors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("consultaPorNombre")]
        public async Task<IActionResult> GetByName([FromQuery] string nombre)
        {
            try
            {
                var Proveedors = await _ProveedorService.GetByNombre(nombre);
                return Ok(Proveedors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearProveedor([FromBody] ProveedorDTO request)
        {
            try
            {
                var result = await _ProveedorService.CrearProveedorAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarProveedor([FromBody] ProveedorDTO request)
        {
            try
            {
                var result = await _ProveedorService.ModificarProveedorAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            try
            {
                var result = await _ProveedorService.EliminarProveedorAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
