
namespace InsumosAPI.DTOs
{
    public class ClienteDTO
    {
        public long IdCliente { get; set; }
        public string Identificacion { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
    }
}
