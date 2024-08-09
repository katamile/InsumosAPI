using InsumosAPI.DTOs;
using InsumosAPI.Services.ComprasService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly IComprasService _comprasService;

        public ComprasController(IComprasService comprasService)
        {
            _comprasService = comprasService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimiento = await _comprasService.GetAll();
            return Ok(movimiento);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var movimiento = await _comprasService.GetById(id);
            return Ok(movimiento);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearMovimiento([FromBody] CompraDTO compraDTO)
        {
            var result = await _comprasService.Insert(compraDTO);
            return Ok(result);
        }
    }
}
