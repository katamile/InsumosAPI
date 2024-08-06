using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.ProveedorRepository;
using InsumosAPI.Utils;
using System.Net;

namespace InsumosAPI.Services.ProveedorService
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<List<ProveedorDTO>> GetAll()
        {
            var proveedores = await _proveedorRepository.GetAll();
            return proveedores.Select(p => new ProveedorDTO
            {
                IdProveedor = p.IdProveedor,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Direccion = p.Direccion
            }).ToList();
        }

        public async Task<ProveedorDTO> GetById(long id)
        {
            var proveedor = await _proveedorRepository.GetById(id);

            return new ProveedorDTO
            {
                IdProveedor = proveedor.IdProveedor,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion
            };
        }

        public async Task<ProveedorDTO> GetByNombre(string nombre)
        {
            var proveedor = await _proveedorRepository.ObtenerPorNombreAsync(nombre);

            if (proveedor == null)
            {
                throw new NotFoundException("Proveedor no encontrado.");
            }

            return new ProveedorDTO
            {
                IdProveedor = proveedor.IdProveedor,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Direccion = proveedor.Direccion
            };
        }

        public async Task<MessageInfoDTO> CrearProveedorAsync(ProveedorDTO request)
        {
            try
            {
                // Validar la existencia del proveedor
                await ValidarInsert(request);

                // Crear el nuevo proveedor
                var proveedor = new ProveedorDTO
                {
                    Nombre = request.Nombre,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion,
                };

                await _proveedorRepository.CrearNuevoProveedor(proveedor);

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
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task<MessageInfoDTO> ModificarProveedorAsync(ProveedorDTO request)
        {
            var proveedor = await _proveedorRepository.GetById(request.IdProveedor);
            if (proveedor == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Proveedor no encontrado.",
                    Success = "false"
                };
            }

            var cambios = ValidarUpdate(request, proveedor);

            // Aplicar los cambios al proveedor solo si hay alguno
            if (cambios != null && (cambios.Nombre != null || cambios.Telefono != null || cambios.Direccion != null))
            {
                if (cambios.Nombre != null) proveedor.Nombre = cambios.Nombre;
                if (cambios.Telefono != null) proveedor.Telefono = cambios.Telefono;
                if (cambios.Direccion != null) proveedor.Direccion = cambios.Direccion;

                await _proveedorRepository.ModificarProveedorAsync(proveedor);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Proveedor modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el proveedor.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarProveedorAsync(long id)
        {
            try
            {
                await _proveedorRepository.EliminarProveedorAsync(id);
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

        private async Task<bool> ValidarInsert(ProveedorDTO request)
        {
            // Verificar si el proveedor ya está registrado
            var proveedorExistentePorNombre = await _proveedorRepository.ObtenerPorNombreAsync(request.Nombre);
            if (proveedorExistentePorNombre != null)
            {
                throw new UniqueFieldException("nombre");
            }

            return true;
        }

        private ProveedorDTO ValidarUpdate(ProveedorDTO request, Proveedor proveedor)
        {
            var cambios = new ProveedorDTO();

            if (!string.IsNullOrEmpty(request.Nombre) && request.Nombre != proveedor.Nombre)
            {
                cambios.Nombre = request.Nombre;
            }

            if (!string.IsNullOrEmpty(request.Telefono) && request.Telefono != proveedor.Telefono)
            {
                cambios.Telefono = request.Telefono;
            }

            if (!string.IsNullOrEmpty(request.Direccion) && request.Direccion != proveedor.Direccion)
            {
                cambios.Direccion = request.Direccion;
            }

            return cambios;
        }
    }
}
