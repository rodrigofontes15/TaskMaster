namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopularTabelaGPs : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GerenteProjs (Nome) VALUES ('Thiago Ribeiro')");
            Sql("INSERT INTO GerenteProjs (Nome) VALUES ('Nilson Moreira')");
            Sql("INSERT INTO GerenteProjs (Nome) VALUES ('Rodrigo Fontes')");
        }
        
        public override void Down()
        {
        }
    }
}
