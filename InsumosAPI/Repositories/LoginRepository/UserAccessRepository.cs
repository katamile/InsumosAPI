using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InsumosAPI.Repositories.LoginRepository
{
    public class UserAccessRepository(IHttpContextAccessor httpContextAccessor) : IUserAccessRepository
    {
        private readonly string _ip = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Alert 401!";
        private readonly string _usuario = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "username")?.Value ?? "Alert 401!";
        private readonly string _rol = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c=>c.Type == "rol")?.Value ?? "Alert 401!";

        public string ObtenerUsuarioLogin()
        {
            return (_usuario);
        }

        public string ObtenerRolLogin()
        {
            return (_rol);
        }

    }
}
