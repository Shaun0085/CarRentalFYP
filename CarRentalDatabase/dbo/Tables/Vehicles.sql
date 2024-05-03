CREATE TABLE [dbo].[Vehicles] (
    [VehicleID]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (MAX)  NULL,
    [Image]                 VARBINARY (MAX) NULL,
    [Price]                 INT             NULL,
    [NumberOfSeat]          INT             NULL,
    [Deposit]               INT             NULL,
    [EngineCapacity]        INT             NULL,
    [Year]                  INT             NULL,
    [VehicleCategoryID]     INT             NULL,
    [VehicleTransmissionID] INT             NULL,
    [VehicleFuelID]         INT             NULL,
    PRIMARY KEY CLUSTERED ([VehicleID] ASC),
    CONSTRAINT [FK_Vehicles_ToVehicleCategories] FOREIGN KEY ([VehicleCategoryID]) REFERENCES [dbo].[VehicleCategories] ([VehicleCategoryID]),
    CONSTRAINT [FK_Vehicles_ToVehicleFuels] FOREIGN KEY ([VehicleFuelID]) REFERENCES [dbo].[VehicleFuels] ([VehicleFuelID]),
    CONSTRAINT [FK_Vehicles_ToVehicleTransmissions] FOREIGN KEY ([VehicleTransmissionID]) REFERENCES [dbo].[VehicleTransmissions] ([VehicleTransmissionID])
);



GO

CREATE INDEX [IX_Vehicles_VehicleCategoryID] ON [dbo].[Vehicles] ([VehicleCategoryID])

GO

CREATE INDEX [IX_Vehicles_VehicleTransmissionID] ON [dbo].[Vehicles] ([VehicleTransmissionID])
