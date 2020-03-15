namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estadoproj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projetos", "EstadoProj", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projetos", "EstadoProj");
        }
    }
}
