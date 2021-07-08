using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Dtos
{
    public class ProvinciaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<MunicipioDto> Municipios { get; set; }
        public ProvinciaDto()
        {
            this.Municipios = new List<MunicipioDto>();
        }
    }
}
