namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotasBUgsBugsRelacionamento : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs");
            DropIndex("dbo.Bugs", new[] { "NotasTrabalhoBugsId" });
            DropColumn("dbo.Bugs", "NotasTrabalhoBugsId");
            DropTable("dbo.NotasTrabalhoBugs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotasTrabalhoBugs",
                c => new
                    {
                        NotasTrabalhoBugsId = c.Int(nullable: false, identity: true),
                        NotasTrabalho = c.String(),
                        DataNotaTrabalho = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NotasTrabalhoBugsId);
            
            AddColumn("dbo.Bugs", "NotasTrabalhoBugsId", c => c.Int());
            CreateIndex("dbo.Bugs", "NotasTrabalhoBugsId");
            AddForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs", "NotasTrabalhoBugsId");
        }
    }
}
