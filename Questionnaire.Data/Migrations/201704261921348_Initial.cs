namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatrixRows",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ComplexMatrixId = c.Int(),
                    PlainMatrixId = c.Int(),
                    value = c.String(maxLength: 256),
                    text = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ComplexMatrixId)
                .Index(t => t.PlainMatrixId);

            CreateTable(
                "dbo.QuestionBases",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    PageId = c.Int(),
                    ComplexMatrixBaseId = c.Int(),
                    name = c.String(maxLength: 512),
                    title = c.String(maxLength: 512),
                    visibleIf = c.String(maxLength: 1024),
                    readOnly = c.Boolean(nullable: false),
                    startWithNewLine = c.Boolean(nullable: false),
                    width = c.String(maxLength: 16),
                    indent = c.Int(nullable: false),
                    isRequired = c.Boolean(nullable: false),
                    visible = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pages", t => t.PageId, cascadeDelete: true)
                .Index(t => t.PageId)
                .Index(t => t.ComplexMatrixBaseId);

            CreateTable(
                "dbo.Choices",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    QuestionId = c.Int(),
                    RatingId = c.Int(),
                    PlainMatrixId = c.Int(),
                    text = c.String(maxLength: 256),
                    value = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.QuestionId)
                .Index(t => t.RatingId)
                .Index(t => t.PlainMatrixId);

            CreateTable(
                "dbo.RestfullChoiceSources",
                c => new
                {
                    Id = c.Int(nullable: false),
                    Url = c.String(),
                    Path = c.String(),
                    ValueName = c.String(),
                    TitleName = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases_SelectBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Pages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SurveyId = c.Int(nullable: false),
                    innerIndent = c.Int(nullable: false),
                    name = c.String(maxLength: 256),
                    navigationButtonsVisibility = c.String(maxLength: 16),
                    title = c.String(maxLength: 256),
                    visible = c.Boolean(nullable: false),
                    visibleIf = c.String(maxLength: 1024),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);

            CreateTable(
                "dbo.Validators",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    QuestionId = c.Int(nullable: false),
                    Text = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);

            CreateTable(
                "dbo.Surveys",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IsActive = c.Boolean(nullable: false),
                    clearInvisibleValues = c.Boolean(nullable: false),
                    completeText = c.String(maxLength: 1024),
                    completedHtml = c.String(),
                    cookieName = c.String(maxLength: 256),
                    focusFirstQuestionAutomatic = c.Boolean(nullable: false),
                    goNextPageAutomatic = c.Boolean(nullable: false),
                    locale = c.String(maxLength: 8),
                    mode = c.String(maxLength: 16),
                    pageNextText = c.String(maxLength: 32),
                    pagePrevText = c.String(maxLength: 32),
                    questionStartIndex = c.String(maxLength: 32),
                    questionTitleLocationValue = c.String(maxLength: 16),
                    questionTitleTemplate = c.String(maxLength: 256),
                    requiredText = c.String(maxLength: 32),
                    sendResultOnPageNext = c.Boolean(nullable: false),
                    showCompletedPage = c.Boolean(nullable: false),
                    showNavigationButtons = c.Boolean(nullable: false),
                    showPageNumbers = c.Boolean(nullable: false),
                    showPageTitles = c.Boolean(nullable: false),
                    showProgressBar = c.String(maxLength: 16),
                    showQuestionNumbers = c.String(maxLength: 16),
                    showTitle = c.Boolean(nullable: false),
                    storeOthersAsComment = c.Boolean(nullable: false),
                    surveyId = c.String(maxLength: 256),
                    surveyPostId = c.String(maxLength: 256),
                    title = c.String(maxLength: 1024),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Triggers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SurveyId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);

            CreateTable(
                "dbo.QuestionBases_SelectBases",
                c => new
                {
                    Id = c.Int(nullable: false),
                    hasComment = c.Boolean(nullable: false),
                    commentText = c.String(maxLength: 1024),
                    choicesOrder = c.String(maxLength: 8),
                    hasOther = c.Boolean(nullable: false),
                    otherText = c.String(maxLength: 256),
                    otherErrorText = c.String(maxLength: 1024),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.SelectBases_ComplexMatrixBase",
                c => new
                {
                    Id = c.Int(nullable: false),
                    cellType = c.String(maxLength: 16),
                    columnColCount = c.Int(nullable: false),
                    columnMinWidth = c.String(maxLength: 16),
                    horizontalScroll = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases_SelectBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ComplexMatrixBases_ComplexMatrix",
                c => new
                {
                    Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_PlainMatrix",
                c => new
                {
                    Id = c.Int(nullable: false),
                    isAllRowRequired = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Validators_AnswerCount",
                c => new
                {
                    Id = c.Int(nullable: false),
                    minCount = c.Int(nullable: false),
                    maxCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Validators_Email",
                c => new
                {
                    Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Validators_Numeric",
                c => new
                {
                    Id = c.Int(nullable: false),
                    minValue = c.Int(nullable: false),
                    maxValue = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Validators_Regex",
                c => new
                {
                    Id = c.Int(nullable: false),
                    regex = c.String(maxLength: 1024),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Validators_Text",
                c => new
                {
                    Id = c.Int(nullable: false),
                    minLength = c.Int(nullable: false),
                    maxLength = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_Comment",
                c => new
                {
                    Id = c.Int(nullable: false),
                    rows = c.Int(nullable: false),
                    cols = c.Int(nullable: false),
                    placeHolder = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_File",
                c => new
                {
                    Id = c.Int(nullable: false),
                    imageHeight = c.String(maxLength: 16),
                    imageWidth = c.String(maxLength: 16),
                    showPreview = c.Boolean(nullable: false),
                    storeDataAsText = c.Boolean(nullable: false),
                    maxSize = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_Html",
                c => new
                {
                    Id = c.Int(nullable: false),
                    html = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_MultipleText",
                c => new
                {
                    Id = c.Int(nullable: false),
                    colCount = c.Int(nullable: false),
                    itemSize = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_MultipleTextItem",
                c => new
                {
                    Id = c.Int(nullable: false),
                    MultipleTextQuestionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id)
                .ForeignKey("dbo.QuestionBases_MultipleText", t => t.MultipleTextQuestionId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.MultipleTextQuestionId);

            CreateTable(
                "dbo.QuestionBases_Rating",
                c => new
                {
                    Id = c.Int(nullable: false),
                    mininumRateDescription = c.String(maxLength: 256),
                    maximumRateDescription = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.QuestionBases_Text",
                c => new
                {
                    Id = c.Int(nullable: false),
                    size = c.Int(nullable: false),
                    inputType = c.String(maxLength: 16),
                    placeHolder = c.String(maxLength: 512),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.SelectBases_CheckBox",
                c => new
                {
                    Id = c.Int(nullable: false),
                    colCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases_SelectBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.SelectBases_Dropdown",
                c => new
                {
                    Id = c.Int(nullable: false),
                    optionsCaption = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases_SelectBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.SelectBases_Radiogroup",
                c => new
                {
                    Id = c.Int(nullable: false),
                    colCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionBases_SelectBases", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ComplexMatrixBases_ComplexMatrixDynamic",
                c => new
                {
                    Id = c.Int(nullable: false),
                    addRowText = c.String(maxLength: 64),
                    removeRowText = c.String(maxLength: 64),
                    rowCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            //************************************************************
            //
            //             F O R E I G N     K E Y S
            //
            //************************************************************

            // Replace automatically created index "dbo.QuestionBases".ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.ComplexMatrixBaseId)
            Sql(Properties.Resources.ForeignKey_ComplexMatrixBaseQuestionBase);

            // Replace automatically created index "dbo.MatrixRows".ForeignKey("dbo.ComplexMatrixBases_ComplexMatrix", t => t.ComplexMatrixId)
            Sql(Properties.Resources.ForeignKey_ComplexMatrixMatrixRows);

            // Replace automatically created index "dbo.MatrixRows".ForeignKey("dbo.QuestionBases_PlainMatrix", t => t.PlainMatrixId)
            Sql(Properties.Resources.ForeignKey_PlainMatrixMatrixRows);

            // Replace automatically created index "dbo.Choices".ForeignKey("dbo.QuestionBases_SelectBases", t => t.QuestionId)
            Sql(Properties.Resources.ForeignKey_SelectQuestionBaseChoices);

            // Replace automatically created index "dbo.Choices".ForeignKey("dbo.QuestionBases_PlainMatrix", t => t.PlainMatrixId)
            Sql(Properties.Resources.ForeignKey_PlainMatrixChoices);

            // Replace automatically created index "dbo.Choices".ForeignKey("dbo.QuestionBases_Rating", t => t.RatingId)
            Sql(Properties.Resources.ForeignKey_RatingChoices);

            //************************************************************
            //
            //                      T R I G G E R S
            //
            //************************************************************

            // Once deleted base entry (QuestionBase) should be deleted as well
            Sql(Properties.Resources.Trigger_DeleteMultipleTextItemBase);

            // When deleted, Columns (also instances of QuestionBase class) should be deleted
            Sql(Properties.Resources.Trigger_DeleteComplexMatrixColumns);

            // Triggers for deleting Choices 
            Sql(Properties.Resources.Trigger_DeleteChoicesOfPlainMatrix);
            Sql(Properties.Resources.Trigger_DeleteChoicesOfRating);
            Sql(Properties.Resources.Trigger_DeleteChoicesOfSelectBase);

            // Triggers for deleting Matrix Rows
            Sql(Properties.Resources.Trigger_DeleteMatrixRowsOfComplexMatrix);
            Sql(Properties.Resources.Trigger_DeleteMatrixRowsOfPlainMatrix);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ComplexMatrixBases_ComplexMatrixDynamic", "Id", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.SelectBases_Radiogroup", "Id", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.SelectBases_Dropdown", "Id", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.SelectBases_CheckBox", "Id", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.QuestionBases_Text", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_Rating", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_MultipleTextItem", "MultipleTextQuestionId", "dbo.QuestionBases_MultipleText");
            DropForeignKey("dbo.QuestionBases_MultipleTextItem", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_MultipleText", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_Html", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_File", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases_Comment", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.Validators_Text", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Regex", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Numeric", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Email", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_AnswerCount", "Id", "dbo.Validators");
            DropForeignKey("dbo.QuestionBases_PlainMatrix", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.ComplexMatrixBases_ComplexMatrix", "Id", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.SelectBases_ComplexMatrixBase", "Id", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.QuestionBases_SelectBases", "Id", "dbo.QuestionBases");
            DropForeignKey("dbo.MatrixRows", "ComplexMatrixId", "dbo.ComplexMatrixBases_ComplexMatrix");
            DropForeignKey("dbo.MatrixRows", "PlainMatrixId", "dbo.QuestionBases_PlainMatrix");
            DropForeignKey("dbo.RestfullChoiceSources", "Id", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.Triggers", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Pages", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Choices", "RatingId", "dbo.QuestionBases_Rating");
            DropForeignKey("dbo.Validators", "QuestionId", "dbo.QuestionBases");
            DropForeignKey("dbo.QuestionBases", "PageId", "dbo.Pages");
            DropForeignKey("dbo.QuestionBases", "ComplexMatrixBaseId", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.QuestionBases_SelectBases");
            DropForeignKey("dbo.Choices", "PlainMatrixId", "dbo.QuestionBases_PlainMatrix");
            DropIndex("dbo.ComplexMatrixBases_ComplexMatrixDynamic", new[] { "Id" });
            DropIndex("dbo.SelectBases_Radiogroup", new[] { "Id" });
            DropIndex("dbo.SelectBases_Dropdown", new[] { "Id" });
            DropIndex("dbo.SelectBases_CheckBox", new[] { "Id" });
            DropIndex("dbo.QuestionBases_Text", new[] { "Id" });
            DropIndex("dbo.QuestionBases_Rating", new[] { "Id" });
            DropIndex("dbo.QuestionBases_MultipleTextItem", new[] { "MultipleTextQuestionId" });
            DropIndex("dbo.QuestionBases_MultipleTextItem", new[] { "Id" });
            DropIndex("dbo.QuestionBases_MultipleText", new[] { "Id" });
            DropIndex("dbo.QuestionBases_Html", new[] { "Id" });
            DropIndex("dbo.QuestionBases_File", new[] { "Id" });
            DropIndex("dbo.QuestionBases_Comment", new[] { "Id" });
            DropIndex("dbo.Validators_Text", new[] { "Id" });
            DropIndex("dbo.Validators_Regex", new[] { "Id" });
            DropIndex("dbo.Validators_Numeric", new[] { "Id" });
            DropIndex("dbo.Validators_Email", new[] { "Id" });
            DropIndex("dbo.Validators_AnswerCount", new[] { "Id" });
            DropIndex("dbo.QuestionBases_PlainMatrix", new[] { "Id" });
            DropIndex("dbo.ComplexMatrixBases_ComplexMatrix", new[] { "Id" });
            DropIndex("dbo.SelectBases_ComplexMatrixBase", new[] { "Id" });
            DropIndex("dbo.QuestionBases_SelectBases", new[] { "Id" });
            DropIndex("dbo.Triggers", new[] { "SurveyId" });
            DropIndex("dbo.Validators", new[] { "QuestionId" });
            DropIndex("dbo.Pages", new[] { "SurveyId" });
            DropIndex("dbo.RestfullChoiceSources", new[] { "Id" });
            DropIndex("dbo.Choices", new[] { "PlainMatrixId" });
            DropIndex("dbo.Choices", new[] { "RatingId" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropIndex("dbo.QuestionBases", new[] { "ComplexMatrixBaseId" });
            DropIndex("dbo.QuestionBases", new[] { "PageId" });
            DropIndex("dbo.MatrixRows", new[] { "PlainMatrixId" });
            DropIndex("dbo.MatrixRows", new[] { "ComplexMatrixId" });
            DropTable("dbo.ComplexMatrixBases_ComplexMatrixDynamic");
            DropTable("dbo.SelectBases_Radiogroup");
            DropTable("dbo.SelectBases_Dropdown");
            DropTable("dbo.SelectBases_CheckBox");
            DropTable("dbo.QuestionBases_Text");
            DropTable("dbo.QuestionBases_Rating");
            DropTable("dbo.QuestionBases_MultipleTextItem");
            DropTable("dbo.QuestionBases_MultipleText");
            DropTable("dbo.QuestionBases_Html");
            DropTable("dbo.QuestionBases_File");
            DropTable("dbo.QuestionBases_Comment");
            DropTable("dbo.Validators_Text");
            DropTable("dbo.Validators_Regex");
            DropTable("dbo.Validators_Numeric");
            DropTable("dbo.Validators_Email");
            DropTable("dbo.Validators_AnswerCount");
            DropTable("dbo.QuestionBases_PlainMatrix");
            DropTable("dbo.ComplexMatrixBases_ComplexMatrix");
            DropTable("dbo.SelectBases_ComplexMatrixBase");
            DropTable("dbo.QuestionBases_SelectBases");
            DropTable("dbo.Triggers");
            DropTable("dbo.Surveys");
            DropTable("dbo.Validators");
            DropTable("dbo.Pages");
            DropTable("dbo.RestfullChoiceSources");
            DropTable("dbo.Choices");
            DropTable("dbo.QuestionBases");
            DropTable("dbo.MatrixRows");
        }
    }
}
