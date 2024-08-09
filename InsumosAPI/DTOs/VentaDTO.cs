using InsumosAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.DTOs
{
    public class VentaDTO
    {
        public long IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public long IdCliente { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal IvaPor { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public List<VentaDetalleDTO> VentaDetalles { get; set; } = [];
    }

    public class DetallesVentaValidacionResult
    {
        public List<VentaDetalleDTO> VentaDetalleDTOs { get; set; } = null!;
        public decimal Subtotal { get; set; }
    }
}
