namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatioBugPrjDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projetos", "BugsRatio", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projetos", "BugsRatio", c => c.Int(nullable: false));
        }
    }
}
