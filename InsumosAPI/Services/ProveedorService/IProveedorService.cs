using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.ProveedorService
{
    public interface IProveedorService
    {
        Task<MessageInfoDTO> CrearProveedorAsync(ProveedorDTO request);
        Task<MessageInfoDTO> EliminarProveedorAsync(long id);
        Task<List<ProveedorDTO>> GetAll();
        Task<ProveedorDTO> GetById(long id);
        Task<ProveedorDTO> GetByNombre(string nombre);
        Task<MessageInfoDTO> ModificarProveedorAsync(ProveedorDTO request);
    }
}
