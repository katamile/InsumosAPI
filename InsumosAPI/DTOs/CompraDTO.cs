using InsumosAPI.Entities;

namespace InsumosAPI.DTOs
{
    public class CompraDTO
    {
        public long IdCompra { get; set; }
        public long IdProveedor { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IvaPor { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public List<CompraDetalleDTO> CompraDetalle { get; set; } = [];
    }

    public class DetallesValidacionResult
    {
        public List<CompraDetalleDTO> CompraDetalleDTOs { get; set; } = null!;
        public decimal Subtotal { get; set; }
    }

}
