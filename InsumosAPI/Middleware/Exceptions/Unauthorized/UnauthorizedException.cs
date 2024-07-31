using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace InsumosAPI.Middleware.Exceptions.Unauthorized
{
    public class UnauthorizedException : UnauthorizedAccessException
    {
        public UnauthorizedException(string mensaje) : base(mensaje)
        {
        }
    }
}
