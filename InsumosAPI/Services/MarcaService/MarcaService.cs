using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.MarcaRepository;
using InsumosAPI.Services.MarcaService;
using System.Net;

namespace InsumosAPI.Services.MarcaService
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<List<MarcaDTO>> GetAll()
        {
            var marcas = await _marcaRepository.GetAll();
            return marcas.Select(l => new MarcaDTO
            {
                Id = l.IdMarca,
                Nombre = l.Nombre,
                Telefono = l.Telefono,
                Direccion = l.Direccion
            }).ToList();
        }

        public async Task<MarcaDTO> GetById(long id)
        {
            var marca = await _marcaRepository.GetById(id);

            return new MarcaDTO
            {
                Id = marca.IdMarca,
                Nombre = marca.Nombre,
                Telefono = marca.Telefono,
                Direccion = marca.Direccion
            };
        }

        public async Task<MarcaDTO> GetByNombre(string nombre)
        {
            var marca = await _marcaRepository.ObtenerPorNombreAsync(nombre);

            if (marca == null)
            {
                throw new NotFoundException("Marca no encontrado.");
            }

            return new MarcaDTO
            {
                Id = marca.IdMarca,
                Nombre = marca.Nombre,
                Telefono = marca.Telefono,
                Direccion = marca.Direccion
            };
        }

        public async Task<MessageInfoDTO> CrearMarcaAsync(MarcaDTO request)
        {
            try
            {
                // Validar la existencia del marca
                await ValidarInsert(request);

                // Crear el nuevo marca
                var marca = new MarcaDTO
                {
                    Nombre = request.Nombre,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion
                };

                await _marcaRepository.CrearNuevoMarca(marca);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Marca creado exitosamente.",
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

        public async Task<MessageInfoDTO> ModificarMarcaAsync(MarcaDTO request)
        {
            var marca = await _marcaRepository.GetById(request.Id);
            if (marca == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Marca no encontrado.",
                    Success = "false"
                };
            }

            var cambios = ValidarUpdate(request, marca);

            // Aplicar los cambios al marca solo si hay alguno
            if (cambios != null && (cambios.Nombre != null || cambios.Telefono != null || cambios.Direccion != null))
            {
                if (cambios.Nombre != null) marca.Nombre = cambios.Nombre;
                if (cambios.Telefono != null) marca.Telefono = cambios.Telefono;
                if (cambios.Direccion != null) marca.Direccion = cambios.Direccion;

                await _marcaRepository.ModificarMarcaAsync(marca);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Marca modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el marca.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarMarcaAsync(long id)
        {
            try
            {
                await _marcaRepository.EliminarMarcaAsync(id);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Marca eliminado exitosamente.",
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

        private async Task<bool> ValidarInsert(MarcaDTO request)
        {
            // Verificar si el marca ya está registrado
            var marcaExistentePorNombre = await _marcaRepository.ObtenerPorNombreAsync(request.Nombre);
            if (marcaExistentePorNombre != null)
            {
                throw new UniqueFieldException("nombre");
            }

            return true;
        }

        private MarcaDTO ValidarUpdate(MarcaDTO request, Marca marca)
        {
            var cambios = new MarcaDTO();

            if (!string.IsNullOrEmpty(request.Nombre) && request.Nombre != marca.Nombre)
            {
                cambios.Nombre = request.Nombre;
            }

            if (!string.IsNullOrEmpty(request.Telefono) && request.Telefono != marca.Telefono)
            {
                cambios.Telefono = request.Telefono;
            }

            if (!string.IsNullOrEmpty(request.Direccion) && request.Direccion != marca.Direccion)
            {
                cambios.Direccion = request.Direccion;
            }

            return cambios;
        }
    }
}
