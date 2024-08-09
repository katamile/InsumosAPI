using InsumosAPI.DTOs;
using InsumosAPI.Services.ProductoService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var productos = await _productoService.GetAll();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var producto = await _productoService.GetById(id);
            return Ok(producto);
        }

        [HttpGet("consultaPorNombre")]
        public async Task<IActionResult> GetByName([FromQuery] string nombre)
        {
            var producto = await _productoService.GetByNombre(nombre);
            return Ok(producto);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoDTO request)
        {

            var result = await _productoService.CrearProductoAsync(request);
            return Ok(result);
        }

        [HttpPut("modificar")]
        public async Task<IActionResult> ModificarProducto([FromBody] ProductoDTO request)
        {
            var result = await _productoService.ModificarProductoAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var result = await _productoService.EliminarProductoAsync(id);
            return Ok(result);
        }
    }
}
