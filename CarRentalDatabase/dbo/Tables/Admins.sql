﻿CREATE TABLE [dbo].[Admins]
(
	[AdminId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(MAX) NULL, 
    [Password] NVARCHAR(MAX) NULL
)
