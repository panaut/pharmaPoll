
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2017 22:32:34
-- Generated from EDMX file: E:\GitHub\Poll Questionnaire\PQ\PollQuestionnaire.UI.Web\Questionnaire.Data\QuestionnaireModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SurveyMaster];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_QuestionTemplateAnswerTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerTemplates] DROP CONSTRAINT [FK_QuestionTemplateAnswerTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_ComplexQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplates] DROP CONSTRAINT [FK_ComplexQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_ConditionalQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplates] DROP CONSTRAINT [FK_ConditionalQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_PollQuestionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplates] DROP CONSTRAINT [FK_PollQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_PollVotingSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotingSessions] DROP CONSTRAINT [FK_PollVotingSession];
GO
IF OBJECT_ID(N'[dbo].[FK_VotingSessionSessionResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionResults] DROP CONSTRAINT [FK_VotingSessionSessionResult];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionResultAnswerTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionResults] DROP CONSTRAINT [FK_SessionResultAnswerTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_SessionResultQuestionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SessionResults] DROP CONSTRAINT [FK_SessionResultQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionTemplateLocalization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Localizations] DROP CONSTRAINT [FK_QuestionTemplateLocalization];
GO
IF OBJECT_ID(N'[dbo].[FK_LocalizationAnswerTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Localizations] DROP CONSTRAINT [FK_LocalizationAnswerTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_PollSurveySection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SurveySections] DROP CONSTRAINT [FK_PollSurveySection];
GO
IF OBJECT_ID(N'[dbo].[FK_SurveySectionQuestionTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionTemplates] DROP CONSTRAINT [FK_SurveySectionQuestionTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_PredefinedAnswer_inherits_AnswerTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerTemplates_PredefinedAnswer] DROP CONSTRAINT [FK_PredefinedAnswer_inherits_AnswerTemplate];
GO
IF OBJECT_ID(N'[dbo].[FK_WriteInAnswer_inherits_AnswerTemplate]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerTemplates_WriteInAnswer] DROP CONSTRAINT [FK_WriteInAnswer_inherits_AnswerTemplate];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[QuestionTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionTemplates];
GO
IF OBJECT_ID(N'[dbo].[AnswerTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerTemplates];
GO
IF OBJECT_ID(N'[dbo].[Polls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Polls];
GO
IF OBJECT_ID(N'[dbo].[VotingSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VotingSessions];
GO
IF OBJECT_ID(N'[dbo].[SessionResults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SessionResults];
GO
IF OBJECT_ID(N'[dbo].[Localizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Localizations];
GO
IF OBJECT_ID(N'[dbo].[SurveySections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SurveySections];
GO
IF OBJECT_ID(N'[dbo].[AnswerTemplates_PredefinedAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerTemplates_PredefinedAnswer];
GO
IF OBJECT_ID(N'[dbo].[AnswerTemplates_WriteInAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerTemplates_WriteInAnswer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'QuestionTemplates'
CREATE TABLE [dbo].[QuestionTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DisplayOrder] nvarchar(max)  NOT NULL,
    [NativeText] nvarchar(max)  NOT NULL,
    [Type] int  NOT NULL,
    [QuestionTemplateId] int  NOT NULL,
    [Appearance] int  NOT NULL,
    [AnswerTemplateId] int  NULL,
    [PollId] int  NOT NULL,
    [SurveySectionId] int  NOT NULL
);
GO

-- Creating table 'AnswerTemplates'
CREATE TABLE [dbo].[AnswerTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [DisplayOrder] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [QuestionTemplateId] int  NOT NULL
);
GO

-- Creating table 'Polls'
CREATE TABLE [dbo].[Polls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Start] datetime  NOT NULL,
    [Duration] datetimeoffset  NOT NULL,
    [Header] nvarchar(max)  NOT NULL,
    [Footer] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VotingSessions'
CREATE TABLE [dbo].[VotingSessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nchar(6)  NOT NULL,
    [State] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL,
    [PollId] int  NOT NULL
);
GO

-- Creating table 'SessionResults'
CREATE TABLE [dbo].[SessionResults] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VotingSessionId] int  NOT NULL,
    [AnswerTemplateId] int  NOT NULL,
    [QuestionTemplateId] int  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Localizations'
CREATE TABLE [dbo].[Localizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Culture] nvarchar(10)  NOT NULL,
    [LocalizedText] nvarchar(max)  NOT NULL,
    [QuestionTemplateId] int  NULL,
    [AnswerTemplateId] int  NULL
);
GO

-- Creating table 'SurveySections'
CREATE TABLE [dbo].[SurveySections] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DisplayOrder] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [PollId] int  NOT NULL
);
GO

-- Creating table 'AnswerTemplates_PredefinedAnswer'
CREATE TABLE [dbo].[AnswerTemplates_PredefinedAnswer] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'AnswerTemplates_WriteInAnswer'
CREATE TABLE [dbo].[AnswerTemplates_WriteInAnswer] (
    [RegEx] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'QuestionTemplates'
ALTER TABLE [dbo].[QuestionTemplates]
ADD CONSTRAINT [PK_QuestionTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnswerTemplates'
ALTER TABLE [dbo].[AnswerTemplates]
ADD CONSTRAINT [PK_AnswerTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Polls'
ALTER TABLE [dbo].[Polls]
ADD CONSTRAINT [PK_Polls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VotingSessions'
ALTER TABLE [dbo].[VotingSessions]
ADD CONSTRAINT [PK_VotingSessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SessionResults'
ALTER TABLE [dbo].[SessionResults]
ADD CONSTRAINT [PK_SessionResults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Localizations'
ALTER TABLE [dbo].[Localizations]
ADD CONSTRAINT [PK_Localizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SurveySections'
ALTER TABLE [dbo].[SurveySections]
ADD CONSTRAINT [PK_SurveySections]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnswerTemplates_PredefinedAnswer'
ALTER TABLE [dbo].[AnswerTemplates_PredefinedAnswer]
ADD CONSTRAINT [PK_AnswerTemplates_PredefinedAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AnswerTemplates_WriteInAnswer'
ALTER TABLE [dbo].[AnswerTemplates_WriteInAnswer]
ADD CONSTRAINT [PK_AnswerTemplates_WriteInAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [QuestionTemplateId] in table 'AnswerTemplates'
ALTER TABLE [dbo].[AnswerTemplates]
ADD CONSTRAINT [FK_QuestionTemplateAnswerTemplate]
    FOREIGN KEY ([QuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionTemplateAnswerTemplate'
CREATE INDEX [IX_FK_QuestionTemplateAnswerTemplate]
ON [dbo].[AnswerTemplates]
    ([QuestionTemplateId]);
GO

-- Creating foreign key on [QuestionTemplateId] in table 'QuestionTemplates'
ALTER TABLE [dbo].[QuestionTemplates]
ADD CONSTRAINT [FK_ComplexQuestion]
    FOREIGN KEY ([QuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ComplexQuestion'
CREATE INDEX [IX_FK_ComplexQuestion]
ON [dbo].[QuestionTemplates]
    ([QuestionTemplateId]);
GO

-- Creating foreign key on [AnswerTemplateId] in table 'QuestionTemplates'
ALTER TABLE [dbo].[QuestionTemplates]
ADD CONSTRAINT [FK_ConditionalQuestion]
    FOREIGN KEY ([AnswerTemplateId])
    REFERENCES [dbo].[AnswerTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConditionalQuestion'
CREATE INDEX [IX_FK_ConditionalQuestion]
ON [dbo].[QuestionTemplates]
    ([AnswerTemplateId]);
GO

-- Creating foreign key on [PollId] in table 'QuestionTemplates'
ALTER TABLE [dbo].[QuestionTemplates]
ADD CONSTRAINT [FK_PollQuestionTemplate]
    FOREIGN KEY ([PollId])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PollQuestionTemplate'
CREATE INDEX [IX_FK_PollQuestionTemplate]
ON [dbo].[QuestionTemplates]
    ([PollId]);
GO

-- Creating foreign key on [PollId] in table 'VotingSessions'
ALTER TABLE [dbo].[VotingSessions]
ADD CONSTRAINT [FK_PollVotingSession]
    FOREIGN KEY ([PollId])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PollVotingSession'
CREATE INDEX [IX_FK_PollVotingSession]
ON [dbo].[VotingSessions]
    ([PollId]);
GO

-- Creating foreign key on [VotingSessionId] in table 'SessionResults'
ALTER TABLE [dbo].[SessionResults]
ADD CONSTRAINT [FK_VotingSessionSessionResult]
    FOREIGN KEY ([VotingSessionId])
    REFERENCES [dbo].[VotingSessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VotingSessionSessionResult'
CREATE INDEX [IX_FK_VotingSessionSessionResult]
ON [dbo].[SessionResults]
    ([VotingSessionId]);
GO

-- Creating foreign key on [AnswerTemplateId] in table 'SessionResults'
ALTER TABLE [dbo].[SessionResults]
ADD CONSTRAINT [FK_SessionResultAnswerTemplate]
    FOREIGN KEY ([AnswerTemplateId])
    REFERENCES [dbo].[AnswerTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionResultAnswerTemplate'
CREATE INDEX [IX_FK_SessionResultAnswerTemplate]
ON [dbo].[SessionResults]
    ([AnswerTemplateId]);
GO

-- Creating foreign key on [QuestionTemplateId] in table 'SessionResults'
ALTER TABLE [dbo].[SessionResults]
ADD CONSTRAINT [FK_SessionResultQuestionTemplate]
    FOREIGN KEY ([QuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionResultQuestionTemplate'
CREATE INDEX [IX_FK_SessionResultQuestionTemplate]
ON [dbo].[SessionResults]
    ([QuestionTemplateId]);
GO

-- Creating foreign key on [QuestionTemplateId] in table 'Localizations'
ALTER TABLE [dbo].[Localizations]
ADD CONSTRAINT [FK_QuestionTemplateLocalization]
    FOREIGN KEY ([QuestionTemplateId])
    REFERENCES [dbo].[QuestionTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionTemplateLocalization'
CREATE INDEX [IX_FK_QuestionTemplateLocalization]
ON [dbo].[Localizations]
    ([QuestionTemplateId]);
GO

-- Creating foreign key on [AnswerTemplateId] in table 'Localizations'
ALTER TABLE [dbo].[Localizations]
ADD CONSTRAINT [FK_LocalizationAnswerTemplate]
    FOREIGN KEY ([AnswerTemplateId])
    REFERENCES [dbo].[AnswerTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocalizationAnswerTemplate'
CREATE INDEX [IX_FK_LocalizationAnswerTemplate]
ON [dbo].[Localizations]
    ([AnswerTemplateId]);
GO

-- Creating foreign key on [PollId] in table 'SurveySections'
ALTER TABLE [dbo].[SurveySections]
ADD CONSTRAINT [FK_PollSurveySection]
    FOREIGN KEY ([PollId])
    REFERENCES [dbo].[Polls]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PollSurveySection'
CREATE INDEX [IX_FK_PollSurveySection]
ON [dbo].[SurveySections]
    ([PollId]);
GO

-- Creating foreign key on [SurveySectionId] in table 'QuestionTemplates'
ALTER TABLE [dbo].[QuestionTemplates]
ADD CONSTRAINT [FK_SurveySectionQuestionTemplate]
    FOREIGN KEY ([SurveySectionId])
    REFERENCES [dbo].[SurveySections]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SurveySectionQuestionTemplate'
CREATE INDEX [IX_FK_SurveySectionQuestionTemplate]
ON [dbo].[QuestionTemplates]
    ([SurveySectionId]);
GO

-- Creating foreign key on [Id] in table 'AnswerTemplates_PredefinedAnswer'
ALTER TABLE [dbo].[AnswerTemplates_PredefinedAnswer]
ADD CONSTRAINT [FK_PredefinedAnswer_inherits_AnswerTemplate]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[AnswerTemplates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'AnswerTemplates_WriteInAnswer'
ALTER TABLE [dbo].[AnswerTemplates_WriteInAnswer]
ADD CONSTRAINT [FK_WriteInAnswer_inherits_AnswerTemplate]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[AnswerTemplates]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------