using InsumosAPI.Entities;

namespace InsumosAPI.DTOs
{
    public class MovimientoInventarioDTO
    {
        public long IdMovimiento { get; set; }
        public long IdProducto { get; set; }
        public string Producto { get; set; } = null!;
        public string TipoMovimiento { get; set; } = null!;
        public int StockProducto { get; set; }
        public int CantidadMovimiento { get; set; }
    }
}
