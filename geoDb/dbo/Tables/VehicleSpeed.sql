CREATE TABLE [dbo].[VehicleSpeed](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
    [DeviceID] varchar(10)  NULL,
	[VehicleID] [int] NOT NULL,
	[Lat] [float] NULL,
	[Lon] [float] NULL,
    [Speed] [float]  NULL,
    [EventTime] binary(8)  NULL,
    [UTCTime] datetime  NULL,
    [SpeedLimit] float  NULL,
    [Distance] float  NULL,
    [Route] nvarchar(max)  NULL
    CONSTRAINT [PK_VehicleSpeed] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
GO
ALTER TABLE [dbo].[VehicleSpeed]  WITH CHECK ADD  CONSTRAINT [FK_VehicleSpeed_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[VehicleSpeed] CHECK CONSTRAINT [FK_VehicleSpeed_User]
GO
ALTER TABLE [dbo].[VehicleSpeed]  WITH CHECK ADD  CONSTRAINT [FK_VehicleSpeed_UserVehicle] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[UserVehicle] ([ID])
GO

ALTER TABLE [dbo].[VehicleSpeed] CHECK CONSTRAINT [FK_VehicleSpeed_UserVehicle]