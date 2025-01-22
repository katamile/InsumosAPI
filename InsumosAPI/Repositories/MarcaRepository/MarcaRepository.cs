using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.MarcaRepository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public MarcaRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Marca>> GetAll()
        {
            return await _contexto.Marcas
                .Where(l => l.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran marcas activos.");
        }

        public async Task<Marca> GetById(long id)
        {
            return await _contexto.Marcas.FirstOrDefaultAsync(l => l.IdMarca == id && l.Estado == Globales.ACTIVO)
                                             ?? throw new NotFoundException("No se encuentra el marca.");
        }

        public async Task<Marca> ObtenerPorNombreAsync(string nombre)
        {
            return await _contexto.Marcas.FirstOrDefaultAsync(l => l.Nombre == nombre);
        }

        public async Task<MessageInfoDTO> CrearNuevoMarca(MarcaDTO marca)
        {
            try
            {
                var marcaSave = new Marca
                {
                    Nombre = marca.Nombre,
                    Telefono = marca.Telefono,
                    Direccion = marca.Direccion,
                };
                await _contexto.Marcas.AddAsync(marcaSave);
                await _contexto.SaveChangesAsync();

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
                    Status = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task ModificarMarcaAsync(Marca marca)
        {
            _contexto.Marcas.Update(marca);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarMarcaAsync(long id)
        {
            var marca = await GetById(id);
            if (marca == null)
            {
                throw new NotFoundException("Marca no encontrado.");
            }

            // Marcar la entidad como eliminada lógicamente
            marca.Estado = Globales.INACTIVO;
            marca.FechaEliminacion = DateTime.Now;
            marca.UsuarioEliminacion = _userAccessRepository.ObtenerUsuarioLogin();

            // Actualizar la entidad en el contexto
            _contexto.Marcas.Update(marca);

            // Guardar los cambios
            await _contexto.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
