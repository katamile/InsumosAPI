using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.LaboratorioRepository
{
    public interface ILaboratorioRepository
    {
        Task<MessageInfoDTO> CrearNuevoLaboratorio(LaboratorioDTO laboratorio);
        Task EliminarLaboratorioAsync(long id);
        Task<List<Laboratorio>> GetAll();
        Task<Laboratorio> GetById(long id);
        Task ModificarLaboratorioAsync(Laboratorio laboratorio);
        Task<Laboratorio> ObtenerPorNombreAsync(string nombre);
        Task SaveChangesAsync();
    }
}
