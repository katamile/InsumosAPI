using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.MovimientoInventarioService
{
    public interface IMovimientoInventarioService
    {
        Task<List<MovimientoInventarioDTO>> GetAll();
        Task<MessageInfoDTO> Insert(MovimientoInventarioDTO movimientoInventarioDTO);
    }
}
