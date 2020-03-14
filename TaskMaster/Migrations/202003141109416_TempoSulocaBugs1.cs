namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempoSulocaBugs1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bugs", "TempoSolucao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bugs", "TempoSolucao", c => c.Time(nullable: false, precision: 7));
        }
    }
}
