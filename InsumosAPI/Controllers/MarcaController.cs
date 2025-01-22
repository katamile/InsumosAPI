using InsumosAPI.DTOs;
using InsumosAPI.Services.MarcaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _MarcaService;

        public MarcaController(IMarcaService MarcaService)
        {
            _MarcaService = MarcaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Marcas = await _MarcaService.GetAll();
            return Ok(Marcas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Marcas = await _MarcaService.GetById(id);
            return Ok(Marcas);
        }

        [HttpGet("consultaPorNombre")]
        public async Task<IActionResult> GetByName([FromQuery]string nombre)
        {
            var Marcas = await _MarcaService.GetByNombre(nombre);
            return Ok(Marcas);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearMarca([FromBody] MarcaDTO request)
        {
            var result = await _MarcaService.CrearMarcaAsync(request);
            return Ok(result);
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarMarca([FromBody] MarcaDTO request)
        {
            var result = await _MarcaService.ModificarMarcaAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var result = await _MarcaService.EliminarMarcaAsync(id);
            return Ok(result);
        }
    }
}
