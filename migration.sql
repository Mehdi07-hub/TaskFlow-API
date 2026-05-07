CREATE DATABASE TaskFlowDb;
GO

USE TaskFlowDb;
GO

CREATE TABLE Users (
    Id           INT           IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(MAX) NOT NULL,
    Email        NVARCHAR(450) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    Role         NVARCHAR(MAX) NOT NULL DEFAULT 'User',
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);
GO

CREATE TABLE Projects (
    Id           INT           IDENTITY(1,1) PRIMARY KEY,
    Name         NVARCHAR(MAX) NOT NULL,
    Description  NVARCHAR(MAX) NULL,
    CreationDate DATETIME2     NOT NULL,
    UserId       INT           NOT NULL,
    CONSTRAINT FK_Projects_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
GO

CREATE TABLE Tasks (
    Id        INT           IDENTITY(1,1) PRIMARY KEY,
    Title     NVARCHAR(MAX) NOT NULL,
    Status    NVARCHAR(MAX) NOT NULL DEFAULT 'Todo',
    DueDate   DATETIME2     NULL,
    ProjectId INT           NOT NULL,
    Comments  NVARCHAR(MAX) NOT NULL DEFAULT '',
    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectId) REFERENCES Projects(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_Projects_UserId ON Projects(UserId);
CREATE INDEX IX_Tasks_ProjectId ON Tasks(ProjectId);
CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
GO