CREATE TABLE [dbo].[Vehicles]
(
	[VehicleID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [Image] VARBINARY(MAX) NULL, 
    [Price] INT NULL, 
    [NumberOfSeat] INT NULL, 
    [PlateNumber] NVARCHAR(MAX) NULL, 
    [Deposit] INT NULL, 
    [EngineCapacity] INT NULL, 
    [Year] INT NULL, 
    [VehicleCategoryID] INT NULL, 
    [VehicleTransmissionID] INT NULL, 
    [VehicleFuelID] INT NULL, 
    CONSTRAINT [FK_Vehicles_ToVehicleCategories] FOREIGN KEY ([VehicleCategoryID]) REFERENCES [VehicleCategories]([VehicleCategoryID]), 
    CONSTRAINT [FK_Vehicles_ToVehicleTransmissions] FOREIGN KEY ([VehicleTransmissionID]) REFERENCES [VehicleTransmissions]([VehicleTransmissionID]), 
    CONSTRAINT [FK_Vehicles_ToVehicleFuels] FOREIGN KEY ([VehicleFuelID]) REFERENCES [VehicleFuels]([VehicleFuelID]) 
)

GO

CREATE INDEX [IX_Vehicles_VehicleCategoryID] ON [dbo].[Vehicles] ([VehicleCategoryID])

GO

CREATE INDEX [IX_Vehicles_VehicleTransmissionID] ON [dbo].[Vehicles] ([VehicleTransmissionID])
