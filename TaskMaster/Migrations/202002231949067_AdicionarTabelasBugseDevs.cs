namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarTabelasBugseDevs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bugs",
                c => new
                    {
                        BugsId = c.Int(nullable: false, identity: true),
                        DescBug = c.String(nullable: false, maxLength: 255),
                        TasksId = c.Int(nullable: false),
                        DataBug = c.DateTime(),
                        DataEstimada = c.DateTime(),
                        DevsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BugsId)
                .ForeignKey("dbo.Devs", t => t.DevsId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TasksId, cascadeDelete: true)
                .Index(t => t.TasksId)
                .Index(t => t.DevsId);
            
            CreateTable(
                "dbo.Devs",
                c => new
                    {
                        DevsId = c.Int(nullable: false, identity: true),
                        DevNome = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.DevsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bugs", "TasksId", "dbo.Tasks");
            DropForeignKey("dbo.Bugs", "DevsId", "dbo.Devs");
            DropIndex("dbo.Bugs", new[] { "DevsId" });
            DropIndex("dbo.Bugs", new[] { "TasksId" });
            DropTable("dbo.Devs");
            DropTable("dbo.Bugs");
        }
    }
}
