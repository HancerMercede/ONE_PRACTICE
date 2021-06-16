using CensoApp.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Dtos
{
    public class ParticipanteDto
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
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public int Edad { get; set; }
        [Required]
        public string TipoCredencial { get; set; }
        [Required]
        public string Credencial { get; set; }
        [Required]
        [Phone]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Can be just numbers")]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Status Status { get; set; }
        [Required]
        public string NivelAcademico { get; set; }
        [Required]
        public string CargoPreasignado { get; set; }
        [Required]
        public DateTime FechaSolicitud { get; set; }
    }
}
