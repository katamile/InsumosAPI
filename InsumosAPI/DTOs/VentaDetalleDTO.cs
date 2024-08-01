using InsumosAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.DTOs
{
    public class VentaDetalleDTO
    {
        public long IdVentaDetalle { get; set; }
        public long IdVenta { get; set; }
        public long IdProducto { get; set; }
        public string? Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
