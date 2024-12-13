using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System;

namespace AndresChaves.Models
{
    public class Usuarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Usuarios { get; set; }

        [Required(ErrorMessage = "El usuario es obligatoria.")]
        [StringLength(50, ErrorMessage = "La usuario no puede exceder los 50 caracteres.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "La contraseña no puede exceder los 64 caracteres.")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime Fecha_creacion { get; set; }

        [HiddenInput]
        public byte[] HashKey { get; set; }

        [HiddenInput]
        public byte[] HashIV { get; set; }
    }
}
