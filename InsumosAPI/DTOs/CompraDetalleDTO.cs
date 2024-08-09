using InsumosAPI.Entities;

namespace InsumosAPI.DTOs
{
    public class CompraDetalleDTO
    {
        public long IdCompraDetalle { get; set; }
        public long IdCompra { get; set; }
        public long IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
