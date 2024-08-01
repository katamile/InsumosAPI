using System.Net;

namespace InsumosAPI.Middleware.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}
