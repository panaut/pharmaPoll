
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2017 23:26:29
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Surveys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Surveys];
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
    [SurveyId] int  NOT NULL
);
GO

-- Creating table 'QuestionTemplateBases_TextQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate] (
    [Placeholder] nvarchar(512)  NULL,
    [Id] int  NOT NULL
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

-- Creating table 'QuestionTemplateBases_CommentQuestionTemplate'
CREATE TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate] (
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

-- Creating primary key on [Id] in table 'QuestionTemplateBases_TextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_TextQuestionTemplate]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_SelectQuestionTemplateBase'
ALTER TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
ADD CONSTRAINT [PK_QuestionTemplateBases_SelectQuestionTemplateBase]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionTemplateBases_CommentQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_CommentQuestionTemplate]
ADD CONSTRAINT [PK_QuestionTemplateBases_CommentQuestionTemplate]
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

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_TextQuestionTemplate'
ALTER TABLE [dbo].[QuestionTemplateBases_TextQuestionTemplate]
ADD CONSTRAINT [FK_TextQuestionTemplate_inherits_QuestionTemplateBase]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[QuestionTemplateBases]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'QuestionTemplateBases_SelectQuestionTemplateBase'
ALTER TABLE [dbo].[QuestionTemplateBases_SelectQuestionTemplateBase]
ADD CONSTRAINT [FK_SelectQuestionTemplateBase_inherits_QuestionTemplateBase]
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------