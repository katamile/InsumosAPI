using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.ProveedorRepository
{
    public interface IProveedorRepository
    {
        Task<MessageInfoDTO> CrearNuevoProveedor(ProveedorDTO proveedor);
        Task<MessageInfoDTO> EliminarProveedorAsync(long id);
        Task<List<Proveedor>> GetAll();
        Task<Proveedor> GetById(long id);
        Task<MessageInfoDTO> ModificarProveedorAsync(Proveedor proveedorDTO);
        Task<Proveedor> ObtenerPorNombreAsync(string nombre);
        Task SaveChangesAsync();
    }
}
