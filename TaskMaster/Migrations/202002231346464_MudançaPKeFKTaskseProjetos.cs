namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudançaPKeFKTaskseProjetos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "Id", "dbo.Projetos");
            DropPrimaryKey("dbo.Projetos");
            DropPrimaryKey("dbo.Tasks");
            DropColumn("dbo.Projetos", "Id");
            DropColumn("dbo.Tasks", "ProjetoId");
            RenameColumn(table: "dbo.Tasks", name: "Id", newName: "ProjetosId");
            RenameIndex(table: "dbo.Tasks", name: "IX_Id", newName: "IX_ProjetosId");
            AddColumn("dbo.Projetos", "ProjetosId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Tasks", "TasksId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Projetos", "ProjetosId");
            AddPrimaryKey("dbo.Tasks", "TasksId");
            AddForeignKey("dbo.Tasks", "ProjetosId", "dbo.Projetos", "ProjetosId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ProjetosId", "dbo.Projetos");
            DropPrimaryKey("dbo.Tasks");
            DropPrimaryKey("dbo.Projetos");
            DropColumn("dbo.Tasks", "TasksId");
            DropColumn("dbo.Projetos", "ProjetosId");
            AddColumn("dbo.Tasks", "ProjetoId", c => c.Int(nullable: false));
            AddColumn("dbo.Projetos", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tasks", "Id");
            AddPrimaryKey("dbo.Projetos", "Id");
            RenameIndex(table: "dbo.Tasks", name: "IX_ProjetosId", newName: "IX_Id");
            RenameColumn(table: "dbo.Tasks", name: "ProjetosId", newName: "Id");
            AddForeignKey("dbo.Tasks", "Id", "dbo.Projetos", "Id");
        }
    }
}
