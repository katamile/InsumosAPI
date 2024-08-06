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
            try
            {
                var Laboratorios = await _LaboratorioService.GetById(id);
                return Ok(Laboratorios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("consultaPorNombre")]
        public async Task<IActionResult> GetByName([FromQuery]string nombre)
        {
            try
            {
                var Laboratorios = await _LaboratorioService.GetByNombre(nombre);
                return Ok(Laboratorios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearLaboratorio([FromBody] LaboratorioDTO request)
        {
            try
            {
                var result = await _LaboratorioService.CrearLaboratorioAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarLaboratorio([FromBody] LaboratorioDTO request)
        {
            try
            {
                var result = await _LaboratorioService.ModificarLaboratorioAsync(request);
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
                var result = await _LaboratorioService.EliminarLaboratorioAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
