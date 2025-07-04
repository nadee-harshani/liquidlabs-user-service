/****** Create Database: [LiquidLabs] ******/
CREATE DATABASE [LiquidLabs];
GO

USE [LiquidLabs];
GO

/****** Create Table: [dbo].[Users] ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [UserName] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(255) NULL,
    [Phone] NVARCHAR(50) NULL,
    [Website] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

/****** Create Table: [dbo].[Addresses] ******/
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Addresses] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [Street] NVARCHAR(50) NOT NULL,
    [Suite] NVARCHAR(50) NULL,
    [City] NVARCHAR(100) NOT NULL,
    [ZipCode] NVARCHAR(50) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY];
GO

/****** Add Foreign Key Constraint ******/
ALTER TABLE [dbo].[Addresses] WITH CHECK 
ADD CONSTRAINT [FK_Addresses_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users]([Id]);
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Users];
GO