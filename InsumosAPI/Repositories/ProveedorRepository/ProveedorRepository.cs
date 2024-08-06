using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.ProveedorRepository
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public ProveedorRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Proveedor>> GetAll()
        {
            return await _contexto.Proveedores
                .Where(p => p.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran proveedores activos.");
        }

        public async Task<Proveedor> GetById(long id)
        {
            return await _contexto.Proveedores.FirstOrDefaultAsync(p => p.IdProveedor == id && p.Estado == Globales.ACTIVO)
                                             ?? throw new NotFoundException("No se encuentra el proveedor.");
        }

        public async Task<Proveedor> ObtenerPorNombreAsync(string nombre)
        {
            return await _contexto.Proveedores.FirstOrDefaultAsync(p => p.Nombre == nombre);
        }

        public async Task<MessageInfoDTO> CrearNuevoProveedor(ProveedorDTO proveedor)
        {
            try
            {
                var proveedorSave = new Proveedor
                {
                    Nombre = proveedor.Nombre,
                    Telefono = proveedor.Telefono,
                    Direccion = proveedor.Direccion,
                    Estado = Globales.ACTIVO
                };
                await _contexto.Proveedores.AddAsync(proveedorSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Proveedor creado exitosamente.",
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

        public async Task<MessageInfoDTO> ModificarProveedorAsync(Proveedor proveedorDTO)
        {
            try
            {
                var proveedor = await GetById(proveedorDTO.IdProveedor);
                if (proveedor == null)
                {
                    throw new NotFoundException("Proveedor no encontrado.");
                }

                proveedor.Nombre = proveedorDTO.Nombre;
                proveedor.Telefono = proveedorDTO.Telefono;
                proveedor.Direccion = proveedorDTO.Direccion;

                _contexto.Proveedores.Update(proveedor);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Proveedor modificado exitosamente.",
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

        public async Task<MessageInfoDTO> EliminarProveedorAsync(long id)
        {
            try
            {
                var proveedor = await GetById(id);
                if (proveedor == null)
                {
                    throw new NotFoundException("Proveedor no encontrado.");
                }

                // Marcar la entidad como eliminada lógicamente
                proveedor.Estado = Globales.INACTIVO;
                proveedor.FechaEliminacion = DateTime.Now;
                proveedor.UsuarioEliminacion = _userAccessRepository.ObtenerUsuarioLogin();

                // Actualizar la entidad en el contexto
                _contexto.Proveedores.Update(proveedor);

                // Guardar los cambios
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Proveedor eliminado exitosamente.",
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

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
