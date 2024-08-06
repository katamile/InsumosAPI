using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.ClienteService
{
    public interface IClienteService
    {
        Task<MessageInfoDTO> CrearClienteAsync(ClienteDTO request);
        Task<MessageInfoDTO> EliminarClienteAsync(long id);
        Task<List<ClienteDTO>> GetAll();
        Task<ClienteDTO> GetById(long id);
        Task<ClienteDTO> GetByIdentificacion(string identificacion);
        Task<MessageInfoDTO> ModificarClienteAsync(ClienteDTO request);
    }
}
