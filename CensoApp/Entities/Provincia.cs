﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Entities
{
    public class Provincia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }
        public Provincia()
        {
            this.Municipios = new List<Municipio>();
        }
    }
}
