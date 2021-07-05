using CensoApp.Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Entities
{
    [Index(nameof(Credencial),IsUnique=true)]
    public class Participante
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [Display(Name ="Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        [Display(Name ="Tipo de Credencial")]
        public string TipoCredencial { get; set; }
        [Required]
        public string Credencial { get; set; }
        [Required]
        [Phone]
        [RegularExpression("(^[0-9]+$)",ErrorMessage="Can be just numbers")]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Status Status { get; set; }
        [Required]
        [Display(Name ="Nivel academico")]
        public string NivelAcademico { get; set; }
        [Required]
        [Display(Name ="Cargo pre-asignado")]
        public string CargoPreasignado { get; set; }
        [Required]
        [Display(Name ="Fecha solicitud")]
        public DateTime FechaSolicitud { get; set; }
    }
}
