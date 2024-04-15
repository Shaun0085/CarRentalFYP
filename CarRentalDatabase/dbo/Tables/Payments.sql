CREATE TABLE [dbo].[Payments]
(
	[PaymentID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Amount] INT NULL, 
    [Date] DATE NULL, 
    [CallBackStatus] NVARCHAR(MAX) NULL
)
