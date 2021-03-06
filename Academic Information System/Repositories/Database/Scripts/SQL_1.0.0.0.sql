DECLARE @transaction varchar(50) = 'UpdateStructureTransaction';
DECLARE @errorCode int;
DECLARE @version varchar(30) = '1.0.0.0';

BEGIN TRANSACTION @transaction;

SET ANSI_NULLS ON ;
SET QUOTED_IDENTIFIER ON ;
SET ANSI_PADDING ON;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Versions]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[Versions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Version] [nvarchar](20) NOT NULL
) ON [PRIMARY];
END

IF EXISTS (SELECT * FROM [dbo].[Versions] WHERE [Version] = @version)
BEGIN
SELECT 0;
RETURN;
END

INSERT INTO [dbo].[Versions] ([UpdateDate], [Version]) VALUES (GETDATE(), @version);

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Exams]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Exams](
	[ID] [nvarchar](10) NOT NULL,
	[Time] [datetime] NOT NULL,
	[SubjectID] [nvarchar](10) NOT NULL,
	[TeacherID] [nvarchar](10) NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Students](
	[ID] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](150) NOT NULL,
	[Semester] [int] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[StudyProgrammeID] [nvarchar](10) NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudentsSignedToExam]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentsSignedToExam](
	[ExamID] [nvarchar](10) NOT NULL,
	[StudentID] [nvarchar](10) NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StudyProgrammes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudyProgrammes](
	[ID] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Length] [int] NOT NULL,
	[StudyType] [int] NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subjects]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Subjects](
	[ID] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Semester] [int] NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SubjectTeachers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SubjectTeachers](
	[TeacherID] [nvarchar](10) NOT NULL,
	[SubjectID] [nvarchar](10) NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teachers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Teachers](
	[ID] [nvarchar](10) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Lastname] [nvarchar](150) NOT NULL,
	[TitleSuffix] [nvarchar](50) NOT NULL
) ON [PRIMARY];
END

SET @errorCode = @@ERROR;
IF( @errorCode <> 0)  GOTO PROBLEM;
 
SET ANSI_PADDING OFF;
 
SET ANSI_PADDING ON;

 
/****** Object:  Index [ExamsIDKey]    Script Date: 25.2.2014 18:29:15 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Exams]') AND name = N'ExamsIDKey')
CREATE UNIQUE NONCLUSTERED INDEX [ExamsIDKey] ON [dbo].[Exams]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
 
SET ANSI_PADDING ON;

 
/****** Object:  Index [StudentsIDKey]    Script Date: 25.2.2014 18:29:15 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND name = N'StudentsIDKey')
CREATE UNIQUE NONCLUSTERED INDEX [StudentsIDKey] ON [dbo].[Students]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
 
SET ANSI_PADDING ON;

 
/****** Object:  Index [StudyProgrammesIDKey]    Script Date: 25.2.2014 18:29:15 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[StudyProgrammes]') AND name = N'StudyProgrammesIDKey')
CREATE UNIQUE NONCLUSTERED INDEX [StudyProgrammesIDKey] ON [dbo].[StudyProgrammes]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
 
SET ANSI_PADDING ON;

 
/****** Object:  Index [SubjectsIDKey]    Script Date: 25.2.2014 18:29:15 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Subjects]') AND name = N'SubjectsIDKey')
CREATE UNIQUE NONCLUSTERED INDEX [SubjectsIDKey] ON [dbo].[Subjects]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
 
SET ANSI_PADDING ON;

 
/****** Object:  Index [TeachersIDKey]    Script Date: 25.2.2014 18:29:15 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Teachers]') AND name = N'TeachersIDKey')
CREATE UNIQUE NONCLUSTERED INDEX [TeachersIDKey] ON [dbo].[Teachers]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
 
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Teachers_Title]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [DF_Teachers_Title]  DEFAULT ('') FOR [Title];
END

COMMIT TRANSACTION @transaction;
SELECT 0;

PROBLEM:
IF (@errorCode <> 0)
BEGIN
	ROLLBACK TRANSACTION @transaction;
	SELECT @errorCode;
END