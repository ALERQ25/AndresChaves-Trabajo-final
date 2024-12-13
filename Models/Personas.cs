using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndresChaves.Models
{
    public class Personas
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Llave primaria autoincremental//
        public int Id_Personas { get; set; }

        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo nombres no puede exceder 50 caracteres.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo apellidos no puede exceder 50 caracteres.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El número de identificación es obligatorio.")]
        [StringLength(50, ErrorMessage = "El número de identificación no puede exceder 50 caracteres.")]
        public string Num_identificacion { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(50, ErrorMessage = "El correo electrónico no puede exceder 50 caracteres.")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "El tipo de identificación no puede exceder 50 caracteres.")]
        public string Tipo_identificacion { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime Fecha_creacion { get; set; }
    }
}