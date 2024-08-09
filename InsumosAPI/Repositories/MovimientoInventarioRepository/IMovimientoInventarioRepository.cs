using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.MovimientoInventarioRepository
{
    public interface IMovimientoInventarioRepository
    {
        Task<MessageInfoDTO> Create(MovimientoInventarioDTO movimiento);
        Task<List<MovimientoInventarioDTO>> GetAll();
        Task<MovimientoInventarioDTO> GetById(long idMovimiento);
        Task<MovimientoInventarioDTO> GetProductLastMove(long idProducto);
    }
}
