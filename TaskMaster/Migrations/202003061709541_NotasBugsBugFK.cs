namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotasBugsBugFK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotasTrabalhoBugs",
                c => new
                    {
                        NotasTrabalhoBugId = c.Int(nullable: false, identity: true),
                        NotasTrabalho = c.String(),
                        DataNotaTrabalho = c.DateTime(nullable: false),
                        BugsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NotasTrabalhoBugId)
                .ForeignKey("dbo.Bugs", t => t.BugsId, cascadeDelete: true)
                .Index(t => t.BugsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotasTrabalhoBugs", "BugsId", "dbo.Bugs");
            DropIndex("dbo.NotasTrabalhoBugs", new[] { "BugsId" });
            DropTable("dbo.NotasTrabalhoBugs");
        }
    }
}
