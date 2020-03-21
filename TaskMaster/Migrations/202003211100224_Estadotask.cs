namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estadotask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "EstadoTask", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "EstadoTask");
        }
    }
}
