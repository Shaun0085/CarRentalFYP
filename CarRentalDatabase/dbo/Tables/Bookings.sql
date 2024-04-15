CREATE TABLE [dbo].[Bookings]
(
	[BookingID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerName] NVARCHAR(MAX) NULL, 
    [ICNumber] NVARCHAR(MAX) NULL, 
    [CustomerPhoneNumber] NVARCHAR(MAX) NULL, 
    [PickUpLocation] NVARCHAR(MAX) NULL, 
    [PickUpDate] DATE NULL, 
    [PickUpTime] TIME NULL, 
    [DropOffLocation] NVARCHAR(MAX) NULL, 
    [DropOffDate] DATE NULL, 
    [DropOffTime] TIME NULL, 
    [BabySeat] NVARCHAR(MAX) NULL, 
    [UsbCable] NVARCHAR(MAX) NULL, 
    [PhoneHolder] NVARCHAR(MAX) NULL, 
    [VehicleID] INT NULL, 
    [PaymentID] INT NULL, 
    [BookingStatusID] INT NULL, 
    [LocationID] INT NULL, 
    [CarID] INT NULL, 
    CONSTRAINT [FK_Bookings_ToVehicles] FOREIGN KEY ([VehicleID]) REFERENCES [Vehicles]([VehicleID]), 
    CONSTRAINT [FK_Bookings_ToPayments] FOREIGN KEY ([PaymentID]) REFERENCES [Payments]([PaymentID]), 
    CONSTRAINT [FK_Bookings_ToBookingStatuses] FOREIGN KEY ([BookingStatusID]) REFERENCES [BookingStatuses]([BookingStatusID]), 
    CONSTRAINT [FK_Bookings_ToLocations] FOREIGN KEY ([LocationID]) REFERENCES [Locations]([LocationID]), 
    CONSTRAINT [FK_Bookings_ToCars] FOREIGN KEY ([CarID]) REFERENCES [Cars]([CarID]) 
)

GO

CREATE INDEX [IX_Bookings_VehicleID] ON [dbo].[Bookings] ([VehicleID])

GO

CREATE INDEX [IX_Bookings_BookingStatusID] ON [dbo].[Bookings] ([BookingStatusID])

GO

CREATE INDEX [IX_Bookings_PaymentID] ON [dbo].[Bookings] ([PaymentID])

GO

CREATE INDEX [IX_Bookings_LocationID] ON [dbo].[Bookings] ([LocationID])

GO

CREATE INDEX [IX_Bookings_CarID] ON [dbo].[Bookings] ([CarID])
