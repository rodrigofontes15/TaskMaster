namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGPProjetos1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projetos", "GerenteProjsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projetos", "GerenteProjsId");
            AddForeignKey("dbo.Projetos", "GerenteProjsId", "dbo.GerenteProjs", "GerenteProjsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projetos", "GerenteProjsId", "dbo.GerenteProjs");
            DropIndex("dbo.Projetos", new[] { "GerenteProjsId" });
            DropColumn("dbo.Projetos", "GerenteProjsId");
        }
    }
}
