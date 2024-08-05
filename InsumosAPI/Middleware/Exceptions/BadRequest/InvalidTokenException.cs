using InsumosAPI.Utils;

namespace InsumosAPI.Middleware.Exceptions.BadRequest
{
    public class InvalidTokenException : BadRequestException
    {
        public InvalidTokenException() : base("Token no válido.")
        {
        }

        public InvalidTokenException(string mensaje) : base($"Token no válido. {mensaje}")
        {
        }
    }
}
