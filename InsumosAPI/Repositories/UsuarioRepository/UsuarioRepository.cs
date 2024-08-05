using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
                .ToListAsync() ?? throw new NotFoundException("No se encuentran usuarios activos.");
        }

        public async Task<Usuario> GetById(long id)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id && u.Estado == Globales.ACTIVO) 
                                                                ?? throw new NotFoundException("No se encuentran usuario.");
        }

        public async Task<Usuario> ObtenerPorUsernameAsync(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Estado==Globales.ACTIVO)
                                                                ?? throw new NotFoundException("No se encuentran usuario."); 
        }

        public async Task<Usuario> ObtenerUsuarioCambiar(string username)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Username == username) ?? throw new NotFoundException("No se encuentran usuarios.");
        }

        public async Task<Usuario> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _contexto.Usuarios.FirstOrDefaultAsync(u => u.Identificacion == identificacion) ?? throw new NotFoundException("No existen registros.");
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

            usuario.Estado = Globales.INACTIVO;
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
