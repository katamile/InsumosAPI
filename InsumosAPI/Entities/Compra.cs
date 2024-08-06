using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.Entities
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCompra { get; set; }

        [Required(ErrorMessage = "La fecha de venta no puede ser nula.")]
        public DateTime FechaCompra { get; set; }

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
        public List<CompraDetalle> CompraDetalle { get; set; } = [];
    }
}
