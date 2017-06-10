namespace _2013201694_PER.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        BusId = c.Int(nullable: false, identity: true),
                        Placa = c.String(nullable: false, maxLength: 15),
                        SerieMotor = c.String(),
                        ServicioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BusId)
                .ForeignKey("dbo.Servicios", t => t.ServicioId, cascadeDelete: true)
                .Index(t => t.ServicioId);
            
            CreateTable(
                "dbo.Servicios",
                c => new
                    {
                        ServicioId = c.Int(nullable: false, identity: true),
                        NombreServicio = c.String(nullable: false, maxLength: 100),
                        Tarifa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VentaId = c.Int(nullable: false),
                        AsuntoEncomienda = c.String(),
                        NombreDestinatario = c.String(),
                        Observaciones = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ServicioId)
                .ForeignKey("dbo.Ventas", t => t.VentaId)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.LugaresViaje",
                c => new
                    {
                        LugarViajeId = c.Int(nullable: false, identity: true),
                        NombreLugar = c.String(nullable: false, maxLength: 300),
                        ServicioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LugarViajeId)
                .ForeignKey("dbo.Servicios", t => t.ServicioId, cascadeDelete: true)
                .Index(t => t.ServicioId);
            
            CreateTable(
                "dbo.TipoLugares",
                c => new
                    {
                        TipoLugarId = c.Int(nullable: false, identity: true),
                        NombreTipo = c.String(nullable: false, maxLength: 300),
                        LugarViajeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoLugarId)
                .ForeignKey("dbo.LugaresViaje", t => t.LugarViajeId, cascadeDelete: true)
                .Index(t => t.LugarViajeId);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        VentaId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VentaId);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        EmpleadoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 250),
                        DNI = c.String(nullable: false, maxLength: 8),
                        Edad = c.Int(nullable: false),
                        Sueldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cargo = c.String(),
                        VentaId = c.Int(),
                        NombreTripulacion = c.String(),
                        BusId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EmpleadoId)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .ForeignKey("dbo.Buses", t => t.BusId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.BusId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 200),
                        DNI = c.String(nullable: false, maxLength: 8),
                        VentaId = c.Int(nullable: false),
                        ServicioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId)
                .ForeignKey("dbo.Servicios", t => t.ServicioId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId)
                .Index(t => t.ServicioId);
            
            CreateTable(
                "dbo.TipoCombrobantes",
                c => new
                    {
                        TipoComprobanteId = c.Int(nullable: false, identity: true),
                        NombreComprobante = c.String(nullable: false, maxLength: 300),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoComprobanteId)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.TipoPagos",
                c => new
                    {
                        TipoPagoId = c.Int(nullable: false, identity: true),
                        MetodoPago = c.String(nullable: false, maxLength: 300),
                        VentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoPagoId)
                .ForeignKey("dbo.Ventas", t => t.VentaId, cascadeDelete: true)
                .Index(t => t.VentaId);
            
            CreateTable(
                "dbo.TipoViajes",
                c => new
                    {
                        TipoViajeId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 300),
                        ServicioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoViajeId)
                .ForeignKey("dbo.Servicios", t => t.ServicioId, cascadeDelete: true)
                .Index(t => t.ServicioId);
            
            CreateTable(
                "dbo.TipoTripulacion",
                c => new
                    {
                        TipoTripulacionId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 300),
                        TripulacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TipoTripulacionId)
                .ForeignKey("dbo.Empleados", t => t.TripulacionId, cascadeDelete: true)
                .Index(t => t.TripulacionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empleados", "BusId", "dbo.Buses");
            DropForeignKey("dbo.TipoTripulacion", "TripulacionId", "dbo.Empleados");
            DropForeignKey("dbo.TipoViajes", "ServicioId", "dbo.Servicios");
            DropForeignKey("dbo.TipoPagos", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.TipoCombrobantes", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Servicios", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Clientes", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.Clientes", "ServicioId", "dbo.Servicios");
            DropForeignKey("dbo.Empleados", "VentaId", "dbo.Ventas");
            DropForeignKey("dbo.LugaresViaje", "ServicioId", "dbo.Servicios");
            DropForeignKey("dbo.TipoLugares", "LugarViajeId", "dbo.LugaresViaje");
            DropForeignKey("dbo.Buses", "ServicioId", "dbo.Servicios");
            DropIndex("dbo.TipoTripulacion", new[] { "TripulacionId" });
            DropIndex("dbo.TipoViajes", new[] { "ServicioId" });
            DropIndex("dbo.TipoPagos", new[] { "VentaId" });
            DropIndex("dbo.TipoCombrobantes", new[] { "VentaId" });
            DropIndex("dbo.Clientes", new[] { "ServicioId" });
            DropIndex("dbo.Clientes", new[] { "VentaId" });
            DropIndex("dbo.Empleados", new[] { "BusId" });
            DropIndex("dbo.Empleados", new[] { "VentaId" });
            DropIndex("dbo.TipoLugares", new[] { "LugarViajeId" });
            DropIndex("dbo.LugaresViaje", new[] { "ServicioId" });
            DropIndex("dbo.Servicios", new[] { "VentaId" });
            DropIndex("dbo.Buses", new[] { "ServicioId" });
            DropTable("dbo.TipoTripulacion");
            DropTable("dbo.TipoViajes");
            DropTable("dbo.TipoPagos");
            DropTable("dbo.TipoCombrobantes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Empleados");
            DropTable("dbo.Ventas");
            DropTable("dbo.TipoLugares");
            DropTable("dbo.LugaresViaje");
            DropTable("dbo.Servicios");
            DropTable("dbo.Buses");
        }
    }
}
