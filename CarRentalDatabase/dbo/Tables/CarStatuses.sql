CREATE TABLE [dbo].[CarStatuses] (
    [CarStatusID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([CarStatusID] ASC)
);

