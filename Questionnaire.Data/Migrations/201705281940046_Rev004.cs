namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev004 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.QuestionBases_Html", "html", c => c.String(maxLength: 1024));
            AlterColumn("dbo.LocalizedStrings", "LocalizedValue", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LocalizedStrings", "LocalizedValue", c => c.String());
            AlterColumn("dbo.QuestionBases_Html", "html", c => c.String());
        }
    }
}
