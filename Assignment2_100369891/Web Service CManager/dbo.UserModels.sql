CREATE TABLE [dbo].[UserModels] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Email]    NVARCHAR (MAX) NULL,
    [Password] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.UserModels] PRIMARY KEY CLUSTERED ([Id] ASC)
);

