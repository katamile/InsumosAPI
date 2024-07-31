using System.Net;

namespace InsumosAPI.Middleware.Exceptions.NotFound
{
    public class RegisterNotFoundException : NotFoundException
    {
        public RegisterNotFoundException() : base("No se encontraron registros.")
        {}

        public RegisterNotFoundException(string mensaje) : base("No se encontraron registros. " + mensaje)
        { }
    }
}
