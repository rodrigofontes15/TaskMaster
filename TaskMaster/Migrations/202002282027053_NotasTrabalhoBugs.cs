namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotasTrabalhoBugs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotasTrabalhoBugs",
                c => new
                    {
                        NotasTrabalhoBugsId = c.Int(nullable: false, identity: true),
                        NotasTrabalho = c.String(),
                    })
                .PrimaryKey(t => t.NotasTrabalhoBugsId);
            
            AddColumn("dbo.Bugs", "NotasTrabalhoBugsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bugs", "NotasTrabalhoBugsId");
            AddForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs", "NotasTrabalhoBugsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs");
            DropIndex("dbo.Bugs", new[] { "NotasTrabalhoBugsId" });
            DropColumn("dbo.Bugs", "NotasTrabalhoBugsId");
            DropTable("dbo.NotasTrabalhoBugs");
        }
    }
}
