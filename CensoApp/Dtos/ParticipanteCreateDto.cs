using CensoApp.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Dtos
{
    public class ParticipanteCreateDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }    
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        [Required]
        public string TipoCredencial { get; set; }
        [Required]
        public string Credencial { get; set; }
        [Phone]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Can be just numbers")]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Status Status { get; set; }
        public string NivelAcademico { get; set; }
        public string CargoPreasignado { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Calle { get; set; }
        public string Sector{ get; set; }
        public string Municipio { get; set; }
    }
}
