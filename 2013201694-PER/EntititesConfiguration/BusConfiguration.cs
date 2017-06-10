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
    public class BusConfiguration : EntityTypeConfiguration<Bus>
    {
        public BusConfiguration()
        {
            //Table configurations
            ToTable("Buses");
            HasKey(c => c.BusId);
            Property(c => c.BusId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Placa).IsRequired().HasMaxLength(15);

            //Relationships Configurations
            HasMany(c => c.Tripulacion)
                .WithRequired(c => c.Bus)
                .HasForeignKey(c => c.BusId);
        }
    }
}
