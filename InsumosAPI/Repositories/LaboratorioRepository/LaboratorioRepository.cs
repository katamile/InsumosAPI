using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.LaboratorioRepository
{
    public class LaboratorioRepository : ILaboratorioRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public LaboratorioRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Laboratorio>> GetAll()
        {
            return await _contexto.Laboratorios
                .Where(l => l.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran laboratorios activos.");
        }

        public async Task<Laboratorio> GetById(long id)
        {
            return await _contexto.Laboratorios.FirstOrDefaultAsync(l => l.IdLaboratorio == id && l.Estado == Globales.ACTIVO)
                                             ?? throw new NotFoundException("No se encuentra el laboratorio.");
        }

        public async Task<Laboratorio> ObtenerPorNombreAsync(string nombre)
        {
            return await _contexto.Laboratorios.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<MessageInfoDTO> CrearNuevoLaboratorio(LaboratorioDTO laboratorio)
        {
            try
            {
                var laboratorioSave = new Laboratorio
                {
                    Nombre = laboratorio.Nombre,
                    Telefono = laboratorio.Telefono,
                    Direccion = laboratorio.Direccion,
                };
                await _contexto.Laboratorios.AddAsync(laboratorioSave);
                await _contexto.SaveChangesAsync();

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
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task ModificarLaboratorioAsync(Laboratorio laboratorio)
        {
            _contexto.Laboratorios.Update(laboratorio);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarLaboratorioAsync(long id)
        {
            var laboratorio = await GetById(id);
            if (laboratorio == null)
            {
                throw new NotFoundException("Laboratorio no encontrado.");
            }

            // Marcar la entidad como eliminada lógicamente
            laboratorio.Estado = Globales.INACTIVO;
            laboratorio.FechaEliminacion = DateTime.Now;
            laboratorio.UsuarioEliminacion = _userAccessRepository.ObtenerUsuarioLogin();

            // Actualizar la entidad en el contexto
            _contexto.Laboratorios.Update(laboratorio);

            // Guardar los cambios
            await _contexto.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
