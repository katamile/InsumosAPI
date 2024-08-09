using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InsumosAPI.Entities
{
    public class VentaDetalle : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdVentaDetalle { get; set; }

        [Required(ErrorMessage = "La venta no puede ser nula.")]
        public long IdVenta { get; set; }

        [ForeignKey(nameof(IdVenta))]
        public virtual Venta Venta { get; set; } = null!;

        [Required(ErrorMessage = "El producto no puede ser nulo.")]
        public long IdProducto { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public Producto Producto { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo.")]
        public int Cantidad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio unitario debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal PrecioUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio total debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal PrecioTotal { get; set; }
    }
}
