namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempoSulocaBugs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "TempoSolucao", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bugs", "TempoSolucao");
        }
    }
}
