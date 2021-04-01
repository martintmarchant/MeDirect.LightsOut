CREATE TABLE [logs].[RequestResponse] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [RequestId]   UNIQUEIDENTIFIER NOT NULL,
    [DateTime]    DATETIME         CONSTRAINT [DF_RequestResponseLogs_DateTime] DEFAULT (getdate()) NOT NULL,
    [Flow]        SMALLINT         NOT NULL,
    [Host]        VARCHAR (50)     NULL,
    [Method]      NVARCHAR (255)   NOT NULL,
    [QueryString] NVARCHAR (2048)  NULL,
    [Body]        VARCHAR (MAX)    NULL,
    [TimeMs]      INT              CONSTRAINT [DF_RequestResponse_TimeMs] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RequestResponseLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

