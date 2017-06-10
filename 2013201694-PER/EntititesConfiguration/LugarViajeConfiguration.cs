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
    public class LugarViajeConfiguration : EntityTypeConfiguration<LugarViaje>
    {
        public LugarViajeConfiguration()
        {
            //Table configurations
            ToTable("LugaresViaje");
            HasKey(c => c.LugarViajeId);
            Property(c => c.LugarViajeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.NombreLugar).IsRequired().HasMaxLength(300);

            //Relationships Configurations
            HasMany(c => c.TipoLugar)
                .WithRequired(c => c.LugarViaje)
                .HasForeignKey(c => c.LugarViajeId);
        }
    }
}
