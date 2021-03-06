CREATE TABLE [Versions] (
  [ID] int IDENTITY (1,1) NOT NULL
, [UpdateDate] datetime NOT NULL
, [Version] nvarchar(20) NOT NULL
);

CREATE TABLE [Teachers] (
  [ID] nvarchar(10) NOT NULL
, [Title] nvarchar(50) NOT NULL
, [Name] nvarchar(100) NOT NULL
, [Lastname] nvarchar(150) NOT NULL
, [TitleSuffix] nvarchar(50) NOT NULL
);

CREATE TABLE [SubjectTeachers] (
  [TeacherID] nvarchar(10) NOT NULL
, [SubjectID] nvarchar(10) NOT NULL
);

CREATE TABLE [Subjects] (
  [ID] nvarchar(10) NOT NULL
, [Name] nvarchar(100) NOT NULL
, [Semester] int NOT NULL
);

CREATE TABLE [StudyProgrammes] (
  [ID] nvarchar(10) NOT NULL
, [Name] nvarchar(100) NOT NULL
, [Length] int NOT NULL
, [StudyType] int NOT NULL
);

CREATE TABLE [StudentsSignedToExam] (
  [ExamID] nvarchar(10) NOT NULL
, [StudentID] nvarchar(10) NOT NULL
);

CREATE TABLE [Students] (
  [ID] nvarchar(10) NOT NULL
, [Name] nvarchar(100) NOT NULL
, [Lastname] nvarchar(150) NOT NULL
, [Semester] int NOT NULL
, [DateOfBirth] datetime NOT NULL
, [StudyProgrammeID] nvarchar(10) NOT NULL
);

CREATE TABLE [Exams] (
  [ID] nvarchar(10) NOT NULL
, [Time] datetime NOT NULL
, [SubjectID] nvarchar(10) NOT NULL
, [TeacherID] nvarchar(10) NOT NULL
);


