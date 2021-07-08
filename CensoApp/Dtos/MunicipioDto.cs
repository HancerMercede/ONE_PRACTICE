using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Dtos
{
    public class MunicipioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ProvinciaDto Provincia { get; set; }
    }
}
