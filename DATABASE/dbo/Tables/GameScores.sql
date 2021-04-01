CREATE TABLE [dbo].[GameScores] (
    [ScoreId]         INT            IDENTITY (1, 1) NOT NULL,
    [Columns]         INT            NOT NULL,
    [Rows]            INT            NOT NULL,
    [StartLightCount] INT            NOT NULL,
    [PlayerName]      NVARCHAR (250) NOT NULL,
    [NoOfMoves]       INT            NULL,
    [InitialLayout]   VARCHAR (MAX)  NULL,
    [GameMoves]       VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_GameScores] PRIMARY KEY CLUSTERED ([ScoreId] ASC)
);

