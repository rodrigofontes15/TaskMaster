namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BugsProjetos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "ProjetosId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bugs", "ProjetosId");
            AddForeignKey("dbo.Bugs", "ProjetosId", "dbo.Projetos", "ProjetosId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "ProjetosId", "dbo.Projetos");
            DropIndex("dbo.Bugs", new[] { "ProjetosId" });
            DropColumn("dbo.Bugs", "ProjetosId");
        }
    }
}
