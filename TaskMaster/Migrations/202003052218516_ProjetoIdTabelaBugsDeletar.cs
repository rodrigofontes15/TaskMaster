namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjetoIdTabelaBugsDeletar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bugs", "ProjetosId", "dbo.Projetos");
            DropIndex("dbo.Bugs", new[] { "ProjetosId" });
            DropColumn("dbo.Bugs", "ProjetosId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bugs", "ProjetosId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bugs", "ProjetosId");
            AddForeignKey("dbo.Bugs", "ProjetosId", "dbo.Projetos", "ProjetosId", cascadeDelete: true);
        }
    }
}
