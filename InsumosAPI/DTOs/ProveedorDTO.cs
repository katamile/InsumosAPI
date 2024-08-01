using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.DTOs
{
    public class ProveedorDTO
    {
        public long IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
    }
}
