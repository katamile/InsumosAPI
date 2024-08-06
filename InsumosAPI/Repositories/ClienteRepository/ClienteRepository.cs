using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Repositories.LoginRepository;
using InsumosAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace InsumosAPI.Repositories.ClienteRepository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly InsumosDBContext _contexto;
        private readonly IUserAccessRepository _userAccessRepository;

        public ClienteRepository(InsumosDBContext contexto, IUserAccessRepository userAccessRepository)
        {
            _contexto = contexto;
            _userAccessRepository = userAccessRepository;
        }

        public async Task<List<Cliente>> GetAll()
        {
            return await _contexto.Clientes
                .Where(c => c.Estado == Globales.ACTIVO)
                .ToListAsync() ?? throw new NotFoundException("No se encuentran clientes activos.");
        }

        public async Task<Cliente> GetById(long id)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.IdCliente == id && c.Estado == Globales.ACTIVO)
                                           ?? throw new NotFoundException("No se encuentra el cliente.");
        }

        public async Task<Cliente> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }

        public async Task<MessageInfoDTO> CrearNuevoCliente(ClienteDTO cliente)
        {
            try
            {
                var clienteSave = new Cliente
                {
                    Identificacion = cliente.Identificacion,
                    NombreCompleto = cliente.NombreCompleto,
                    RazonSocial = cliente.RazonSocial,
                    Telefono = cliente.Telefono,
                    Direccion = cliente.Direccion,
                    Correo = cliente.Correo,
                };
                await _contexto.Clientes.AddAsync(clienteSave);
                await _contexto.SaveChangesAsync();

                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.Created,
                    Message = "Cliente creado exitosamente.",
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

        public async Task ModificarClienteAsync(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
        }

        public async Task EliminarClienteAsync(long id)
        {
            var cliente = await GetById(id);
            if (cliente == null)
            {
                throw new NotFoundException("Cliente no encontrado.");
            }

            // Marcar la entidad como eliminada lógicamente
            cliente.Estado = Globales.INACTIVO;
            cliente.FechaEliminacion = DateTime.Now;
            cliente.UsuarioEliminacion = _userAccessRepository.ObtenerUsuarioLogin();

            // Actualizar la entidad en el contexto
            _contexto.Clientes.Update(cliente);

            // Guardar los cambios
            await _contexto.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contexto.SaveChangesAsync();
        }
    }
}
