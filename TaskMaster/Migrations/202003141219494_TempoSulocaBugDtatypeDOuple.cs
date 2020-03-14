namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempoSulocaBugDtatypeDOuple : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bugs", "TempoSolucao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bugs", "TempoSolucao", c => c.DateTime());
        }
    }
}
