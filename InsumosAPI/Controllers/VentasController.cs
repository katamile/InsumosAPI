using InsumosAPI.DTOs;
using InsumosAPI.Services.VentasService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentasController : ControllerBase
    {
        private readonly IVentasService _ventasService;

        public VentasController(IVentasService ventasService)
        {
            _ventasService = ventasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimiento = await _ventasService.GetAll();
            return Ok(movimiento);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var movimiento = await _ventasService.GetById(id);
            return Ok(movimiento);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearMovimiento([FromBody] VentaDTO ventaDTO)
        {
            var result = await _ventasService.Insert(ventaDTO);
            return Ok(result);
        }
    }
}
