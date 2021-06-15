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
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
