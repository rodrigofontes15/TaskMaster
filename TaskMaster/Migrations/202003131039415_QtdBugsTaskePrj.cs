namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QtdBugsTaskePrj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "QtdBugsTsk", c => c.Int(nullable: false));
            AddColumn("dbo.Projetos", "QtdBugsPrj", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projetos", "QtdBugsPrj");
            DropColumn("dbo.Tasks", "QtdBugsTsk");
        }
    }
}
