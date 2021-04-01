CREATE TABLE [dbo].[GamePlayModes] (
    [ConfigurationId] INT           IDENTITY (1, 1) NOT NULL,
    [GameName]        VARCHAR (150) NOT NULL,
    [Columns]         INT           NOT NULL,
    [Rows]            INT           NOT NULL,
    [StartLightCount] INT           NOT NULL,
    CONSTRAINT [PK_GameConfigurations] PRIMARY KEY CLUSTERED ([ConfigurationId] ASC)
);

