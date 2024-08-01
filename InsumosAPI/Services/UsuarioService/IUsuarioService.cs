using InsumosAPI.DTOs;
using InsumosAPI.Entities;

namespace InsumosAPI.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<string> ValidarCredencialesAsync(UsuarioLoginRequest request);
        Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña);
        Task<List<Usuario>> GetAll();
    }
}
