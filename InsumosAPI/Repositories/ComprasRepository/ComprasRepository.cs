using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.ComprasRepository
{
    public class ComprasRepository : IComprasRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public ComprasRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Compra>> GetAll()
        {
            return await _contexto.Compras
                .Where(c => c.Estado == Globales.ACTIVO)
                .Include(c=>c.Proveedor)
                .Include(c=>c.CompraDetalle)
                    .ThenInclude(c=>c.Producto)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran facturas.");
        }

        public async Task<Compra> GetById(long idCompra)
        {
            return await _contexto.Compras
                .Where(c => c.Estado == Globales.ACTIVO && c.IdCompra == idCompra)
                .Include(c => c.CompraDetalle)
                    .ThenInclude(c => c.Producto)
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("No se encuentra factura.");
        }

        public async Task<MessageInfoDTO> Create(CompraDTO compraDTO)
        {
            try
            {
                var compraSave = new Compra
                {
                    IdProveedor = compraDTO.IdProveedor,
                    FechaCompra = compraDTO.FechaCompra,
                    Subtotal = compraDTO.Subtotal,
                    IvaPor = compraDTO.IvaPor,
                    Iva = compraDTO.Iva,
                    Total = compraDTO.Total,
                    CompraDetalle = compraDTO.CompraDetalle.Select(detalle => new CompraDetalle
                    {
                        IdCompraDetalle = detalle.IdCompraDetalle,
                        IdProducto = detalle.IdProducto,
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario,
                        PrecioTotal = detalle.PrecioTotal
                    }).ToList()
                };

                await _contexto.Compras.AddAsync(compraSave);
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
