using InsumosAPI.Middleware.Exceptions.Unauthorized;
using InsumosAPI.Middleware.Models;
using InsumosAPI.Utils;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class JwtAuthorizationFilter
{
    private readonly RequestDelegate _next;
    private readonly HttpClient _httpClient;
    private readonly string _validateTokenUrl = "api/login/validate";
    private string mensajeError = "Token de autorización inválido o faltante. Por favor, proporcione un token válido.";

    public JwtAuthorizationFilter(RequestDelegate next, HttpClient httpClient)
    {
        _next = next;
        _httpClient = httpClient;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var token = ExtractTokenFromHeader(context);
            await ValidateTokenAsync(token);
            await _next(context);
        }
        catch (UnauthorizedException ex)
        {
            // Log the exception details here
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync($"{mensajeError} Detalle: {ex.Message}");
        }
    }

    private string ExtractTokenFromHeader(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            throw new UnauthorizedException(mensajeError);

        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }

    private async Task ValidateTokenAsync(string token)
    {
        var jsonData = JsonSerializer.Serialize(token);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_validateTokenUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new UnauthorizedException(errorMessage);
        }
    }
}


