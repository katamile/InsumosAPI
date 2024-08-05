using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Repositories.UsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAll();
        Task<Usuario> ObtenerPorUsernameAsync(string username);
        Task<Usuario> ObtenerUsuarioCambiar(string username);
        Task<Usuario> GetById(long id);
        Task<Usuario> ObtenerPorIdentificacionAsync(string identificacion);
        Task<MessageInfoDTO> CrearNuevoUsuario(UsuarioDTO usuario);
        Task ModificarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(long id);
        Task SaveChangesAsync();

    }
}
