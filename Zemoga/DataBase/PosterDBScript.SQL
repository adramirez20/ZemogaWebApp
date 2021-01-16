/*
Created: 14/01/2021
Modified: 15/01/2021
*/
USE master

IF DB_ID('PosterDB') IS NOT NULL
    drop DATABASE PosterDB;
    
CREATE DATABASE PosterDB;
GO

Use PosterDB

GO
CREATE SCHEMA [Core]
GO

CREATE SCHEMA [App]
GO

CREATE SCHEMA [Admin]
GO


-- Create tables section -------------------------------------------------

-- Table Admin.Role

CREATE TABLE [Admin].[Role]
(
 [ID] Int IDENTITY(1,1) NOT NULL,
 [Name] Nvarchar(12) NOT NULL
)
go

-- Add keys for table Admin.Role

ALTER TABLE [Admin].[Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY ([ID])
go

-- Table Admin.User

CREATE TABLE [Admin].[User]
(
 [ID] Int IDENTITY(1,1) NOT NULL,
 [IDRole] Int NULL,
 [Name] Nvarchar(255) NOT NULL,
 [UserName] Nvarchar(128) NOT NULL,
 [Password] Nvarchar(128) NOT NULL
)
go

-- Create indexes for table Admin.User

CREATE INDEX [IX_User_Role] ON [Admin].[User] ([IDRole])
go

-- Add keys for table Admin.User

ALTER TABLE [Admin].[User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([ID])
go

-- Table Core.Poster

CREATE TABLE [Core].[Poster]
(
 [ID] Int IDENTITY(1,1) NOT NULL,
 [StatusID] Int NOT NULL,
 [CreatedByUserID] Int NOT NULL,
 [ModifiedByUserID] Int NULL,
 [Content] Text NOT NULL,
 [Active] Bit NOT NULL,
 [CreatedDate] Datetime2 NOT NULL,
 [ModifiedDate] Datetime2 NULL
)
go

-- Create indexes for table Core.Poster

CREATE INDEX [IX_CreatedUser_Poster] ON [Core].[Poster] ([CreatedByUserID])
go

CREATE INDEX [IX_ModifiedUser_Poster] ON [Core].[Poster] ([ModifiedByUserID])
go

CREATE INDEX [IX_Status_Poster] ON [Core].[Poster] ([StatusID])
go

-- Add keys for table Core.Poster

ALTER TABLE [Core].[Poster] ADD CONSTRAINT [PK_Poster] PRIMARY KEY ([ID])
go

-- Table Core.Comment

CREATE TABLE [Core].[Comment]
(
 [ID] Int IDENTITY(1,1) NOT NULL,
 [PosterID] Int NOT NULL,
 [Description] Text NOT NULL,
 [AddedDate] Datetime2 NOT NULL
)
go

-- Create indexes for table Core.Comment

CREATE INDEX [IX_Poster_Comments] ON [Core].[Comment] ([PosterID])
go

-- Add keys for table Core.Comment

ALTER TABLE [Core].[Comment] ADD CONSTRAINT [PK_Comment] PRIMARY KEY ([ID])
go

-- Table App.Status

CREATE TABLE [App].[Status]
(
 [ID] Int IDENTITY(1,1) NOT NULL,
 [Value] Nvarchar(15) NOT NULL
)
go

-- Add keys for table App.Status

ALTER TABLE [App].[Status] ADD CONSTRAINT [PK_Status] PRIMARY KEY ([ID])
go

-- Create foreign keys (relationships) section ------------------------------------------------- 


ALTER TABLE [Admin].[User] ADD CONSTRAINT [FK_User_Role] FOREIGN KEY ([IDRole]) REFERENCES [Admin].[Role] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Core].[Poster] ADD CONSTRAINT [FK_CreatedByUser] FOREIGN KEY ([CreatedByUserID]) REFERENCES [Admin].[User] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Core].[Poster] ADD CONSTRAINT [FK_ModifiedByUser] FOREIGN KEY ([ModifiedByUserID]) REFERENCES [Admin].[User] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Core].[Comment] ADD CONSTRAINT [FK_Poster_Comments] FOREIGN KEY ([PosterID]) REFERENCES [Core].[Poster] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go



ALTER TABLE [Core].[Poster] ADD CONSTRAINT [FK_Status_Poster] FOREIGN KEY ([StatusID]) REFERENCES [App].[Status] ([ID]) ON UPDATE NO ACTION ON DELETE NO ACTION
go





IF NOT EXISTS(SELECT * FROM [Admin].[Role]  WHERE NAME='Editor')
BEGIN
INSERT INTO [Admin].[Role]  (NAME) VALUES ('Editor')
END

IF NOT EXISTS(SELECT * FROM [Admin].[Role]  WHERE NAME='Writer')
BEGIN
INSERT INTO [Admin].[Role]  (NAME) VALUES ('Writer')
END

IF NOT EXISTS(SELECT * FROM [Admin].[Role]  WHERE NAME='Admin')
BEGIN
INSERT INTO [Admin].[Role]  (NAME) VALUES ('Admin')
END


IF NOT EXISTS(SELECT * FROM [Admin].[User]  WHERE UserName='UEditor')
BEGIN
INSERT INTO [Admin].[User] ([IDRole],[Name],[UserName],[Password])
VALUES
           (1,'Editor User','UEditor','1234*')
END

IF NOT EXISTS(SELECT * FROM [Admin].[User]  WHERE UserName='UWriter')
BEGIN
INSERT INTO [Admin].[User] ([IDRole],[Name],[UserName],[Password])
VALUES
           (2,'Writer User','UWriter','1234!')
END

IF NOT EXISTS(SELECT * FROM [Admin].[User]  WHERE UserName='AndreRS')
BEGIN
INSERT INTO [Admin].[User] ([IDRole],[Name],[UserName],[Password])
VALUES
           (3,'Andre Ramirez','AndreRS','852')
END

IF NOT EXISTS(SELECT * FROM [App].[Status]  WHERE VALUE='Pending')
BEGIN
INSERT INTO [App].[Status]  (VALUE) VALUES ('Pending')
END



IF NOT EXISTS(SELECT * FROM [App].[Status]  WHERE VALUE='Approved')
BEGIN
INSERT INTO [App].[Status]  (VALUE) VALUES ('Approved')
END



IF NOT EXISTS(SELECT * FROM [App].[Status]  WHERE VALUE='Rejected')
BEGIN
INSERT INTO [App].[Status]  (VALUE) VALUES ('Rejected')
END

USE [PosterDB]
GO

/****** Object:  StoredProcedure [app].[GetUserApplications]    Script Date: 14/01/2021 08:46:35 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Core].[UpdateState] (@PosterID INT=0, @StatusID INT=0)
AS BEGIN
    SET NOCOUNT ON;
	UPDATE [Core].[Poster]
	SET StatusID =  @StatusID
	WHERE ID =@PosterID
END;

GO


USE [PosterDB]
GO

/****** Object:  StoredProcedure [Core].[UpdateState]    Script Date: 15/01/2021 10:30:50 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [Admin].[Login] (@UserName varchar(250), @Password  varchar(250), @RoleValid  varchar(250) OUT)
AS BEGIN
 SET @RoleValid=(SELECT R.Name 
				 FROM [Admin].[User] AS U 
				 INNER JOIN [Admin].[Role] AS R ON R.ID = U.IDRole
				 WHERE U.UserName = @UserName AND U.Password = @Password)
END;


GO

SELECT 'All Ok :)'
