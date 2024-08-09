using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Repositories.UsuarioRepository;
using InsumosAPI.Middleware.Exceptions.NotFound;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InsumosAPI.Utils;
using InsumosAPI.Middleware.Models;
using System.Net;
using Azure.Core;
using InsumosAPI.Middleware.Exceptions.BadRequest;

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

        public async Task<UsuarioDTO> GetById(long id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            var usuarioDTO = new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Identificacion = usuario.Identificacion,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Username = usuario.Username,
                Contraseña = usuario.Contraseña,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            };
            
            return usuarioDTO;
        }

        public async Task<UsuarioDTO> GetByUsername(string username)
        {
            var usuario = await _usuarioRepository.ObtenerPorUsernameAsync(username);

            var usuarioDTO = new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Identificacion = usuario.Identificacion,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Username = usuario.Username,
                Contraseña = usuario.Contraseña,
                Correo = usuario.Correo,
                Rol = usuario.Rol
            } ?? throw new NotFoundException("Usuario no encontrado."); 

            return usuarioDTO;
        }

        public async Task<MessageInfoDTO> CambiarContraseñaAsync(CambiarContraseñaRequest request)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioCambiar(request.Username);
            if (usuario == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Usuario no encontrado.",
                    Success = "false"
                };
            }

            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(request.NuevaContraseña);
            usuario.IntentosFallidos = 0;
            usuario.Estado = Globales.ACTIVO;
            await _usuarioRepository.SaveChangesAsync();

            return new MessageInfoDTO
            {
                Status = HttpStatusCode.OK,
                Message = "Contraseña cambiada exitosamente.",
                Success = "true"
            };
        }

        public async Task<MessageInfoDTO> CrearUsuarioAsync(UsuarioDTO request)
        {
            try
            {
                // Validar la existencia del usuario
                await ValidarInsert(request);

                // Crear el nuevo usuario
                var usuario = new UsuarioDTO
                {
                    Identificacion = request.Identificacion,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Username = request.Username,
                    Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña),
                    Correo = request.Correo,
                    Rol = request.Rol
                };

                await _usuarioRepository.CrearNuevoUsuario(usuario);

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
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task<MessageInfoDTO> ModificarUsuarioAsync(UsuarioDTO request)
        {
            var usuario = await _usuarioRepository.GetById(request.IdUsuario);
            if (usuario == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Usuario no encontrado.",
                    Success = "false"
                };
            }

            var cambios = ValidarUpdate(request, usuario);

            // Aplicar los cambios al usuario solo si hay alguno
            if (cambios != null && (cambios.Contraseña != null || cambios.Nombres != null || cambios.Apellidos != null ||
                                     cambios.Correo != null || cambios.Rol != null))
            {
                if (cambios.Contraseña != null) usuario.Contraseña = cambios.Contraseña;
                if (cambios.Nombres != null) usuario.Nombres = cambios.Nombres;
                if (cambios.Apellidos != null) usuario.Apellidos = cambios.Apellidos;
                if (cambios.Correo != null) usuario.Correo = cambios.Correo;
                if (cambios.Rol != null) usuario.Rol = cambios.Rol;

                await _usuarioRepository.ModificarUsuarioAsync(usuario);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Usuario modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el usuario.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarUsuarioAsync(long id)
        {
            try
            {
                await _usuarioRepository.EliminarUsuarioAsync(id);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Usuario eliminado exitosamente.",
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

        private async Task<bool> ValidarInsert(UsuarioDTO request)
        {
            // Verificar si el nombre de usuario ya existe
            var usuarioExistentePorUsername = await _usuarioRepository.ObtenerPorUsernameAsync(request.Username);
            if (usuarioExistentePorUsername != null)
            {
                throw new UniqueFieldException("usuario");
            }

            // Verificar si la cédula ya está registrada
            var usuarioExistentePorIdentificacion = await _usuarioRepository.ObtenerPorIdentificacionAsync(request.Identificacion);
            if (usuarioExistentePorIdentificacion != null)
            {
                throw new UniqueFieldException("cédula");
            }

            // Verificar la validez de la identificación
            bool esIdentificacionValida = VerificaIdentificacion(request.Identificacion);
            if (!esIdentificacionValida)
            {
                throw new InvalidFieldException("La identificación proporcionada no es válida.");
            }

            return true;
        }

        public static bool VerificaIdentificacion(string identificacion)
        {
            if (string.IsNullOrWhiteSpace(identificacion) || identificacion.Length < 10)
            {
                return false;
            }

            identificacion = identificacion.Trim();
            var valced = identificacion.ToCharArray();
            int provincia = int.Parse(new string(valced, 0, 2));

            if (provincia <= 0 || provincia >= 31) //Permitir cédulas emitidas en Consulados
            {
                return false;
            }

            int tercerDigito = int.Parse(valced[2].ToString());
            if (tercerDigito < 6)
            {
                return VerificaCedula(valced);
            }
            else if (tercerDigito == 6)
            {
                if (valced.Length == 13)
                {
                    // Validar RUC si el tercer dígito es 6 y la longitud es 13
                    return true;
                }
                else
                {
                    return VerificaCedula(valced);
                }
            }
            else if (tercerDigito == 8)
            {
                if (valced.Length == 13)
                {
                    return VerificaSectorPublico(valced);
                }
                return false;
            }
            else if (tercerDigito == 9)
            {
                return true;
            }

            return false;
        }

        public static bool VerificaRUCPersonaNatural(char[] validarCedula)
        {
            try
            {
                const string establecimiento = "001";
                var establecimientoRUC = new string(validarCedula, 10, 3);
                return establecimientoRUC.Equals(establecimiento) && VerificaCedula(validarCedula);
            }
            catch
            {
                return false;
            }
        }

        private static bool VerificaCedula(char[] validarCedula)
        {
            int sumaPares = 0, sumaImpares = 0;
            for (int i = 0; i < 9; i += 2)
            {
                int valor = 2 * (validarCedula[i] - '0');
                if (valor > 9) valor -= 9;
                sumaPares += valor;
            }

            for (int i = 1; i < 9; i += 2)
            {
                sumaImpares += (validarCedula[i] - '0');
            }

            int sumaTotal = sumaPares + sumaImpares;
            int verifi = (sumaTotal % 10 == 0) ? 0 : 10 - (sumaTotal % 10);
            return verifi == (validarCedula[9] - '0');
        }

        private static bool VerificaSectorPublico(char[] validarCedula)
        {
            int sumaCoeficientes = 0;
            int[] coeficientes = { 3, 2, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 8; i++)
            {
                int producto = (validarCedula[i] - '0') * coeficientes[i];
                sumaCoeficientes += producto;
            }

            int verificador = (sumaCoeficientes % 11 == 0) ? 0 : 11 - (sumaCoeficientes % 11);
            return verificador == (validarCedula[8] - '0');
        }

        private UsuarioDTO ValidarUpdate(UsuarioDTO request, Usuario usuario)
        {
            var cambios = new UsuarioDTO();

            if (request.Contraseña != null && !BCrypt.Net.BCrypt.Verify(request.Contraseña, usuario.Contraseña))
            {
                cambios.Contraseña = BCrypt.Net.BCrypt.HashPassword(request.Contraseña);
            }

            if (!string.IsNullOrEmpty(request.Nombres) && request.Nombres != usuario.Nombres)
            {
                cambios.Nombres = request.Nombres;
            }

            if (!string.IsNullOrEmpty(request.Apellidos) && request.Apellidos != usuario.Apellidos)
            {
                cambios.Apellidos = request.Apellidos;
            }

            if (!string.IsNullOrEmpty(request.Correo) && request.Correo != usuario.Correo)
            {
                cambios.Correo = request.Correo;
            }

            if (!string.IsNullOrEmpty(request.Rol) && request.Rol != usuario.Rol)
            {
                cambios.Rol = request.Rol;
            }

            return cambios;
        }

    }
}
