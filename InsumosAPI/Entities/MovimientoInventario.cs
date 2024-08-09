using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using InsumosAPI.Utils;

namespace InsumosAPI.Entities
{
    public class MovimientoInventario : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdMovimiento { get; set; }

        [Required(ErrorMessage = "El producto no puede ser nulo.")]
        public long IdProducto { get; set; }

        [RegularExpression("^(IN|EG)\\d*$", ErrorMessage = $"El campo Tipo Movimiento debe ser {Globales.INGRESO} o {Globales.EGRESO}")]
        public string TipoMovimiento { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "El Stock del Producto debe ser un número entero positivo.")]
        public int StockProducto { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La Cantidad del movimiento de stock debe ser un número entero positivo.")]
        public int CantidadMovimiento { get; set; }

    }
}
