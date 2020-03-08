namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudancacamposrequeridosdatainicio : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bugs", "DataBug", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tasks", "DataInicio", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projetos", "DataInicio", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projetos", "DataEstimada", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projetos", "DataEstimada", c => c.DateTime());
            AlterColumn("dbo.Projetos", "DataInicio", c => c.DateTime());
            AlterColumn("dbo.Tasks", "DataInicio", c => c.DateTime());
            AlterColumn("dbo.Bugs", "DataBug", c => c.DateTime());
        }
    }
}
