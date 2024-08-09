using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.ComprasRepository;
using InsumosAPI.Repositories.MovimientoInventarioRepository;
using InsumosAPI.Repositories.ProveedorRepository;
using InsumosAPI.Services.ProductoService;
using InsumosAPI.Services.ProveedorService;
using InsumosAPI.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace InsumosAPI.Services.ComprasService
{
    public class ComprasService : IComprasService
    {
        private readonly IComprasRepository _comprasRepository;
        private readonly IProductoService _productoService;
        private readonly IMovimientoInventarioRepository _movimientoInventarioRepository;
        private readonly IProveedorService _proveedorService;
        
        public ComprasService(IComprasRepository comprasRepository, 
                                IProductoService productoService,
                                IMovimientoInventarioRepository movimientoInventarioRepository,
                                IProveedorService proveedorService)
        {
            _comprasRepository = comprasRepository;
            _productoService = productoService;
            _movimientoInventarioRepository = movimientoInventarioRepository;
            _proveedorService = proveedorService;
        }

        public async Task<List<CompraDTO>> GetAll()
        {
            var compras = await _comprasRepository.GetAll();
            return compras.Select(c => new CompraDTO
            {
                IdCompra = c.IdCompra,
                IdProveedor = c.IdProveedor,
                Proveedor = c.Proveedor.Nombre,
                FechaCompra = c.FechaCompra,
                Subtotal = c.Subtotal,
                IvaPor = c.IvaPor,
                Iva = c.Iva,
                Total = c.Total,
                CompraDetalle = c.CompraDetalle.Select(detalle => new CompraDetalleDTO
                {
                    IdCompraDetalle = detalle.IdCompraDetalle,
                    IdProducto = detalle.IdProducto,
                    Producto = detalle.Producto?.Nombre ?? "",
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.Producto.PrecioCompra != 0 ? detalle.Producto.PrecioCompra : 0,
                    PrecioTotal = detalle.PrecioTotal
                }).ToList()
            }).ToList();
        }

        public async Task<CompraDTO> GetById(long idCompra)
        {
            var compra = await _comprasRepository.GetById(idCompra);

            if (compra == null)
            {
                throw new NotFoundException("Compra no encontrada.");
            }

            return new CompraDTO
            {
                IdCompra = compra.IdCompra,
                IdProveedor = compra.IdProveedor,
                Proveedor = compra.Proveedor.Nombre,
                FechaCompra = compra.FechaCompra,
                Subtotal = compra.Subtotal,
                IvaPor = compra.IvaPor,
                Iva = compra.Iva,
                Total = compra.Total,
                CompraDetalle = compra.CompraDetalle.Select(detalle => new CompraDetalleDTO
                {
                    IdCompraDetalle = detalle.IdCompraDetalle,
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    PrecioTotal = detalle.PrecioTotal
                }).ToList()
            };
        }

        public async Task<MessageInfoDTO> Insert(CompraDTO compraDTO)
        {
            try {

                var proveedor = await _proveedorService.GetById(compraDTO.IdProveedor) 
                                        ?? throw new NotFoundException("No se encuentra el proveedor.");

                var detallesValidacionResult = await ValidarDetalles(compraDTO.CompraDetalle);

                var subtotal = detallesValidacionResult.Subtotal;
                var ivaPor = compraDTO.IvaPor != 0 ? compraDTO.IvaPor : 15m;
                var iva = subtotal * ivaPor / 100;

                var compra = new CompraDTO
                {
                    IdProveedor = compraDTO.IdProveedor,
                    FechaCompra = compraDTO.FechaCompra,
                    Subtotal = subtotal,
                    IvaPor = ivaPor,
                    Iva = iva,
                    Total = subtotal + iva,
                    CompraDetalle = detallesValidacionResult.CompraDetalleDTOs.Select(detalle => new CompraDetalleDTO
                    {
                        IdCompraDetalle = detalle.IdCompraDetalle,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        PrecioTotal = detalle.PrecioTotal
                    }).ToList()
                };

                // Guarda la compra en la base de datos
                await _comprasRepository.Create(compra);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Compra insertada exitosamente.",
                    Success = "true"
                };
            } catch(Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task<DetallesValidacionResult> ValidarDetalles(List<CompraDetalleDTO> compraDetalleDTO)
        {
            decimal subtotal = 0;

            foreach (var detalle in compraDetalleDTO)
            {
                var producto = await _productoService.GetById(detalle.IdProducto)
                    ?? throw new NotFoundException("No se encuentra producto.");

                var inventario = await _movimientoInventarioRepository.GetProductLastMove(detalle.IdProducto);
                MovimientoInventarioDTO movimiento;

                if (inventario == null)
                {
                    movimiento = new MovimientoInventarioDTO
                    {
                        IdProducto = detalle.IdProducto,
                        TipoMovimiento = Globales.INGRESO,
                        StockProducto = detalle.Cantidad,
                        CantidadMovimiento = detalle.Cantidad,
                    };
                }
                else
                {
                    movimiento = new MovimientoInventarioDTO
                    {
                        IdProducto = detalle.IdProducto,
                        TipoMovimiento = Globales.INGRESO,
                        StockProducto = inventario.StockProducto + detalle.Cantidad,
                        CantidadMovimiento = detalle.Cantidad,
                    };
                }

                detalle.PrecioUnitario = producto.PrecioCompra;
                detalle.PrecioTotal = detalle.PrecioUnitario * detalle.Cantidad;
                subtotal += detalle.PrecioTotal;

                await _movimientoInventarioRepository.Create(movimiento);
            }

            return new DetallesValidacionResult
            {
                CompraDetalleDTOs = compraDetalleDTO,
                Subtotal = subtotal
            };
        }

    }
}
