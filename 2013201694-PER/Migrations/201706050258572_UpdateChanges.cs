namespace _2013201694_PER.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoTripulacion", "Descripcion", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.TipoTripulacion", "Nombre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoTripulacion", "Nombre", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.TipoTripulacion", "Descripcion");
        }
    }
}
