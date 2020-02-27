namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populartiposbugstestes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Unit�rio')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Integra��o')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Teste de Fuma�a')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Teste de Interface')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Teste de Regress�o')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Performance')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Carga')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Stress')");
            Sql("INSERT INTO TiposTestes (TipoTeste) VALUES ('Beta/Aceita��o')");

            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Erro 500')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Erro 404')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Crash')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Interface')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Typos')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Erro de Fluxo')");
            Sql("INSERT INTO TiposBugs (TipoBug) VALUES ('Erro de Calculo')");
        }
        
        public override void Down()
        {
        }
    }
}
