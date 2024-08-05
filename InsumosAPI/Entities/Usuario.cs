using InsumosAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsumosAPI.Entities
{
    public class Usuario : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "El número identificación no puede ser nulo.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "El número identificación debe contener 10 dígitos numéricos.")]
        public string Identificacion { get; set; } = null!;

        [Required(ErrorMessage = "El nombre no puede ser nulo.")]
        [StringLength(100, ErrorMessage = "El nombre debe contener máximo 100 caracteres.")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido no puede ser nulo.")]
        [StringLength(100, ErrorMessage = "El apellido debe contener máximo 100 caracteres.")]
        public string Apellidos { get; set; } = null!;

        [RegularExpression("^[a-zA-Z0-9_!#$%&’*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El username no puede ser nulo.")]
        [StringLength(50, ErrorMessage = "El username debe contener máximo 50 caracteres.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña no puede ser nula.")]
        [StringLength(200, ErrorMessage = "La contraseña debe contener máximo 150 caracteres.")]
        public string Contraseña { get; set; } = null!;

        [Required(ErrorMessage = "El Rol no puede ser nulo.")]
        public string Rol { get; set; } = null!;

        [RegularExpression("^[0-3]\\d*$", ErrorMessage = "El campo intentosfallidos debe ser un número entero mayor a cero.")]
        public int? IntentosFallidos { get; set; } = 0;

    }
}
