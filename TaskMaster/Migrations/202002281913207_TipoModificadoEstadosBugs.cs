namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoModificadoEstadosBugs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EstadosBugs", "NomeEstado", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EstadosBugs", "NomeEstado", c => c.Int(nullable: false));
        }
    }
}
