using InsumosAPI.DTOs;
using InsumosAPI.Services.LaboratorioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LaboratorioController : ControllerBase
    {
        private readonly ILaboratorioService _LaboratorioService;

        public LaboratorioController(ILaboratorioService LaboratorioService)
        {
            _LaboratorioService = LaboratorioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Laboratorios = await _LaboratorioService.GetAll();
            return Ok(Laboratorios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var Laboratorios = await _LaboratorioService.GetById(id);
            return Ok(Laboratorios);
        }

        [HttpGet("consultaPorNombre")]
        public async Task<IActionResult> GetByName([FromQuery]string nombre)
        {
            var Laboratorios = await _LaboratorioService.GetByNombre(nombre);
            return Ok(Laboratorios);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearLaboratorio([FromBody] LaboratorioDTO request)
        {
            var result = await _LaboratorioService.CrearLaboratorioAsync(request);
            return Ok(result);
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarLaboratorio([FromBody] LaboratorioDTO request)
        {
            var result = await _LaboratorioService.ModificarLaboratorioAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var result = await _LaboratorioService.EliminarLaboratorioAsync(id);
            return Ok(result);
        }
    }
}
