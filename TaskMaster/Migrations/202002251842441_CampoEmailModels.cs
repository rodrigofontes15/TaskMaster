namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoEmailModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bugs", "BugAceito", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bugs", "UrlRepoCodigo", c => c.String());
            AddColumn("dbo.Devs", "EmailDev", c => c.String());
            AddColumn("dbo.GerenteProjs", "EmailGp", c => c.String());
            AddColumn("dbo.Testers", "EmailTester", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testers", "EmailTester");
            DropColumn("dbo.GerenteProjs", "EmailGp");
            DropColumn("dbo.Devs", "EmailDev");
            DropColumn("dbo.Bugs", "UrlRepoCodigo");
            DropColumn("dbo.Bugs", "BugAceito");
        }
    }
}
