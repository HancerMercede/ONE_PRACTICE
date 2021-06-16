using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Dtos
{
    public class ParticipanteUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }
        [Phone]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Can be just numbers")]
        public string Telefono { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
