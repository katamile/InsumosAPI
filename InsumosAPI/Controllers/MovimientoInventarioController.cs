using InsumosAPI.DTOs;
using InsumosAPI.Services.MovimientoInventarioService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovimientoInventarioController : ControllerBase
    {
        private readonly IMovimientoInventarioService _movimientoInventarioService;

        public MovimientoInventarioController(IMovimientoInventarioService movimientoInventarioService)
        {
            _movimientoInventarioService = movimientoInventarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimiento = await _movimientoInventarioService.GetAll();
            return Ok(movimiento);
        }


        [HttpPost("crear")]
        public async Task<IActionResult> CrearMovimiento([FromBody] MovimientoInventarioDTO movimientoInventarioDTO)
        {
            var result = await _movimientoInventarioService.Insert(movimientoInventarioDTO);
            return Ok(result);
        }
    }
}
