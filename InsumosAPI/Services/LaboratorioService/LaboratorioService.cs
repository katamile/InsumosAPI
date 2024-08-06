using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LaboratorioRepository;
using System.Net;

namespace InsumosAPI.Services.LaboratorioService
{
    public class LaboratorioService : ILaboratorioService
    {
        private readonly ILaboratorioRepository _laboratorioRepository;

        public LaboratorioService(ILaboratorioRepository laboratorioRepository)
        {
            _laboratorioRepository = laboratorioRepository;
        }

        public async Task<List<LaboratorioDTO>> GetAll()
        {
            var laboratorios = await _laboratorioRepository.GetAll();
            return laboratorios.Select(l => new LaboratorioDTO
            {
                IdLaboratorio = l.IdLaboratorio,
                Nombre = l.Nombre,
                Telefono = l.Telefono,
                Direccion = l.Direccion
            }).ToList();
        }

        public async Task<LaboratorioDTO> GetById(long id)
        {
            var laboratorio = await _laboratorioRepository.GetById(id);

            return new LaboratorioDTO
            {
                IdLaboratorio = laboratorio.IdLaboratorio,
                Nombre = laboratorio.Nombre,
                Telefono = laboratorio.Telefono,
                Direccion = laboratorio.Direccion
            };
        }

        public async Task<LaboratorioDTO> GetByNombre(string nombre)
        {
            var laboratorio = await _laboratorioRepository.ObtenerPorNombreAsync(nombre);

            if (laboratorio == null)
            {
                throw new NotFoundException("Laboratorio no encontrado.");
            }

            return new LaboratorioDTO
            {
                IdLaboratorio = laboratorio.IdLaboratorio,
                Nombre = laboratorio.Nombre,
                Telefono = laboratorio.Telefono,
                Direccion = laboratorio.Direccion
            };
        }

        public async Task<MessageInfoDTO> CrearLaboratorioAsync(LaboratorioDTO request)
        {
            try
            {
                // Validar la existencia del laboratorio
                await ValidarInsert(request);

                // Crear el nuevo laboratorio
                var laboratorio = new LaboratorioDTO
                {
                    Nombre = request.Nombre,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion
                };

                await _laboratorioRepository.CrearNuevoLaboratorio(laboratorio);

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Laboratorio creado exitosamente.",
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

        public async Task<MessageInfoDTO> ModificarLaboratorioAsync(LaboratorioDTO request)
        {
            var laboratorio = await _laboratorioRepository.GetById(request.IdLaboratorio);
            if (laboratorio == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Laboratorio no encontrado.",
                    Success = "false"
                };
            }

            var cambios = ValidarUpdate(request, laboratorio);

            // Aplicar los cambios al laboratorio solo si hay alguno
            if (cambios != null && (cambios.Nombre != null || cambios.Telefono != null || cambios.Direccion != null))
            {
                if (cambios.Nombre != null) laboratorio.Nombre = cambios.Nombre;
                if (cambios.Telefono != null) laboratorio.Telefono = cambios.Telefono;
                if (cambios.Direccion != null) laboratorio.Direccion = cambios.Direccion;

                await _laboratorioRepository.ModificarLaboratorioAsync(laboratorio);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Laboratorio modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el laboratorio.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarLaboratorioAsync(long id)
        {
            try
            {
                await _laboratorioRepository.EliminarLaboratorioAsync(id);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Laboratorio eliminado exitosamente.",
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

        private async Task<bool> ValidarInsert(LaboratorioDTO request)
        {
            // Verificar si el laboratorio ya está registrado
            var laboratorioExistentePorNombre = await _laboratorioRepository.ObtenerPorNombreAsync(request.Nombre);
            if (laboratorioExistentePorNombre != null)
            {
                throw new UniqueFieldException("nombre");
            }

            return true;
        }

        private LaboratorioDTO ValidarUpdate(LaboratorioDTO request, Laboratorio laboratorio)
        {
            var cambios = new LaboratorioDTO();

            if (!string.IsNullOrEmpty(request.Nombre) && request.Nombre != laboratorio.Nombre)
            {
                cambios.Nombre = request.Nombre;
            }

            if (!string.IsNullOrEmpty(request.Telefono) && request.Telefono != laboratorio.Telefono)
            {
                cambios.Telefono = request.Telefono;
            }

            if (!string.IsNullOrEmpty(request.Direccion) && request.Direccion != laboratorio.Direccion)
            {
                cambios.Direccion = request.Direccion;
            }

            return cambios;
        }
    }
}
