namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedataEstimadaBug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "DataEstimadaBug", c => c.DateTime());
            DropColumn("dbo.Bugs", "DataEstimada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bugs", "DataEstimada", c => c.DateTime());
            DropColumn("dbo.Bugs", "DataEstimadaBug");
        }
    }
}
