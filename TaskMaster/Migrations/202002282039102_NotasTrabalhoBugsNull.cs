namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotasTrabalhoBugsNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs");
            DropIndex("dbo.Bugs", new[] { "NotasTrabalhoBugsId" });
            AlterColumn("dbo.Bugs", "NotasTrabalhoBugsId", c => c.Int());
            CreateIndex("dbo.Bugs", "NotasTrabalhoBugsId");
            AddForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs", "NotasTrabalhoBugsId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs");
            DropIndex("dbo.Bugs", new[] { "NotasTrabalhoBugsId" });
            AlterColumn("dbo.Bugs", "NotasTrabalhoBugsId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bugs", "NotasTrabalhoBugsId");
            AddForeignKey("dbo.Bugs", "NotasTrabalhoBugsId", "dbo.NotasTrabalhoBugs", "NotasTrabalhoBugsId", cascadeDelete: true);
        }
    }
}
