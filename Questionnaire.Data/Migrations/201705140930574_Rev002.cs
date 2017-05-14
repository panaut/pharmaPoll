namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rev002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalizedStrings",
                c => new
                    {
                        TypeIdentifier = c.String(nullable: false, maxLength: 128),
                        TypeUniqueId = c.Int(nullable: false),
                        FieldIdentifier = c.String(nullable: false, maxLength: 128),
                        Culture = c.Int(nullable: false),
                        SurveyId = c.Int(nullable: false),
                        LocalizedValue = c.String(),
                    })
                .PrimaryKey(t => new { t.TypeIdentifier, t.TypeUniqueId, t.FieldIdentifier, t.Culture })
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.VotingSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        ClientIP = c.String(maxLength: 15),
                        Code = c.String(maxLength: 128),
                        InitializationTime = c.DateTime(nullable: false),
                        LastSubmissionTime = c.DateTime(nullable: false),
                        NumberOfTimesSubmitted = c.Int(nullable: false),
                        JsonData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.Code, unique: true);
            
            AddColumn("dbo.ElementBases", "PositionWithinContainer", c => c.Int(nullable: false));
            AlterColumn("dbo.Surveys", "surveyId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Surveys", "surveyId", unique: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VotingSessions", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.LocalizedStrings", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.VotingSessions", new[] { "Code" });
            DropIndex("dbo.VotingSessions", new[] { "SurveyId" });
            DropIndex("dbo.LocalizedStrings", new[] { "SurveyId" });
            DropIndex("dbo.Surveys", new[] { "surveyId" });
            AlterColumn("dbo.Surveys", "surveyId", c => c.String(maxLength: 256));
            DropColumn("dbo.ElementBases", "PositionWithinContainer");
            DropTable("dbo.VotingSessions");
            DropTable("dbo.LocalizedStrings");
        }
    }
}
