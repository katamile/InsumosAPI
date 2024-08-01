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
        public long IdUsuario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IvaPor { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public List<VentaDetalle> VentaDetalles { get; set; } = [];
    }
}
