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
    public class TipoTripulacionConfiguration : EntityTypeConfiguration<TipoTripulacion>
    {
        public TipoTripulacionConfiguration()
        {
            //Table configurations
            ToTable("TipoTripulacion");
            HasKey(c => c.TipoTripulacionId);
            Property(c => c.TipoTripulacionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Descripcion).IsRequired().HasMaxLength(300);

            //Relationships configuration
            HasRequired(c => c.Tripulacion)
                .WithMany(c => c.TipoTripulacion)
                .HasForeignKey(c => c.TripulacionId);
        }
    }
}
