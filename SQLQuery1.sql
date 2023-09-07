CREATE TABLE [dbo].[AirplaneModel] (
    [AirplaneId]  INT  IDENTITY (1, 1) NOT NULL,
    [CreatingTime] DATETIME  NULL,
    [StationId] INT          NULL,
    PRIMARY KEY CLUSTERED ([AirplaneId] ASC)
);

CREATE TABLE [dbo].[AirportImage] (
    [ImageId]  INT  IDENTITY (1, 1) NOT NULL,
    [ImageTime] DATETIME  NULL,
    [DeparturePriority] BIT   NULL,
    PRIMARY KEY CLUSTERED ([ImageId] ASC)
);

CREATE TABLE [dbo].[FlightModel] (
    [FlightId]  INT  IDENTITY (1, 1) NOT NULL,
    [IsLanding] BIT   NULL,
    [AirplaneId] INT          NULL,
    [TimeProcessDone] DATETIME  NULL,
    PRIMARY KEY CLUSTERED ([FlightId] ASC)
);

CREATE TABLE [dbo].[ProcessStatusModel] (
    [Index]  INT  IDENTITY (1, 1) NOT NULL,
    [FlightNum] INT          NULL,
    [AirplaneId] INT          NULL,
    [IsLanding] BIT   NULL,
    [StationNum] INT          NULL,
    [IsTaskDone] BIT   NULL,
    [CreationTime] DATETIME  NULL,
    [Enters] VARCHAR (500) NULL,
    
    PRIMARY KEY CLUSTERED ([Index] ASC)
);

CREATE TABLE [dbo].[StationModel] (
    [StationId]  INT  IDENTITY (1, 1) NOT NULL,
    [Type]   varchar(10) NOT NULL CHECK ([Type] IN('LandingRequest', 'LandingPreparation', 'Approach' , 'Runway', 'Transportaion' , 'Load')),
    [IsFree] BIT   NULL,
   
    PRIMARY KEY CLUSTERED ([StationId] ASC)
);