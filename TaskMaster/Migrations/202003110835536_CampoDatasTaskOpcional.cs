namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDatasTaskOpcional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "DataInicio", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "DataInicio", c => c.DateTime(nullable: false));
        }
    }
}
