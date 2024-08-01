using InsumosAPI.Utils;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.Entities
{
    public class CRUDBase
    {
        [Required(ErrorMessage = "El estado no puede ser nulo.")]
        [RegularExpression("^[AI]$", ErrorMessage = $"El campo estado debe ser {Globales.ACTIVO} o {Globales.ACTIVO}.")]
        public string Estado { get; set; } = Globales.ACTIVO;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public string UsuarioCreacion { get; set; } = "SYSTEM";

        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }
        public string? UsuarioEliminacion { get; set; }
    }
}
