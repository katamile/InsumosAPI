using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.UsuarioRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public UsuarioRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _contexto.Usuarios
                .Where(u => u.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran usuarios activos.");
        }

        public async Task<Usuario> GetById(long id)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.Estado == Globales.ACTIVO) 
                                                                ?? throw new NotFoundException("No se encuentran usuario.");
        }

        public async Task<Usuario> ObtenerPorUsernameAsync(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Estado==Globales.ACTIVO); 
        }

        public async Task<Usuario> ObtenerUsuarioCambiar(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Usuario> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Identificacion == identificacion);
        }

        public async Task<MessageInfoDTO> CrearNuevoUsuario(UsuarioDTO usuario)
        {
            try
            {
                var usuarioSave = new Usuario
                {
                    Identificacion = usuario.Identificacion,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Username = usuario.Username,
                    Contraseña = usuario.Contraseña,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol
                };
                await _contexto.Usuarios.AddAsync(usuarioSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Usuario creado exitosamente.",
                    Success = "true"
                };
            }
            catch (Exception ex)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task ModificarUsuarioAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(long id)
        {
            var usuario = await GetById(id);
            if (usuario == null)
            {
                throw new NotFoundException("Usuario no encontrado.");
            }

            // Marcar la entidad como eliminada lógicamente
            usuario.Estado = Globales.INACTIVO;
            usuario.FechaEliminacion = DateTime.Now;
            usuario.UsuarioEliminacion = _userAccessRepository.ObtenerUsuarioLogin();

            // Actualizar la entidad en el contexto
            _contexto.Usuarios.Update(usuario);

            // Guardar los cambios
            await _contexto.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
