namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstadosBugs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstadosBugs",
                c => new
                    {
                        EstadosBugId = c.Int(nullable: false, identity: true),
                        NomeEstado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EstadosBugId);
            
            AddColumn("dbo.Bugs", "EstadosBugId", c => c.Int(nullable: false));
            AddColumn("dbo.Devs", "TelDev", c => c.String());
            AddColumn("dbo.Devs", "UrlPhotoDev", c => c.String());
            AddColumn("dbo.GerenteProjs", "TelGp", c => c.String());
            AddColumn("dbo.GerenteProjs", "UrlPhotoGp", c => c.String());
            AddColumn("dbo.Testers", "TelTester", c => c.String());
            AddColumn("dbo.Testers", "UrlPhotoTester", c => c.String());
            CreateIndex("dbo.Bugs", "EstadosBugId");
            AddForeignKey("dbo.Bugs", "EstadosBugId", "dbo.EstadosBugs", "EstadosBugId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "EstadosBugId", "dbo.EstadosBugs");
            DropIndex("dbo.Bugs", new[] { "EstadosBugId" });
            DropColumn("dbo.Testers", "UrlPhotoTester");
            DropColumn("dbo.Testers", "TelTester");
            DropColumn("dbo.GerenteProjs", "UrlPhotoGp");
            DropColumn("dbo.GerenteProjs", "TelGp");
            DropColumn("dbo.Devs", "UrlPhotoDev");
            DropColumn("dbo.Devs", "TelDev");
            DropColumn("dbo.Bugs", "EstadosBugId");
            DropTable("dbo.EstadosBugs");
        }
    }
}
