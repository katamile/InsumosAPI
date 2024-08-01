using InsumosAPI.DTOs;
using InsumosAPI.Entities;

namespace InsumosAPI.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> ObtenerPorUsernameAsync(string username);
        Task<string> ValidarCredencialesAsync(UsuarioLoginRequest request);
        Task<bool> CambiarContraseñaAsync(CambiarContraseñaRequest request);
    }
}
