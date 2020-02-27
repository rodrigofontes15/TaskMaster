namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiposTestesBugsTabelasTasksBugsRemoveKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TiposBugs", "BugsId", "dbo.Bugs");
            DropForeignKey("dbo.TiposTestes", "TasksId", "dbo.Tasks");
            DropIndex("dbo.TiposBugs", new[] { "BugsId" });
            DropIndex("dbo.TiposTestes", new[] { "TasksId" });
            DropColumn("dbo.TiposBugs", "BugsId");
            DropColumn("dbo.TiposTestes", "TasksId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TiposTestes", "TasksId", c => c.Int(nullable: false));
            AddColumn("dbo.TiposBugs", "BugsId", c => c.Int(nullable: false));
            CreateIndex("dbo.TiposTestes", "TasksId");
            CreateIndex("dbo.TiposBugs", "BugsId");
            AddForeignKey("dbo.TiposTestes", "TasksId", "dbo.Tasks", "TasksId", cascadeDelete: true);
            AddForeignKey("dbo.TiposBugs", "BugsId", "dbo.Bugs", "BugsId", cascadeDelete: true);
        }
    }
}
