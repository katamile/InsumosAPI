using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InsumosAPI.Entities
{
    public class Venta : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdVenta { get; set; }

        [Required(ErrorMessage = "La fecha de venta no puede ser nula.")]
        public DateTime FechaVenta { get; set; }

        [Required(ErrorMessage = "El cliente no puede ser nulo.")]
        public long IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente Cliente { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "El subtotal debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal Subtotal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El porcentaje de IVA debe ser un número positivo.")]
        [Precision(5, 2)]
        public decimal IvaPor { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El IVA debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal Iva { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El total debe ser un número positivo.")]
        [Precision(18, 2)]
        public decimal Total { get; set; }
        public List<VentaDetalle> VentaDetalles { get; set; } = [];
    }
}
