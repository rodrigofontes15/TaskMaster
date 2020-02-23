namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopularTabelaTesters : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Testers (Nome) VALUES ('Enzo')");
            Sql("INSERT INTO Testers (Nome) VALUES ('Valentina')");
            Sql("INSERT INTO Testers (Nome) VALUES ('Miguel')");
        }
        
        public override void Down()
        {
        }
    }
}
