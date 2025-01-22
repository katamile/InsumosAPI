using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.MarcaRepository
{
    public interface IMarcaRepository
    {
        Task<MessageInfoDTO> CrearNuevoMarca(MarcaDTO marca);
        Task EliminarMarcaAsync(long id);
        Task<List<Marca>> GetAll();
        Task<Marca> GetById(long id);
        Task ModificarMarcaAsync(Marca marca);
        Task<Marca> ObtenerPorNombreAsync(string nombre);
        Task SaveChangesAsync();
    }
}
