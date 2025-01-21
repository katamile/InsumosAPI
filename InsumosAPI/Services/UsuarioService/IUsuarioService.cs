using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;
using System.Threading.Tasks;

namespace InsumosAPI.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetAll();
        Task<UsuarioDTO> GetById(long id);
        Task<MessageInfoDTO> CambiarContraseñaAsync(CambiarContraseñaRequest request);
        Task<UsuarioDTO> GetByUsername(string username);
        Task<MessageInfoDTO> CrearUsuarioAsync(UsuarioDTO request);
        Task<MessageInfoDTO> ModificarUsuarioAsync(UsuarioDTO request);
        Task<MessageInfoDTO> EliminarUsuarioAsync(long id);
    }
}
