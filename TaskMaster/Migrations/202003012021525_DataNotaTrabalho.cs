namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataNotaTrabalho : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotasTrabalhoBugs", "DataNotaTrabalho", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotasTrabalhoBugs", "DataNotaTrabalho");
        }
    }
}
