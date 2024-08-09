using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LaboratorioRepository;
using InsumosAPI.Repositories.ProductoRepository;
using InsumosAPI.Services.LaboratorioService;
using System.Net;

namespace InsumosAPI.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly ILaboratorioService _laboratorioService;

        public ProductoService(IProductoRepository productoRepository, ILaboratorioService laboratorioService)
        {
            _productoRepository = productoRepository;
            _laboratorioService = laboratorioService;
        }

        public async Task<List<ProductoDTO>> GetAll()
        {
            var productos = await _productoRepository.GetAll();
            return productos.Select(p => new ProductoDTO
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                PrecioCompra = p.PrecioCompra,
                PrecioVenta = p.PrecioVenta,
                RutaImg = p.RutaImg,
                Stock = p.Stock,
            }).ToList();
        }

        public async Task<ProductoDTO> GetById(long id)
        {
            var producto = await _productoRepository.GetById(id);

            if (producto == null)
            {
                throw new NotFoundException("Producto no encontrado.");
            }

            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                PrecioCompra = producto.PrecioCompra,
                PrecioVenta = producto.PrecioVenta,
                RutaImg = producto.RutaImg,
                Stock = producto.Stock
            };
        }

        public async Task<ProductoDTO> GetByNombre(string nombre)
        {
            var producto = await _productoRepository.ObtenerPorNombreAsync(nombre);

            if (producto == null)
            {
                throw new NotFoundException("Producto no encontrado.");
            }

            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                PrecioCompra = producto.PrecioCompra,
                PrecioVenta = producto.PrecioVenta,
                RutaImg = producto.RutaImg,
                Stock = producto.Stock,
            };
        }

        public async Task<MessageInfoDTO> CrearProductoAsync(ProductoDTO request)
        {
            try
            {
                //Validar Laboratorio Existente
                var validarLab = await _laboratorioService.GetById(request.IdLaboratorio) 
                        ?? throw new NotFoundException("El laboratorio ingresado no existe");

                //Validar Laboratorio Existente
                var validarName = await _productoRepository.ObtenerPorNombreAsync(request.Nombre)
                        ?? throw new NotFoundException("El nombre ingresado ya existe.");

                // Crear el nuevo producto
                var producto = new ProductoDTO
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    IdLaboratorio = request.IdLaboratorio,
                    PrecioCompra = request.PrecioCompra,
                    PrecioVenta = request.PrecioVenta,
                    RutaImg = request.RutaImg,
                };

                await _productoRepository.CrearNuevoProducto(producto);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Producto creado exitosamente.",
                    Success = "true"
                };
            }
            catch (Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }
        public async Task<MessageInfoDTO> ModificarProductoAsync(ProductoDTO request)
        {
            var producto = await _productoRepository.GetByIdAsync(request.IdProducto);
            if (producto == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Producto no encontrado.",
                    Success = "false"
                };
            }

            var validarLab = await _laboratorioService.GetById(request.IdLaboratorio)
                    ?? throw new NotFoundException("El laboratorio ingresado no existe");

            var cambios = ValidarUpdate(request, producto);

            // Aplicar los cambios al producto solo si hay alguno
            if (cambios != null && (cambios.Nombre != null || cambios.Descripcion != null
                || cambios.PrecioCompra != null || cambios.PrecioVenta != null ||
                cambios.IdLaboratorio != null || cambios.RutaImg != null))
            {
                // Aplicar los cambios al producto
                if (cambios.Nombre != null) producto.Nombre = cambios.Nombre;
                if (cambios.Descripcion != null) producto.Descripcion = cambios.Descripcion;
                if (cambios.PrecioCompra != null) producto.PrecioCompra = cambios.PrecioCompra;
                if (cambios.PrecioVenta != null) producto.PrecioVenta = cambios.PrecioVenta;
                if (cambios.RutaImg != null) producto.RutaImg = cambios.RutaImg;
                if (cambios.IdLaboratorio != null) producto.IdLaboratorio = cambios.IdLaboratorio;

                await _productoRepository.ModificarProductoAsync(producto);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Producto modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el producto.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarProductoAsync(long id)
        {
            try
            {
                await _productoRepository.EliminarProductoAsync(id);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Producto eliminado exitosamente.",
                    Success = "true"
                };
            }
            catch (Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        private Producto ValidarUpdate(ProductoDTO request, Producto producto)
        {
            var cambios = new Producto();

            if (!string.IsNullOrEmpty(request.Nombre) && request.Nombre != producto.Nombre)
            {
                cambios.Nombre = request.Nombre;
            }

            if (!string.IsNullOrEmpty(request.Descripcion) && request.Nombre != producto.Descripcion)
            {
                cambios.Descripcion = request.Descripcion;
            }

            if (request.PrecioCompra != null && request.PrecioCompra != producto.PrecioCompra)
            {
                cambios.PrecioCompra = request.PrecioCompra;
            }

            if (request.PrecioVenta != null && request.PrecioVenta != producto.PrecioVenta)
            {
                cambios.PrecioVenta = request.PrecioVenta;
            }

            if (!string.IsNullOrEmpty(request.RutaImg) && request.RutaImg != producto.RutaImg)
            {
                cambios.RutaImg = request.RutaImg;
            }

            if (request.IdLaboratorio != null && request.IdLaboratorio != producto.IdLaboratorio)
            {
                cambios.IdLaboratorio = request.IdLaboratorio;
            }

            return cambios;
        }
    }
}
