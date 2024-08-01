
namespace InsumosAPI.DTOs
{
    public class UsuarioDTO
    {
        public long IdUsuario { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Correo { get; set; }
        public string Username { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public int? IntentosFallidos { get; set; }
    }

    public class UsuarioLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CambiarContraseñaRequest
    {
        public string Username { get; set; }
        public string NuevaContraseña { get; set; }
    }

}
