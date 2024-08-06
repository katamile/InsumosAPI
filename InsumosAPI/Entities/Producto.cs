using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InsumosAPI.Entities
{
    public class Producto : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre no puede ser nulo.")]
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = String.Empty;

        [Required(ErrorMessage = "El laboratorio no puede ser nulo.")]
        public long IdLaboratorio { get; set; }

        [ForeignKey(nameof(IdLaboratorio))]
        public Laboratorio Laboratorio { get; set; } = null!;

        [Required(ErrorMessage = "El producto no puede ser nulo.")]
        public long IdMovimiento { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio de compra debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal PrecioCompra { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio de venta debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal PrecioVenta { get; set; }
    }
}
