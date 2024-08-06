using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Repositories.ClienteRepository;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Utils;
using System.Net;
using System.Threading.Tasks;
using InsumosAPI.Middleware.Exceptions.BadRequest;

namespace InsumosAPI.Services.ClienteService
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteDTO>> GetAll()
        {
            var clientes = await _clienteRepository.GetAll();
            return clientes.Select(c => new ClienteDTO
            {
                IdCliente = c.IdCliente,
                Identificacion = c.Identificacion,
                NombreCompleto = c.NombreCompleto,
                RazonSocial = c.RazonSocial,
                Telefono = c.Telefono,
                Direccion = c.Direccion,
                Correo = c.Correo
            }).ToList();
        }

        public async Task<ClienteDTO> GetById(long id)
        {
            var cliente = await _clienteRepository.GetById(id);

            return new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Identificacion = cliente.Identificacion,
                NombreCompleto = cliente.NombreCompleto,
                RazonSocial = cliente.RazonSocial,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Correo = cliente.Correo
            };
        }

        public async Task<ClienteDTO> GetByIdentificacion(string identificacion)
        {
            var cliente = await _clienteRepository.ObtenerPorIdentificacionAsync(identificacion);

            if (cliente == null)
            {
                throw new NotFoundException("Cliente no encontrado.");
            }

            return new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                Identificacion = cliente.Identificacion,
                NombreCompleto = cliente.NombreCompleto,
                RazonSocial = cliente.RazonSocial,
                Telefono = cliente.Telefono,
                Direccion = cliente.Direccion,
                Correo = cliente.Correo
            };
        }

        public async Task<MessageInfoDTO> CrearClienteAsync(ClienteDTO request)
        {
            try
            {
                // Validar la existencia del cliente
                await ValidarInsert(request);

                // Crear el nuevo cliente
                var cliente = new ClienteDTO
                {
                    Identificacion = request.Identificacion,
                    NombreCompleto = request.NombreCompleto,
                    RazonSocial = request.RazonSocial,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion,
                    Correo = request.Correo,
                };

                await _clienteRepository.CrearNuevoCliente(cliente);

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
                    Status = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                    Success = "false"
                };
            }
        }

        public async Task<MessageInfoDTO> ModificarClienteAsync(ClienteDTO request)
        {
            var cliente = await _clienteRepository.GetById(request.IdCliente);
            if (cliente == null)
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NotFound,
                    Message = "Cliente no encontrado.",
                    Success = "false"
                };
            }

            var cambios = ValidarUpdate(request, cliente);

            // Aplicar los cambios al cliente solo si hay alguno
            if (cambios != null && (cambios.NombreCompleto != null || cambios.RazonSocial != null ||
                                     cambios.Telefono != null || cambios.Direccion != null ||
                                     cambios.Correo != null))
            {
                if (cambios.NombreCompleto != null) cliente.NombreCompleto = cambios.NombreCompleto;
                if (cambios.RazonSocial != null) cliente.RazonSocial = cambios.RazonSocial;
                if (cambios.Telefono != null) cliente.Telefono = cambios.Telefono;
                if (cambios.Direccion != null) cliente.Direccion = cambios.Direccion;
                if (cambios.Correo != null) cliente.Correo = cambios.Correo;

                await _clienteRepository.ModificarClienteAsync(cliente);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Cliente modificado exitosamente.",
                    Success = "true"
                };
            }
            else
            {
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.NoContent,
                    Message = "No se realizaron cambios en el cliente.",
                    Success = "true"
                };
            }
        }

        public async Task<MessageInfoDTO> EliminarClienteAsync(long id)
        {
            try
            {
                await _clienteRepository.EliminarClienteAsync(id);
                return new MessageInfoDTO
                {
                    Status = HttpStatusCode.OK,
                    Message = "Cliente eliminado exitosamente.",
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

        private async Task<bool> ValidarInsert(ClienteDTO request)
        {
            // Verificar si la cédula ya está registrada
            var clienteExistentePorIdentificacion = await _clienteRepository.ObtenerPorIdentificacionAsync(request.Identificacion);
            if (clienteExistentePorIdentificacion != null)
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

        private ClienteDTO ValidarUpdate(ClienteDTO request, Cliente cliente)
        {
            var cambios = new ClienteDTO();

            if (!string.IsNullOrEmpty(request.NombreCompleto) && request.NombreCompleto != cliente.NombreCompleto)
            {
                cambios.NombreCompleto = request.NombreCompleto;
            }

            if (!string.IsNullOrEmpty(request.RazonSocial) && request.RazonSocial != cliente.RazonSocial)
            {
                cambios.RazonSocial = request.RazonSocial;
            }

            if (!string.IsNullOrEmpty(request.Telefono) && request.Telefono != cliente.Telefono)
            {
                cambios.Telefono = request.Telefono;
            }

            if (!string.IsNullOrEmpty(request.Direccion) && request.Direccion != cliente.Direccion)
            {
                cambios.Direccion = request.Direccion;
            }

            if (!string.IsNullOrEmpty(request.Correo) && request.Correo != cliente.Correo)
            {
                cambios.Correo = request.Correo;
            }

            return cambios;
        }
    }
}
