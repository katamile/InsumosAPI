using InsumosAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsumosAPI.Entities
{
    public class Cliente : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCliente { get; set; }

        [Required(ErrorMessage = "El número identificación no puede ser nulo.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "El número identificación debe contener 10 dígitos numéricos.")]
        public string Identificacion { get; set; } = null!;

        [Required(ErrorMessage = "El nombre no puede ser nulo.")]
        public string NombreCompleto { get; set; } = null!;

        [RegularExpression("^\\+?[1-9]\\d{1,14}$", ErrorMessage = "El formato del número telefónico no es válido.")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "La dirección debe contener máximo 100 caracteres.")]
        public string? Direccion { get; set; }

        [RegularExpression("^[a-zA-Z0-9_!#$%&’*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Correo { get; set; }

    }
}
