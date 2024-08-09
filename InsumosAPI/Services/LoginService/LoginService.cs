using InsumosAPI.DTOs;
using InsumosAPI.Entities;
using InsumosAPI.Middleware.Exceptions.BadRequest;
using InsumosAPI.Middleware.Exceptions.NotFound;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Services.UsuarioService;
using InsumosAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace InsumosAPI.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioService _usuarioService;

        public LoginService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<JWTRequest> LoginUsuario(UsuarioLoginRequest usuariosLoginDAO)
        {
            UsuarioDTO usuarioDAO = await ValidarDatosUsuario(usuariosLoginDAO);

            return new JWTRequest
            {
                Token = GenerateJwtToken(usuarioDAO)
            };
        }

        public async Task<MessageInfoDTO> ValidarToken(string token)
        {
            bool isTokenValid = ValidateToken(token);

            if (!isTokenValid)
                throw new InvalidTokenException();

            return new MessageInfoDTO
            {
                Status = HttpStatusCode.OK,
                Message = "Éxito.",
                Success = "true"
            };
        }

        public async Task<JWTRequest> RefrescarToken(string token)
        {
            bool isTokenValid = ValidateToken(token);

            if (!isTokenValid)
                throw new InvalidTokenException();

            // Obtener el nombre de usuario del token
            string username = GetUsernameFromToken(token);

            // Obtener el usuario usando el nombre de usuario
            UsuarioDTO? usuarioDAO = await _usuarioService.GetByUsername(username)
                ?? throw new InvalidTokenException();

            // Generar un nuevo token JWT
            return new JWTRequest
            {
                Token = GenerateJwtToken(usuarioDAO)
            };
        }

        private async Task<UsuarioDTO> ValidarDatosUsuario(UsuarioLoginRequest usuariosLoginDAO)
        {
            if (usuariosLoginDAO == null)
                throw new InvalidSintaxisException();

            Validator.ValidateObject(usuariosLoginDAO, new ValidationContext(usuariosLoginDAO), true);

            UsuarioDTO? usuarioDAO = await _usuarioService.GetByUsername(usuariosLoginDAO.Username)
                ?? throw new InvalidaUserOrPasswordException();

            if (!BCrypt.Net.BCrypt.Verify(usuariosLoginDAO.Password, usuarioDAO.Contraseña))
                throw new InvalidaUserOrPasswordException();

            return usuarioDAO;
        }

        private string GenerateJwtToken(UsuarioDTO usuarioDAO)
        {
            var now = DateTime.UtcNow;
            var expirationTime = GetTokenExpirationTime();

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("username", usuarioDAO.Username),
                new Claim("rol", usuarioDAO.Rol)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable(Globales.JWTKey)));
            SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: Environment.GetEnvironmentVariable("JWTIssuer"),
                audience: Environment.GetEnvironmentVariable("JWTAudience"),
                claims: claims,
                expires: expirationTime,
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static DateTime GetTokenExpirationTime()
        {
            int expirationMinutes = int.Parse(Environment.GetEnvironmentVariable("expirationTime"));
            return DateTime.UtcNow.AddMinutes(expirationMinutes);
        }

        private static bool ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("JWTIssuer"),
                ValidAudience = Environment.GetEnvironmentVariable("JWTAudience"),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(1000),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKey")))
            };

            try
            {
                SecurityToken securityToken;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

                if (securityToken.ValidTo <= DateTime.UtcNow)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception details here
                throw new InvalidTokenException($"Token de validación fallido: {ex.Message}");
            }
        }

        private static string? GetUsernameFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                if (tokenHandler.ReadToken(token) is not JwtSecurityToken jwtToken)
                    return null;

                Claim? usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "username");

                return usernameClaim?.Value;
            }
            catch
            {
                return null;
            }
        }

    }
}
