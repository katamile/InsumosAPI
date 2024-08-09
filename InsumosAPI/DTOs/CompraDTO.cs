using InsumosAPI.Entities;

namespace InsumosAPI.DTOs
{
    public class CompraDTO
    {
        public long IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IvaPor { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public List<CompraDetalle> CompraDetalle { get; set; } = [];
    }
}
