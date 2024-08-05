using InsumosAPI.DTOs;
using InsumosAPI.Middleware.Models;

namespace InsumosAPI.Services.LoginService
{
    public interface ILoginService
    {
        Task<JWTRequest> LoginUsuario(UsuarioLoginRequest usuariosLoginDAO);
        Task<MessageInfoDTO> ValidarToken(string token);
        Task<JWTRequest> RefrescarToken(string token);
    }
}
