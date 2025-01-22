using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.MarcaService
{
    public interface IMarcaService
    {
        Task<MessageInfoDTO> CrearMarcaAsync(MarcaDTO request);
        Task<MessageInfoDTO> EliminarMarcaAsync(long id);
        Task<List<MarcaDTO>> GetAll();
        Task<MarcaDTO> GetById(long id);
        Task<MarcaDTO> GetByNombre(string nombre);
        Task<MessageInfoDTO> ModificarMarcaAsync(MarcaDTO request);
    }
}
