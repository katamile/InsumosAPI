using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection.Emit;

namespace InsumosAPI.Repositories.MovimientoInventarioRepository
{
    public class MovimientoInventarioRepository : IMovimientoInventarioRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public MovimientoInventarioRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<MovimientoInventarioDTO>> GetAll()
        {
            var movInve = await _contexto.MovimientoInventarios
                .Where(c => c.Estado == Globales.ACTIVO)
                .Include(c=>c.Producto)
                .Select(c=>new MovimientoInventarioDTO
                {
                    IdMovimiento = c.IdMovimiento,
                    TipoMovimiento = c.TipoMovimiento,
                    IdProducto=c.IdProducto,
                    Producto = c.Producto.Nombre,
                    StockProducto = c.StockProducto,
                    CantidadMovimiento = c.CantidadMovimiento
                })
                .ToListAsync() ?? throw new NotFoundException("No se encuentran clientes activos.");

            return movInve;
        }

        public async Task<MovimientoInventarioDTO> GetById(long idMovimiento )
        {
            var movInve = await _contexto.MovimientoInventarios
                .Where(c => c.Estado == Globales.ACTIVO && c.IdMovimiento==idMovimiento)
                .Include(c => c.Producto)
                .Select(c => new MovimientoInventarioDTO
                {
                    IdMovimiento = c.IdMovimiento,
                    TipoMovimiento = c.TipoMovimiento,
                    IdProducto = c.IdProducto,
                    Producto = c.Producto.Nombre,
                    StockProducto = c.StockProducto,
                    CantidadMovimiento = c.CantidadMovimiento
                })
                .FirstOrDefaultAsync() ?? throw new NotFoundException("No se encuentran movimientos para este producto.");

            return movInve;
        }

        public async Task<MovimientoInventarioDTO> GetProductLastMove(long idProducto)
        {
            var movInve = await _contexto.MovimientoInventarios
                .Where(c => c.Estado == Globales.ACTIVO && c.IdProducto == idProducto)
                .Include(c => c.Producto)
                .Select(c => new MovimientoInventarioDTO
                {
                    IdMovimiento = c.IdMovimiento,
                    TipoMovimiento = c.TipoMovimiento,
                    IdProducto = c.IdProducto,
                    Producto = c.Producto.Nombre,
                    StockProducto = c.StockProducto,
                    CantidadMovimiento = c.CantidadMovimiento
                })
                .OrderByDescending(c => c.IdMovimiento)
                .FirstOrDefaultAsync();

            return movInve;
        }

        public async Task<MessageInfoDTO> Create(MovimientoInventarioDTO movimiento)
        {
            try
            {
                var movimientoSave = new MovimientoInventario
                {
                    IdProducto = movimiento.IdProducto,
                    TipoMovimiento = movimiento.TipoMovimiento,
                    StockProducto = movimiento.StockProducto,
                    CantidadMovimiento = movimiento.CantidadMovimiento
                };

                await _contexto.MovimientoInventarios.AddAsync(movimientoSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Movimiento Inventario generado con éxito.",
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
