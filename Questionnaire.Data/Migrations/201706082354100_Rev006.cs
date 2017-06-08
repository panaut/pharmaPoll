namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ElementBases_QuestionBase", "renderAs", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ElementBases_QuestionBase", "renderAs");
        }
    }
}
