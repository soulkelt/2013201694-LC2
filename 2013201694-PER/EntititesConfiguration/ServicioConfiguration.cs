using _2013201694_ENT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_PER.EntititesConfiguration
{
    public class ServicioConfiguration : EntityTypeConfiguration<Servicio>
    {
        public ServicioConfiguration()
        {
            //Table configurations
            ToTable("Servicios");
            HasKey(c => c.ServicioId);
            Property(c => c.ServicioId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.NombreServicio).IsRequired().HasMaxLength(100);

            //Relationships configurations
            Map<Encomienda>(m => m.Requires("Discriminator").HasValue("Encomienda"));
            Map<Transporte>(m => m.Requires("Discriminator").HasValue("Transporte"));

            HasMany(s => s.Bus)
                .WithRequired(c => c.Servicio)
                .HasForeignKey(c => c.ServicioId);
            HasMany(s => s.LugarViaje)
                .WithRequired(c => c.Servicio)
                .HasForeignKey(c => c.ServicioId);
        }
    }
}
