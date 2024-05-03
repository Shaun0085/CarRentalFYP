CREATE TABLE [dbo].[Bookings] (
    [BookingID]           INT            IDENTITY (1, 1) NOT NULL,
    [CustomerName]        NVARCHAR (MAX) NULL,
    [ICNumber]            NVARCHAR (MAX) NULL,
    [CustomerPhoneNumber] NVARCHAR (MAX) NULL,
    [PickUpLocation]      NVARCHAR (MAX) NULL,
    [PickUpDate]          DATE           NULL,
    [PickUpTime]          TIME (7)       NULL,
    [DropOffLocation]     NVARCHAR (MAX) NULL,
    [DropOffDate]         DATE           NULL,
    [DropOffTime]         TIME (7)       NULL,
    [BabySeat]            NVARCHAR (MAX) NULL,
    [UsbCable]            NVARCHAR (MAX) NULL,
    [PhoneHolder]         NVARCHAR (MAX) NULL,
    [VehicleID]           INT            NULL,
    [PaymentID]           INT            NULL,
    [BookingStatusID]     INT            NULL,
    [LocationID]          INT            NULL,
    [CarID]               INT            NULL,
    [Amount]              INT            NULL,
    [Email]               VARCHAR (MAX)  NULL,
    [PaymentType]         VARCHAR (MAX)  NULL,
    [OrderID]             VARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([BookingID] ASC),
    FOREIGN KEY ([BookingStatusID]) REFERENCES [dbo].[BookingStatuses] ([BookingStatusID]),
    FOREIGN KEY ([CarID]) REFERENCES [dbo].[Cars] ([CarID]),
    FOREIGN KEY ([LocationID]) REFERENCES [dbo].[Locations] ([LocationID]),
    FOREIGN KEY ([PaymentID]) REFERENCES [dbo].[Payments] ([PaymentID]),
    FOREIGN KEY ([VehicleID]) REFERENCES [dbo].[Vehicles] ([VehicleID])
);



GO



GO



GO



GO



GO


