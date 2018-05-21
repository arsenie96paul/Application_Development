CREATE TABLE [dbo].[CalendarModels] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Email]       NVARCHAR (MAX) NULL,
    [Title]       NVARCHAR (MAX) NULL,
    [Date]        DATETIME       NULL,
    [Location]    NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.CalendarModels] PRIMARY KEY CLUSTERED ([Id] ASC)
);

