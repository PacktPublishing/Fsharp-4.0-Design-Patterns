IF OBJECT_ID('[dbo].[LaserShip]','U') IS NOT NULL
   DROP TABLE [dbo].[LaserShip]
GO
CREATE TABLE [dbo].[LaserShip](
   [invno] nvarchar(13) NOT NULL,
   [JobNumber] nvarchar(14) NOT NULL,
   [TDate] nvarchar(18) NOT NULL,
   [Reference] nvarchar(49) NULL,
   [LSTrackingNumber] nvarchar(20) NULL,
   [Caller] nvarchar(36) NULL,
   [FromName] nvarchar(19) NULL,
   [FromNumber] nvarchar(10) NULL,
   [FromStreet] nvarchar(20) NULL,
   [FromRoom] nvarchar(17) NULL,
   [FromCity] nvarchar(10) NULL,
   [FromZip] nvarchar(10) NULL,
   [ToName] nvarchar(50) NOT NULL,
   [ToNumber] nvarchar(13) NULL,
   [ToStreet] nvarchar(50) NULL,
   [ToRoom] nvarchar(50) NULL,
   [ToCity] nvarchar(23) NOT NULL,
   [ToZip] nvarchar(5) NOT NULL,
   [ServiceCode] nvarchar(11) NOT NULL,
   [ServiceAmount] decimal(18,2) NULL,
   [ExtraCode1] nvarchar(10) NULL,
   [Extra1Amount] decimal(18,2) NULL,
   [ExtraCode2] nvarchar(10) NULL,
   [Extra2Amount] decimal(18,2) NULL,
   [ExtraCode3] nvarchar(10) NULL,
   [Extra3Amount] decimal(18,2) NULL,
   [ExtraCode4] nvarchar(10) NULL,
   [Extra4Amount] decimal(18,2) NULL,
   [EN] decimal(18,2) NULL,
   [Tax] decimal(18,2) NULL,
   [Total] decimal(18,2) NULL,
   [Zone] nvarchar(4) NULL,
   [Weight] decimal(18,2) NULL,
   [POD] nvarchar(50) NULL,
   [PODDate] nvarchar(50) NOT NULL,
   [PODTime] nvarchar(8) NOT NULL,
   [PickupDate] nvarchar(50) NULL,
   [SourceId] int NOT NULL,
   [RowKey] BigInt IDENTITY(1,1) NOT NULL
CONSTRAINT [PK_LaserShip] PRIMARY KEY ([RowKey])
)
