using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.Entities
{
    public class CompraDetalle : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCompraDetalle { get; set; }


        [Required(ErrorMessage = "La compra no puede ser nula.")]
        public long IdCompra { get; set; }

        [ForeignKey(nameof(IdCompra))]
        public virtual Compra Compra { get; set; } = null!;

        [Required(ErrorMessage = "El producto no puede ser nulo.")]
        public long IdProducto { get; set; }

        [ForeignKey(nameof(IdProducto))]
        public Producto? Producto { get; set; } = null!;

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
