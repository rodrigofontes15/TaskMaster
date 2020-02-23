namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarGP : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GerenteProjs (Nome) VALUES ('Paulo')");
            Sql("INSERT INTO GerenteProjs (Nome) VALUES ('Patricia Simone')");
        }
        
        public override void Down()
        {
        }
    }
}
