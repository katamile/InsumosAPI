using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.VentasRepository;
using InsumosAPI.Repositories.MovimientoInventarioRepository;
using InsumosAPI.Services.ProductoService;
using InsumosAPI.Services.ProveedorService;
using InsumosAPI.Utils;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace InsumosAPI.Services.VentasService
{
    public class VentasService : IVentasService
    {
        private readonly IVentasRepository _ventasRepository;
        private readonly IProductoService _productoService;
        private readonly IMovimientoInventarioRepository _movimientoInventarioRepository;
        private readonly IProveedorService _proveedorService;

        public VentasService(IVentasRepository ventasRepository,
                                IProductoService productoService,
                                IMovimientoInventarioRepository movimientoInventarioRepository,
                                IProveedorService proveedorService)
        {
            _ventasRepository = ventasRepository;
            _productoService = productoService;
            _movimientoInventarioRepository = movimientoInventarioRepository;
            _proveedorService = proveedorService;
        }

        public async Task<List<VentaDTO>> GetAll()
        {
            var ventas = await _ventasRepository.GetAll();
            return ventas.Select(c => new VentaDTO
            {
                IdVenta = c.IdVenta,
                IdCliente = c.IdCliente,
                Cliente = c.Cliente.NombreCompleto,
                FechaVenta = c.FechaVenta,
                Subtotal = c.Subtotal,
                IvaPor = c.IvaPor,
                Iva = c.Iva,
                Total = c.Total,
                VentaDetalles = c.VentaDetalles.Select(detalle => new VentaDetalleDTO
                {
                    IdVentaDetalle = detalle.IdVentaDetalle,
                    IdProducto = detalle.IdProducto,
                    Producto = detalle.Producto?.Nombre ?? "",
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.Producto.PrecioVenta != 0 ? detalle.Producto.PrecioVenta : 0,
                    PrecioTotal = detalle.PrecioTotal
                }).ToList()
            }).ToList();
        }

        public async Task<VentaDTO> GetById(long idVenta)
        {
            var venta = await _ventasRepository.GetById(idVenta);

            if (venta == null)
            {
                throw new NotFoundException("Venta no encontrada.");
            }

            return new VentaDTO
            {
                IdVenta = venta.IdVenta,
                IdCliente = venta.IdCliente,
                Cliente = venta.Cliente.NombreCompleto,
                FechaVenta = venta.FechaVenta,
                Subtotal = venta.Subtotal,
                IvaPor = venta.IvaPor,
                Iva = venta.Iva,
                Total = venta.Total,
                VentaDetalles = venta.VentaDetalles.Select(detalle => new VentaDetalleDTO
                {
                    IdVentaDetalle = detalle.IdVentaDetalle,
                    IdProducto = detalle.IdProducto,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    PrecioTotal = detalle.PrecioTotal
                }).ToList()
            };
        }

        public async Task<MessageInfoDTO> Insert(VentaDTO ventaDTO)
        {
            try
            {

                var proveedor = await _proveedorService.GetById(ventaDTO.IdCliente)
                                        ?? throw new NotFoundException("No se encuentra el proveedor.");

                var detallesValidacionResult = await ValidarDetalles(ventaDTO.VentaDetalles);

                var subtotal = detallesValidacionResult.Subtotal;
                var ivaPor = ventaDTO.IvaPor != 0 ? ventaDTO.IvaPor : 15m;
                var iva = subtotal * ivaPor / 100;

                var venta = new VentaDTO
                {
                    IdCliente = ventaDTO.IdCliente, 
                    FechaVenta = ventaDTO.FechaVenta,
                    Subtotal = subtotal,
                    IvaPor = ivaPor,
                    Iva = iva,
                    Total = subtotal + iva,
                    VentaDetalles = detallesValidacionResult.VentaDetalleDTOs.Select(detalle => new VentaDetalleDTO
                    {
                        IdVentaDetalle = detalle.IdVentaDetalle,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        PrecioTotal = detalle.PrecioTotal
                    }).ToList()
                };

                // Guarda la venta en la base de datos
                await _ventasRepository.Create(venta);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Venta insertada exitosamente.",
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

        public async Task<DetallesVentaValidacionResult> ValidarDetalles(List<VentaDetalleDTO> ventaDetalleDTO)
        {

            decimal subtotal = 0;

            foreach (var detalle in ventaDetalleDTO)
            {
                var producto = await _productoService.GetById(detalle.IdProducto)
                    ?? throw new NotFoundException("No se encuentra producto.");

                if (producto.Stock == null || producto.Stock == 0)
                {
                    throw new NotFoundException($"No existe stock disponible para el producto: {producto.Nombre}.");
                }

                if (producto.Stock < detalle.Cantidad)
                {
                    throw new NotFoundException($"Stock insuficiente para el producto: {producto.Nombre}. Stock disponible: {producto.Stock}. ");
                }

                var inventario = await _movimientoInventarioRepository.GetProductLastMove(detalle.IdProducto);
                MovimientoInventarioDTO movimiento;

                if (inventario == null)
                {
                    throw new NotFoundException($"No existe stock disponible para el producto: {producto.Nombre}.");
                }
                else
                {
                    movimiento = new MovimientoInventarioDTO
                    {
                        IdProducto = detalle.IdProducto,
                        TipoMovimiento = Globales.EGRESO,
                        StockProducto = inventario.StockProducto - detalle.Cantidad,
                        CantidadMovimiento = detalle.Cantidad,
                    };
                }

                detalle.PrecioUnitario = producto.PrecioVenta;
                detalle.PrecioTotal = detalle.PrecioUnitario * detalle.Cantidad;
                subtotal += detalle.PrecioTotal;

                await _movimientoInventarioRepository.Create(movimiento);
            }

            return new DetallesVentaValidacionResult
            {
                VentaDetalleDTOs = ventaDetalleDTO,
                Subtotal = subtotal
            };
        }
    }
}
