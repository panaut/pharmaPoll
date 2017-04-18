
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/19/2017 00:09:21
-- Generated from EDMX file: E:\GitHub\pharmaPoll\Questionnaire.Data\SurveyModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SurveyDemo];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SurveyQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases] DROP CONSTRAINT [FK_SurveyQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_SelectQuestionTemplateBaseValueTextPair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValueTextPairs] DROP CONSTRAINT [FK_SelectQuestionTemplateBaseValueTextPair];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixDropdownBaseTemplateMatrixColumnTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate] DROP CONSTRAINT [FK_MatrixDropdownBaseTemplateMatrixColumnTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixQuestionTemplateMatrixColumnTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate] DROP CONSTRAINT [FK_MatrixQuestionTemplateMatrixColumnTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixColumnTemplateValueTextPair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate] DROP CONSTRAINT [FK_MatrixColumnTemplateValueTextPair];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixQuestionTemplateMatrixRowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate] DROP CONSTRAINT [FK_MatrixQuestionTemplateMatrixRowTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixDropdownQuestionTemplateMatrixRowTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate] DROP CONSTRAINT [FK_MatrixDropdownQuestionTemplateMatrixRowTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_MultipleTextQuestionTemplateQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases] DROP CONSTRAINT [FK_MultipleTextQuestionTemplateQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_SelectQuestionTemplateBase_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase] DROP CONSTRAINT [FK_SelectQuestionTemplateBase_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixDropdownBaseTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate] DROP CONSTRAINT [FK_MatrixDropdownBaseTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixColumnTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate] DROP CONSTRAINT [FK_MatrixColumnTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixQuestionTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixQuestionTemplate] DROP CONSTRAINT [FK_MatrixQuestionTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixRowTemplate_inherits_ValueTextPair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate] DROP CONSTRAINT [FK_MatrixRowTemplate_inherits_ValueTextPair];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixDropdownQuestionTemplate_inherits_MatrixDropdownBaseTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate] DROP CONSTRAINT [FK_MatrixDropdownQuestionTemplate_inherits_MatrixDropdownBaseTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_MultipleTextQuestionTemplate_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate] DROP CONSTRAINT [FK_MultipleTextQuestionTemplate_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_TextQuestionTemplate_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate] DROP CONSTRAINT [FK_TextQuestionTemplate_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentQuestionTemplate_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate] DROP CONSTRAINT [FK_CommentQuestionTemplate_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_CheckBoxQuestionTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate] DROP CONSTRAINT [FK_CheckBoxQuestionTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_RadiogroupQuestionTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate] DROP CONSTRAINT [FK_RadiogroupQuestionTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_DropdownQuestionTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_DropdownQuestionTemplate] DROP CONSTRAINT [FK_DropdownQuestionTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_RatingQuestionTemplate_inherits_SelectQuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_RatingQuestionTemplate] DROP CONSTRAINT [FK_RatingQuestionTemplate_inherits_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_HtmlQuestionTemplate_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_HtmlQuestionTemplate] DROP CONSTRAINT [FK_HtmlQuestionTemplate_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_FileQuestionBase_inherits_QuestionTemplateBase]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_FileQuestionBase] DROP CONSTRAINT [FK_FileQuestionBase_inherits_QuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[FK_MatrixDynamicQuestionTemplate_inherits_MatrixDropdownBaseTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate] DROP CONSTRAINT [FK_MatrixDynamicQuestionTemplate_inherits_MatrixDropdownBaseTemplate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Surveys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Surveys];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases];
GO
IF OBJECT_ID(N'[dbo].[ValueTextPairs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValueTextPairs];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MatrixColumnTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MatrixQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MatrixQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[ValueTextPairs_MatrixRowTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValueTextPairs_MatrixRowTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_TextQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_CommentQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_DropdownQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_DropdownQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_RatingQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_RatingQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_HtmlQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_HtmlQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_FileQuestionBase]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_FileQuestionBase];
GO
IF OBJECT_ID(N'[dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Surveys'
CREATE TABLE [dbo].[Surveys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NULL,
    [IsActive] bit  NOT NULL,
    [SurveyJson] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases'
CREATE TABLE [dbo].[QuestionTemplateBases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [Title] nvarchar(512)  NULL,
    [SurveyId] int  NOT NULL,
    [MultipleTextQuestionTemplateId] int  NULL
);
GO

-- Creating table 'ValueTextPairs'
CREATE TABLE [dbo].[ValueTextPairs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(256)  NULL,
    [Text] nvarchar(256)  NULL,
    [SelectQuestionTemplateBaseId] int  NULL
);
GO

-- Creating table 'QuestionTemplateBases_SelectQuestionTemplateBase'
CREATE TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase] (
    [CommentText] nvarchar(1024)  NULL,
    [OtherText] nvarchar(256)  NULL,
    [OtherErrorText] nvarchar(1024)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MatrixDropdownBaseTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MatrixColumnTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate] (
    [MatrixDropdownBaseTemplateId] int  NULL,
    [MatrixQuestionTemplateId] int  NULL,
    [Id] int  NOT NULL,
    [ValueTextPair_Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MatrixQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MatrixQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'ValueTextPairs_MatrixRowTemplate'
CREATE TABLE [dbo].[ValueTextPairs_MatrixRowTemplate] (
    [MatrixQuestionTemplateId] int  NULL,
    [MatrixDropdownQuestionTemplateId] int  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MatrixDropdownQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MultipleTextQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_TextQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate] (
    [Placeholder] nvarchar(512)  NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_CommentQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_CheckBoxQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_RadiogroupQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_DropdownQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_DropdownQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_RatingQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_RatingQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_HtmlQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_HtmlQuestionTemplate] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_FileQuestionBase'
CREATE TABLE [dbo].[QuestionTemplateBases_FileQuestionBase] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_MatrixDynamicQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate] (
    [AddRowText] nvarchar(256)  NULL,
    [RemoveRowText] nvarchar(256)  NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Surveys'
ALTER TABLE [dbo].[Surveys]
ADD CONSTRAINT [PK_Surveys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases'
ALTER TABLE [dbo].[QuestionTemplateBases]
ADD CONSTRAINT [PK_QuestionTemplateBases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValueTextPairs'
ALTER TABLE [dbo].[ValueTextPairs]
ADD CONSTRAINT [PK_ValueTextPairs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_SelectQuestionTemplateBase'
ALTER TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
ADD CONSTRAINT [PK_QuestionTemplateBases_SelectQuestionTemplateBase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MatrixDropdownBaseTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MatrixDropdownBaseTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MatrixColumnTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MatrixColumnTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MatrixQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MatrixQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ValueTextPairs_MatrixRowTemplate'
ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate]
ADD CONSTRAINT [PK_ValueTextPairs_MatrixRowTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MatrixDropdownQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MatrixDropdownQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MultipleTextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MultipleTextQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_TextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_TextQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_CommentQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_CommentQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_CheckBoxQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_CheckBoxQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_RadiogroupQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_RadiogroupQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_DropdownQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_DropdownQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_DropdownQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_RatingQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_RatingQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_RatingQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_HtmlQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_HtmlQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_HtmlQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_FileQuestionBase'
ALTER TABLE [dbo].[QuestionTemplateBases_FileQuestionBase]
ADD CONSTRAINT [PK_QuestionTemplateBases_FileQuestionBase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_MatrixDynamicQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_MatrixDynamicQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SurveyId] in table 'QuestionTemplateBases'
ALTER TABLE [dbo].[QuestionTemplateBases]
ADD CONSTRAINT [FK_SurveyQuestionTemplateBase]
    FOREIGN KEY ([SurveyId])
    REFERENCES [dbo].[Surveys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveyQuestionTemplateBase'
CREATE INDEX [IX_FK_SurveyQuestionTemplateBase]
ON [dbo].[QuestionTemplateBases]
    ([SurveyId]);
GO

-- Creating foreign key on [SelectQuestionTemplateBaseId] in table 'ValueTextPairs'
ALTER TABLE [dbo].[ValueTextPairs]
ADD CONSTRAINT [FK_SelectQuestionTemplateBaseValueTextPair]
    FOREIGN KEY ([SelectQuestionTemplateBaseId])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SelectQuestionTemplateBaseValueTextPair'
CREATE INDEX [IX_FK_SelectQuestionTemplateBaseValueTextPair]
ON [dbo].[ValueTextPairs]
    ([SelectQuestionTemplateBaseId]);
GO

-- Creating foreign key on [MatrixDropdownBaseTemplateId] in table 'QuestionTemplateBases_MatrixColumnTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
ADD CONSTRAINT [FK_MatrixDropdownBaseTemplateMatrixColumnTemplate]
    FOREIGN KEY ([MatrixDropdownBaseTemplateId])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrixDropdownBaseTemplateMatrixColumnTemplate'
CREATE INDEX [IX_FK_MatrixDropdownBaseTemplateMatrixColumnTemplate]
ON [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
    ([MatrixDropdownBaseTemplateId]);
GO

-- Creating foreign key on [MatrixQuestionTemplateId] in table 'QuestionTemplateBases_MatrixColumnTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
ADD CONSTRAINT [FK_MatrixQuestionTemplateMatrixColumnTemplate]
    FOREIGN KEY ([MatrixQuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixQuestionTemplate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrixQuestionTemplateMatrixColumnTemplate'
CREATE INDEX [IX_FK_MatrixQuestionTemplateMatrixColumnTemplate]
ON [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
    ([MatrixQuestionTemplateId]);
GO

-- Creating foreign key on [ValueTextPair_Id] in table 'QuestionTemplateBases_MatrixColumnTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
ADD CONSTRAINT [FK_MatrixColumnTemplateValueTextPair]
    FOREIGN KEY ([ValueTextPair_Id])
    REFERENCES [dbo].[ValueTextPairs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrixColumnTemplateValueTextPair'
CREATE INDEX [IX_FK_MatrixColumnTemplateValueTextPair]
ON [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
    ([ValueTextPair_Id]);
GO

-- Creating foreign key on [MatrixQuestionTemplateId] in table 'ValueTextPairs_MatrixRowTemplate'
ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate]
ADD CONSTRAINT [FK_MatrixQuestionTemplateMatrixRowTemplate]
    FOREIGN KEY ([MatrixQuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixQuestionTemplate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrixQuestionTemplateMatrixRowTemplate'
CREATE INDEX [IX_FK_MatrixQuestionTemplateMatrixRowTemplate]
ON [dbo].[ValueTextPairs_MatrixRowTemplate]
    ([MatrixQuestionTemplateId]);
GO

-- Creating foreign key on [MatrixDropdownQuestionTemplateId] in table 'ValueTextPairs_MatrixRowTemplate'
ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate]
ADD CONSTRAINT [FK_MatrixDropdownQuestionTemplateMatrixRowTemplate]
    FOREIGN KEY ([MatrixDropdownQuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MatrixDropdownQuestionTemplateMatrixRowTemplate'
CREATE INDEX [IX_FK_MatrixDropdownQuestionTemplateMatrixRowTemplate]
ON [dbo].[ValueTextPairs_MatrixRowTemplate]
    ([MatrixDropdownQuestionTemplateId]);
GO

-- Creating foreign key on [MultipleTextQuestionTemplateId] in table 'QuestionTemplateBases'
ALTER TABLE [dbo].[QuestionTemplateBases]
ADD CONSTRAINT [FK_MultipleTextQuestionTemplateQuestionTemplateBase]
    FOREIGN KEY ([MultipleTextQuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MultipleTextQuestionTemplateQuestionTemplateBase'
CREATE INDEX [IX_FK_MultipleTextQuestionTemplateQuestionTemplateBase]
ON [dbo].[QuestionTemplateBases]
    ([MultipleTextQuestionTemplateId]);
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_SelectQuestionTemplateBase'
ALTER TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
ADD CONSTRAINT [FK_SelectQuestionTemplateBase_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MatrixDropdownBaseTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]
ADD CONSTRAINT [FK_MatrixDropdownBaseTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MatrixColumnTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixColumnTemplate]
ADD CONSTRAINT [FK_MatrixColumnTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MatrixQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixQuestionTemplate]
ADD CONSTRAINT [FK_MatrixQuestionTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'ValueTextPairs_MatrixRowTemplate'
ALTER TABLE [dbo].[ValueTextPairs_MatrixRowTemplate]
ADD CONSTRAINT [FK_MatrixRowTemplate_inherits_ValueTextPair]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ValueTextPairs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MatrixDropdownQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDropdownQuestionTemplate]
ADD CONSTRAINT [FK_MatrixDropdownQuestionTemplate_inherits_MatrixDropdownBaseTemplate]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MultipleTextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MultipleTextQuestionTemplate]
ADD CONSTRAINT [FK_MultipleTextQuestionTemplate_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_TextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate]
ADD CONSTRAINT [FK_TextQuestionTemplate_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_CommentQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate]
ADD CONSTRAINT [FK_CommentQuestionTemplate_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_CheckBoxQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_CheckBoxQuestionTemplate]
ADD CONSTRAINT [FK_CheckBoxQuestionTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_RadiogroupQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_RadiogroupQuestionTemplate]
ADD CONSTRAINT [FK_RadiogroupQuestionTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_DropdownQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_DropdownQuestionTemplate]
ADD CONSTRAINT [FK_DropdownQuestionTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_RatingQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_RatingQuestionTemplate]
ADD CONSTRAINT [FK_RatingQuestionTemplate_inherits_SelectQuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_HtmlQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_HtmlQuestionTemplate]
ADD CONSTRAINT [FK_HtmlQuestionTemplate_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_FileQuestionBase'
ALTER TABLE [dbo].[QuestionTemplateBases_FileQuestionBase]
ADD CONSTRAINT [FK_FileQuestionBase_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_MatrixDynamicQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_MatrixDynamicQuestionTemplate]
ADD CONSTRAINT [FK_MatrixDynamicQuestionTemplate_inherits_MatrixDropdownBaseTemplate]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases_MatrixDropdownBaseTemplate]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------