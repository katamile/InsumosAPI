using InsumosAPI.DTOs;

namespace InsumosAPI.Repositories.MovimientoInventarioRepository
{
    public interface IMovimientoInventarioRepository
    {
        Task<List<MovimientoInventarioDTO>> GetAll();
    }
}
