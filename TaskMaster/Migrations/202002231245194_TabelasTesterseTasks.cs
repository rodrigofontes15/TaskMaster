namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelasTesterseTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NomeTask = c.String(nullable: false, maxLength: 255),
                        ProjetoId = c.Int(nullable: false),
                        DataInicio = c.DateTime(),
                        DataEstimada = c.DateTime(),
                        TestersId = c.Int(nullable: false),
                        DataReal = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projetos", t => t.Id)
                .ForeignKey("dbo.Testers", t => t.TestersId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.TestersId);
            
            CreateTable(
                "dbo.Testers",
                c => new
                    {
                        TestersId = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TestersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TestersId", "dbo.Testers");
            DropForeignKey("dbo.Tasks", "Id", "dbo.Projetos");
            DropIndex("dbo.Tasks", new[] { "TestersId" });
            DropIndex("dbo.Tasks", new[] { "Id" });
            DropTable("dbo.Testers");
            DropTable("dbo.Tasks");
        }
    }
}
