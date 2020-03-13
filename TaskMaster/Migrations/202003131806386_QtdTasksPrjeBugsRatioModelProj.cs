namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QtdTasksPrjeBugsRatioModelProj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projetos", "QtdTasksPrj", c => c.Int(nullable: false));
            AddColumn("dbo.Projetos", "BugsRatio", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projetos", "BugsRatio");
            DropColumn("dbo.Projetos", "QtdTasksPrj");
        }
    }
}
