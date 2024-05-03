CREATE TABLE [dbo].[Payments] (
    [PaymentID]       INT            IDENTITY (1, 1) NOT NULL,
    [Date]            DATE           NULL,
    [CallBackStatus]  NVARCHAR (MAX) NULL,
    [ReferenceNumber] VARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([PaymentID] ASC)
);


