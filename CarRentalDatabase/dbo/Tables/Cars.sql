CREATE TABLE [dbo].[Cars]
(
	[CarID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [PlateNumber] NVARCHAR(MAX) NULL, 
    [Year] INT NULL, 
    [Color] NVARCHAR(MAX) NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [Status] NVARCHAR(MAX) NULL
)
