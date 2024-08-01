using BCrypt.Net;
using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsumosAPI.Repositories.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly InsumosDBContext _contexto;

        public UsuarioRepository(InsumosDBContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _contexto.Usuarios
                .Where(u => u.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentra usuario");
        }

        public async Task<Usuario> ObtenerPorUsernameAsync(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username &&
                                                                u.Estado == Globales.ACTIVO) 
                                            ?? throw new NotFoundException("No se encuentra usuario");
        }

        public async Task<Usuario> ObtenerUsuarioCambiar(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username) 
                                           ?? throw new NotFoundException("No se encuentra usuario.");
        }

        public async Task<string> ValidarCredencialesAsync(UsuarioLoginRequest request)
        {
            var usuario = await ObtenerPorUsernameAsync(request.Username);

            if (usuario == null)
                return null;

            // Verificar la contraseña
            if (BCrypt.Net.BCrypt.Verify(request.Password, usuario.Contraseña))
            {
                // Si las credenciales son válidas, restablecer el contador de intentos fallidos
                usuario.IntentosFallidos = 0;

                // Generar el token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("JsonWebApiTokenWithSwaggerAuthorizationAuthenticationAspNetCore");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, usuario.Username),
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                await _contexto.SaveChangesAsync();
                return tokenString;
            }
            else
            {
                // Si las credenciales son inválidas, incrementar el contador de intentos fallidos
                if (usuario.IntentosFallidos == null)
                    usuario.IntentosFallidos = 1;
                else
                    usuario.IntentosFallidos++;

                // Bloquear el usuario si se exceden los intentos fallidos
                if (usuario.IntentosFallidos >= 3)
                {
                    usuario.Estado = Globales.BLOQUEADO;
                }

                await _contexto.SaveChangesAsync();
                return null;
            }
        }

        public async Task<bool> CambiarContraseñaAsync(CambiarContraseñaRequest request)
        {
            var usuario = await ObtenerUsuarioCambiar(request.Username);

            if (usuario == null)
                return false;

            // Actualizar la contraseña
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(request.NuevaContraseña);
            usuario.IntentosFallidos = 0;
            usuario.Estado = Globales.ACTIVO;
            await _contexto.SaveChangesAsync();
            return true;
        }
    }
}
