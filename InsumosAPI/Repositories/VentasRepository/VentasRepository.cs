using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.VentasRepository
{
    public class VentasRepository : IVentasRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public VentasRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Venta>> GetAll()
        {
            return await _contexto.Ventas
                .Where(c => c.Estado == Globales.ACTIVO)
                .Include(c=>c.Cliente)
                .Include(c => c.VentaDetalles)
                    .ThenInclude(c => c.Producto)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran facturas.");
        }

        public async Task<Venta> GetById(long idCompra)
        {
            return await _contexto.Ventas
                .Where(c => c.Estado == Globales.ACTIVO && c.IdVenta == idCompra)
                .Include(c => c.VentaDetalles)
                    .ThenInclude(c => c.Producto)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("No se encuentra factura.");
        }

        public async Task<MessageInfoDTO> Create(VentaDTO ventaDTO)
        {
            try
            {
                var ventaSave = new Venta
                {
                    IdCliente = ventaDTO.IdCliente,
                    FechaVenta = ventaDTO.FechaVenta,
                    Subtotal = ventaDTO.Subtotal,
                    IvaPor = ventaDTO.IvaPor,
                    Iva = ventaDTO.Iva,
                    Total = ventaDTO.Total,
                    VentaDetalles = ventaDTO.VentaDetalles.Select(detalle => new VentaDetalle
                    {
                        IdVentaDetalle = detalle.IdVentaDetalle,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        PrecioTotal = detalle.PrecioTotal
                    }).ToList()
                };

                await _contexto.Ventas.AddAsync(ventaSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Cliente creado exitosamente.",
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
    }
}
