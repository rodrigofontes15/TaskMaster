namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiposTestesBugs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TiposBugs",
                c => new
                    {
                        TiposBugsId = c.Int(nullable: false, identity: true),
                        TipoBug = c.String(),
                    })
                .PrimaryKey(t => t.TiposBugsId);
            
            CreateTable(
                "dbo.TiposTestes",
                c => new
                    {
                        TiposTestesId = c.Int(nullable: false, identity: true),
                        TipoTeste = c.String(),
                    })
                .PrimaryKey(t => t.TiposTestesId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TiposTestes");
            DropTable("dbo.TiposBugs");
        }
    }
}
