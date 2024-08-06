using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.ClienteRepository
{
    public interface IClienteRepository
    {
        Task<MessageInfoDTO> CrearNuevoCliente(ClienteDTO cliente);
        Task EliminarClienteAsync(long id);
        Task<List<Cliente>> GetAll();
        Task<Cliente> GetById(long id);
        Task ModificarClienteAsync(Cliente cliente);
        Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion);
        Task SaveChangesAsync();
    }
}
