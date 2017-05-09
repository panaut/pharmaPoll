namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev001 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.QuestionBases_MultipleTextItem", new[] { "MultipleTextQuestionId" });
            AddColumn("dbo.QuestionBases_Rating", "minRateDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.QuestionBases_Rating", "maxRateDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.Choices", "SurveyId", c => c.Int(nullable: false));
            AddColumn("dbo.MatrixRows", "SurveyId", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionBases_MultipleTextItem", "MultipleTextQuestionId", c => c.Int());
            CreateIndex("dbo.Choices", "SurveyId");
            CreateIndex("dbo.MatrixRows", "SurveyId");
            CreateIndex("dbo.QuestionBases_MultipleTextItem", "MultipleTextQuestionId");
            AddForeignKey("dbo.Choices", "SurveyId", "dbo.Surveys", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MatrixRows", "SurveyId", "dbo.Surveys", "Id", cascadeDelete: true);
            DropColumn("dbo.QuestionBases_Rating", "mininumRateDescription");
            DropColumn("dbo.QuestionBases_Rating", "maximumRateDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionBases_Rating", "maximumRateDescription", c => c.String(maxLength: 256));
            AddColumn("dbo.QuestionBases_Rating", "mininumRateDescription", c => c.String(maxLength: 256));
            DropForeignKey("dbo.MatrixRows", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Choices", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.QuestionBases_MultipleTextItem", new[] { "MultipleTextQuestionId" });
            DropIndex("dbo.MatrixRows", new[] { "SurveyId" });
            DropIndex("dbo.Choices", new[] { "SurveyId" });
            AlterColumn("dbo.QuestionBases_MultipleTextItem", "MultipleTextQuestionId", c => c.Int(nullable: false));
            DropColumn("dbo.MatrixRows", "SurveyId");
            DropColumn("dbo.Choices", "SurveyId");
            DropColumn("dbo.QuestionBases_Rating", "maxRateDescription");
            DropColumn("dbo.QuestionBases_Rating", "minRateDescription");
            CreateIndex("dbo.QuestionBases_MultipleTextItem", "MultipleTextQuestionId");
        }
    }
}
