namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKTasksTiposTeste : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "TiposBugsId", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "TiposTestesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bugs", "TiposBugsId");
            CreateIndex("dbo.Tasks", "TiposTestesId");
            AddForeignKey("dbo.Tasks", "TiposTestesId", "dbo.TiposTestes", "TiposTestesId", cascadeDelete: true);
            AddForeignKey("dbo.Bugs", "TiposBugsId", "dbo.TiposBugs", "TiposBugsId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "TiposBugsId", "dbo.TiposBugs");
            DropForeignKey("dbo.Tasks", "TiposTestesId", "dbo.TiposTestes");
            DropIndex("dbo.Tasks", new[] { "TiposTestesId" });
            DropIndex("dbo.Bugs", new[] { "TiposBugsId" });
            DropColumn("dbo.Tasks", "TiposTestesId");
            DropColumn("dbo.Bugs", "TiposBugsId");
        }
    }
}
