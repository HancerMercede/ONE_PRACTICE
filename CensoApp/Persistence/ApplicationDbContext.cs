using CensoApp.Entities;
using CensoApp.Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoApp.Persistence
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            :base(options)
        {

        }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Participante>().HasQueryFilter(x => x.Status != Status.DeActive);
            builder.Entity<Participante>().Property(x => x.FechaSolicitud).HasMaxLength(10).HasColumnType("date");

        }
    }
}
