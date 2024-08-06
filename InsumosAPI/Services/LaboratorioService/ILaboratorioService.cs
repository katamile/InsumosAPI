using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.LaboratorioService
{
    public interface ILaboratorioService
    {
        Task<MessageInfoDTO> CrearLaboratorioAsync(LaboratorioDTO request);
        Task<MessageInfoDTO> EliminarLaboratorioAsync(long id);
        Task<List<LaboratorioDTO>> GetAll();
        Task<LaboratorioDTO> GetById(long id);
        Task<LaboratorioDTO> GetByNombre(string nombre);
        Task<MessageInfoDTO> ModificarLaboratorioAsync(LaboratorioDTO request);
    }
}
