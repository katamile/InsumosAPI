using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;

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
                .Select(c=>new MovimientoInventarioDTO
                {
                    IdMovimiento = c.IdMovimiento,
                    TipoMovimiento = c.TipoMovimiento,
                    Producto = c.Producto.Nombre,
                    StockProducto = c.StockProducto,
                    CantidadMovimiento = c.CantidadMovimiento
                })
                .ToListAsync() ?? throw new NotFoundException("No se encuentran clientes activos.");

            return movInve;
        }

    }
}
