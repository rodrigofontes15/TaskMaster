namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempoSulocaBugDtatypeDouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "TempoSolucao", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bugs", "TempoSolucao");
        }
    }
}
