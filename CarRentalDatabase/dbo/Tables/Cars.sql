CREATE TABLE [dbo].[Cars] (
    [CarID]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [PlateNumber] NVARCHAR (MAX) NULL,
    [Year]        INT            NULL,
    [Color]       NVARCHAR (MAX) NULL,
    [Notes]       NVARCHAR (MAX) NULL,
    [CarStatusID] INT            NULL,
    [VehicleID]   INT            NULL,
    PRIMARY KEY CLUSTERED ([CarID] ASC),
    FOREIGN KEY ([CarStatusID]) REFERENCES [dbo].[CarStatuses] ([CarStatusID]),
    FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[Vehicles] ([VehicleID])
);


