using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Provincia Provincia { get; set; }
    }
}
