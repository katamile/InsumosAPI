using InsumosAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.DTOs
{
    public class ProductoDTO
    {
        public long IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = String.Empty;
        public long IdLaboratorio { get; set; }
        public string? LaboratorioName { get; set; } = null!;
        public int? Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public string RutaImg { get; set; } = String.Empty;
    }
}
