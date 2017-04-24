namespace Questionnaire.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatrixRow",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplexMatrixId = c.Int(),
                        PlainMatrixId = c.Int(),
                        value = c.String(maxLength: 256),
                        text = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases_PlainMatrix", t => t.PlainMatrixId)
                .ForeignKey("dbo.ComplexMatrixBases_ComplexMatrix", t => t.ComplexMatrixId)
                .Index(t => t.ComplexMatrixId)
                .Index(t => t.PlainMatrixId);
            
            CreateTable(
                "dbo.QDefBases",
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
                .ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.ComplexMatrixBaseId)
                .ForeignKey("dbo.Pages", t => t.PageId)
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
                .ForeignKey("dbo.QDefBases_PlainMatrix", t => t.PlainMatrixId)
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.QuestionId)
                .ForeignKey("dbo.QDefBases_Rating", t => t.RatingId)
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
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
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
                .ForeignKey("dbo.QDefBases", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        SurveyJson = c.String(),
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
                "dbo.QDefBases_SelectBases",
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
                .ForeignKey("dbo.QDefBases", t => t.Id)
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
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ComplexMatrixBases_ComplexMatrix",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_PlainMatrix",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        isAllRowRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
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
                .ForeignKey("dbo.Validators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Validators_Email",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id)
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
                .ForeignKey("dbo.Validators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Validators_Regex",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        regex = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validators", t => t.Id)
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
                .ForeignKey("dbo.Validators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_File",
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
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_Html",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        html = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_MultipleText",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        colCount = c.Int(nullable: false),
                        itemSize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_MultipleTextItem",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MultipleTextQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .ForeignKey("dbo.QDefBases_MultipleText", t => t.MultipleTextQuestionId)
                .Index(t => t.Id)
                .Index(t => t.MultipleTextQuestionId);
            
            CreateTable(
                "dbo.QDefBases_Rating",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        mininumRateDescription = c.String(maxLength: 256),
                        maximumRateDescription = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_Text",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        size = c.Int(nullable: false),
                        inputType = c.String(maxLength: 16),
                        placeHolder = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SelectBases_CheckBox",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        colCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SelectBases_Dropdown",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        optionsCaption = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SelectBases_Radiogroup",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        colCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QDefBases_Comment",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        rows = c.Int(nullable: false),
                        cols = c.Int(nullable: false),
                        placeHolder = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QDefBases_SelectBases", t => t.Id)
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
                .ForeignKey("dbo.SelectBases_ComplexMatrixBase", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComplexMatrixBases_ComplexMatrixDynamic", "Id", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.QDefBases_Comment", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.SelectBases_Radiogroup", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.SelectBases_Dropdown", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.SelectBases_CheckBox", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.QDefBases_Text", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases_Rating", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases_MultipleTextItem", "MultipleTextQuestionId", "dbo.QDefBases_MultipleText");
            DropForeignKey("dbo.QDefBases_MultipleTextItem", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases_MultipleText", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases_Html", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases_File", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.Validators_Text", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Regex", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Numeric", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_Email", "Id", "dbo.Validators");
            DropForeignKey("dbo.Validators_AnswerCount", "Id", "dbo.Validators");
            DropForeignKey("dbo.QDefBases_PlainMatrix", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.ComplexMatrixBases_ComplexMatrix", "Id", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.SelectBases_ComplexMatrixBase", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.QDefBases_SelectBases", "Id", "dbo.QDefBases");
            DropForeignKey("dbo.MatrixRow", "ComplexMatrixId", "dbo.ComplexMatrixBases_ComplexMatrix");
            DropForeignKey("dbo.MatrixRow", "PlainMatrixId", "dbo.QDefBases_PlainMatrix");
            DropForeignKey("dbo.RestfullChoiceSources", "Id", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.Triggers", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Pages", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Choices", "RatingId", "dbo.QDefBases_Rating");
            DropForeignKey("dbo.Validators", "QuestionId", "dbo.QDefBases");
            DropForeignKey("dbo.QDefBases", "PageId", "dbo.Pages");
            DropForeignKey("dbo.QDefBases", "ComplexMatrixBaseId", "dbo.SelectBases_ComplexMatrixBase");
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.QDefBases_SelectBases");
            DropForeignKey("dbo.Choices", "PlainMatrixId", "dbo.QDefBases_PlainMatrix");
            DropIndex("dbo.ComplexMatrixBases_ComplexMatrixDynamic", new[] { "Id" });
            DropIndex("dbo.QDefBases_Comment", new[] { "Id" });
            DropIndex("dbo.SelectBases_Radiogroup", new[] { "Id" });
            DropIndex("dbo.SelectBases_Dropdown", new[] { "Id" });
            DropIndex("dbo.SelectBases_CheckBox", new[] { "Id" });
            DropIndex("dbo.QDefBases_Text", new[] { "Id" });
            DropIndex("dbo.QDefBases_Rating", new[] { "Id" });
            DropIndex("dbo.QDefBases_MultipleTextItem", new[] { "MultipleTextQuestionId" });
            DropIndex("dbo.QDefBases_MultipleTextItem", new[] { "Id" });
            DropIndex("dbo.QDefBases_MultipleText", new[] { "Id" });
            DropIndex("dbo.QDefBases_Html", new[] { "Id" });
            DropIndex("dbo.QDefBases_File", new[] { "Id" });
            DropIndex("dbo.Validators_Text", new[] { "Id" });
            DropIndex("dbo.Validators_Regex", new[] { "Id" });
            DropIndex("dbo.Validators_Numeric", new[] { "Id" });
            DropIndex("dbo.Validators_Email", new[] { "Id" });
            DropIndex("dbo.Validators_AnswerCount", new[] { "Id" });
            DropIndex("dbo.QDefBases_PlainMatrix", new[] { "Id" });
            DropIndex("dbo.ComplexMatrixBases_ComplexMatrix", new[] { "Id" });
            DropIndex("dbo.SelectBases_ComplexMatrixBase", new[] { "Id" });
            DropIndex("dbo.QDefBases_SelectBases", new[] { "Id" });
            DropIndex("dbo.Triggers", new[] { "SurveyId" });
            DropIndex("dbo.Validators", new[] { "QuestionId" });
            DropIndex("dbo.Pages", new[] { "SurveyId" });
            DropIndex("dbo.RestfullChoiceSources", new[] { "Id" });
            DropIndex("dbo.Choices", new[] { "PlainMatrixId" });
            DropIndex("dbo.Choices", new[] { "RatingId" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropIndex("dbo.QDefBases", new[] { "ComplexMatrixBaseId" });
            DropIndex("dbo.QDefBases", new[] { "PageId" });
            DropIndex("dbo.MatrixRow", new[] { "PlainMatrixId" });
            DropIndex("dbo.MatrixRow", new[] { "ComplexMatrixId" });
            DropTable("dbo.ComplexMatrixBases_ComplexMatrixDynamic");
            DropTable("dbo.QDefBases_Comment");
            DropTable("dbo.SelectBases_Radiogroup");
            DropTable("dbo.SelectBases_Dropdown");
            DropTable("dbo.SelectBases_CheckBox");
            DropTable("dbo.QDefBases_Text");
            DropTable("dbo.QDefBases_Rating");
            DropTable("dbo.QDefBases_MultipleTextItem");
            DropTable("dbo.QDefBases_MultipleText");
            DropTable("dbo.QDefBases_Html");
            DropTable("dbo.QDefBases_File");
            DropTable("dbo.Validators_Text");
            DropTable("dbo.Validators_Regex");
            DropTable("dbo.Validators_Numeric");
            DropTable("dbo.Validators_Email");
            DropTable("dbo.Validators_AnswerCount");
            DropTable("dbo.QDefBases_PlainMatrix");
            DropTable("dbo.ComplexMatrixBases_ComplexMatrix");
            DropTable("dbo.SelectBases_ComplexMatrixBase");
            DropTable("dbo.QDefBases_SelectBases");
            DropTable("dbo.Triggers");
            DropTable("dbo.Surveys");
            DropTable("dbo.Validators");
            DropTable("dbo.Pages");
            DropTable("dbo.RestfullChoiceSources");
            DropTable("dbo.Choices");
            DropTable("dbo.QDefBases");
            DropTable("dbo.MatrixRow");
        }
    }
}
