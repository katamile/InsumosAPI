﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InsumosAPI.Entities
{
    public class Proveedor : CRUDBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdProveedor { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor no puede ser nulo.")]
        public string Nombre { get; set; } = null!;

        [RegularExpression("^\\+?[1-9]\\d{1,14}$", ErrorMessage = "El formato del número telefónico no es válido.")]
        public string? Telefono { get; set; }

        [StringLength(100, ErrorMessage = "La dirección debe contener máximo 100 caracteres.")]
        public string? Direccion { get; set; }
    }
}
