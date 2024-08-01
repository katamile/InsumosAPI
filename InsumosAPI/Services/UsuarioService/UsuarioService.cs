using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Repositories.UsuarioRepository;

namespace InsumosAPI.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<string> ValidarCredencialesAsync(UsuarioLoginRequest request)
        {
            //var usuario = await _usuarioRepository.ObtenerPorUsernameAsync(request.Username);

            return await _usuarioRepository.ValidarCredencialesAsync(request);
        }

        public async Task<bool> CambiarContraseñaAsync(string username, string nuevaContraseña)
        {
            var request = new CambiarContraseñaRequest
            {
                Username = username,
                NuevaContraseña = nuevaContraseña
            };

            return await _usuarioRepository.CambiarContraseñaAsync(request);
        }
    }
}
