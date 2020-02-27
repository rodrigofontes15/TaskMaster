namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiposTestesBugsTabelasTasksBugs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TiposBugs", "BugsId", c => c.Int(nullable: false));
            AddColumn("dbo.TiposTestes", "TasksId", c => c.Int(nullable: false));
            CreateIndex("dbo.TiposBugs", "BugsId");
            CreateIndex("dbo.TiposTestes", "TasksId");
            AddForeignKey("dbo.TiposBugs", "BugsId", "dbo.Bugs", "BugsId", cascadeDelete: true);
            AddForeignKey("dbo.TiposTestes", "TasksId", "dbo.Tasks", "TasksId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TiposTestes", "TasksId", "dbo.Tasks");
            DropForeignKey("dbo.TiposBugs", "BugsId", "dbo.Bugs");
            DropIndex("dbo.TiposTestes", new[] { "TasksId" });
            DropIndex("dbo.TiposBugs", new[] { "BugsId" });
            DropColumn("dbo.TiposTestes", "TasksId");
            DropColumn("dbo.TiposBugs", "BugsId");
        }
    }
}
