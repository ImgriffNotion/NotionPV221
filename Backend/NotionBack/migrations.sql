IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Files] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Url] nvarchar(max) NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY ([Id])
);

CREATE TABLE [PageTypes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [TypeCode] int NOT NULL,
    CONSTRAINT [PK_PageTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Lastname] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Avatar] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

CREATE TABLE [Pages] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Banner] nvarchar(max) NULL,
    [Icon] nvarchar(max) NULL,
    [DeleteDt] datetime2 NULL,
    [Slug] nvarchar(max) NULL,
    [TypeId] uniqueidentifier NULL,
    [OwnerId] uniqueidentifier NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pages_PageTypes_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [PageTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Pages_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [Boards] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [DeleteDt] datetime2 NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Boards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Boards_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Calendars] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Calendars] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Calendars_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Galleries] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [DeleteDt] datetime2 NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Galleries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Galleries_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [JustPageContents] (
    [Id] uniqueidentifier NOT NULL,
    [Text] nvarchar(max) NULL,
    [Index] int NOT NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_JustPageContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JustPageContents_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Tables] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Rows] int NOT NULL,
    [Columns] int NOT NULL,
    [DeleteDt] datetime2 NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Tables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tables_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Lists] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [DeleteDt] datetime2 NULL,
    [ParentPageId] uniqueidentifier NOT NULL,
    [BoardId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Lists] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lists_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Boards] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Lists_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [CalendarContents] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [PlanedDate] datetime2 NULL,
    [Untitled] int NOT NULL,
    [Color] nvarchar(max) NULL,
    [CalendarId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CalendarContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CalendarContents_Calendars_CalendarId] FOREIGN KEY ([CalendarId]) REFERENCES [Calendars] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [GalleryContents] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Url] nvarchar(max) NULL,
    [GalleryId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_GalleryContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GalleryContents_Galleries_GalleryId] FOREIGN KEY ([GalleryId]) REFERENCES [Galleries] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [TableContents] (
    [Id] uniqueidentifier NOT NULL,
    [Row] int NOT NULL,
    [Column] int NOT NULL,
    [Data] nvarchar(max) NULL,
    [Foreground] nvarchar(max) NULL,
    [Background] nvarchar(max) NULL,
    [TableId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TableContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TableContents_Tables_TableId] FOREIGN KEY ([TableId]) REFERENCES [Tables] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ListContents] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Text] nvarchar(max) NULL,
    [Number] nvarchar(max) NULL,
    [Date] datetime2 NULL,
    [Status] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Label] nvarchar(max) NULL,
    [Index] int NOT NULL,
    [ListId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ListContents] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ListContents_Lists_ListId] FOREIGN KEY ([ListId]) REFERENCES [Lists] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [CalendarFile] (
    [FileId] uniqueidentifier NOT NULL,
    [CalendarContentId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_CalendarFile] PRIMARY KEY ([FileId], [CalendarContentId]),
    CONSTRAINT [FK_CalendarFile_CalendarContents_CalendarContentId] FOREIGN KEY ([CalendarContentId]) REFERENCES [CalendarContents] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CalendarFile_Files_FileId] FOREIGN KEY ([FileId]) REFERENCES [Files] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [ListFile] (
    [FileId] uniqueidentifier NOT NULL,
    [ListContentId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ListFile] PRIMARY KEY ([FileId], [ListContentId]),
    CONSTRAINT [FK_ListFile_Files_FileId] FOREIGN KEY ([FileId]) REFERENCES [Files] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ListFile_ListContents_ListContentId] FOREIGN KEY ([ListContentId]) REFERENCES [ListContents] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_Boards_ParentPageId] ON [Boards] ([ParentPageId]);

CREATE INDEX [IX_CalendarContents_CalendarId] ON [CalendarContents] ([CalendarId]);

CREATE INDEX [IX_CalendarFile_CalendarContentId] ON [CalendarFile] ([CalendarContentId]);

CREATE INDEX [IX_Calendars_ParentPageId] ON [Calendars] ([ParentPageId]);

CREATE INDEX [IX_Galleries_ParentPageId] ON [Galleries] ([ParentPageId]);

CREATE INDEX [IX_GalleryContents_GalleryId] ON [GalleryContents] ([GalleryId]);

CREATE INDEX [IX_JustPageContents_ParentPageId] ON [JustPageContents] ([ParentPageId]);

CREATE INDEX [IX_ListContents_ListId] ON [ListContents] ([ListId]);

CREATE INDEX [IX_ListFile_ListContentId] ON [ListFile] ([ListContentId]);

CREATE INDEX [IX_Lists_BoardId] ON [Lists] ([BoardId]);

CREATE INDEX [IX_Lists_ParentPageId] ON [Lists] ([ParentPageId]);

CREATE INDEX [IX_Pages_OwnerId] ON [Pages] ([OwnerId]);

CREATE INDEX [IX_Pages_TypeId] ON [Pages] ([TypeId]);

CREATE INDEX [IX_TableContents_TableId] ON [TableContents] ([TableId]);

CREATE INDEX [IX_Tables_ParentPageId] ON [Tables] ([ParentPageId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250320202651_InitialCreate', N'9.0.2');

ALTER TABLE [Lists] DROP CONSTRAINT [FK_Lists_Boards_BoardId];

ALTER TABLE [Lists] DROP CONSTRAINT [FK_Lists_Pages_ParentPageId];

ALTER TABLE [Lists] ADD CONSTRAINT [FK_Lists_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Boards] ([Id]) ON DELETE SET NULL;

ALTER TABLE [Lists] ADD CONSTRAINT [FK_Lists_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250320203331_SecTry', N'9.0.2');

ALTER TABLE [Lists] DROP CONSTRAINT [FK_Lists_Boards_BoardId];

ALTER TABLE [Lists] DROP CONSTRAINT [FK_Lists_Pages_ParentPageId];

ALTER TABLE [Pages] DROP CONSTRAINT [FK_Pages_Users_OwnerId];

ALTER TABLE [Lists] ADD CONSTRAINT [FK_Lists_Boards_BoardId] FOREIGN KEY ([BoardId]) REFERENCES [Boards] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Lists] ADD CONSTRAINT [FK_Lists_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE CASCADE;

ALTER TABLE [Pages] ADD CONSTRAINT [FK_Pages_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250320204212_TrdTry', N'9.0.2');

ALTER TABLE [Lists] DROP CONSTRAINT [FK_Lists_Pages_ParentPageId];

ALTER TABLE [Pages] DROP CONSTRAINT [FK_Pages_Users_OwnerId];

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tables]') AND [c].[name] = N'ParentPageId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Tables] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Tables] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TableContents]') AND [c].[name] = N'TableId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TableContents] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [TableContents] ALTER COLUMN [TableId] uniqueidentifier NULL;

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lists]') AND [c].[name] = N'ParentPageId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Lists] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Lists] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Lists]') AND [c].[name] = N'BoardId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Lists] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Lists] ALTER COLUMN [BoardId] uniqueidentifier NULL;

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ListContents]') AND [c].[name] = N'ListId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ListContents] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [ListContents] ALTER COLUMN [ListId] uniqueidentifier NULL;

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[JustPageContents]') AND [c].[name] = N'ParentPageId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [JustPageContents] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [JustPageContents] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GalleryContents]') AND [c].[name] = N'GalleryId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [GalleryContents] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [GalleryContents] ALTER COLUMN [GalleryId] uniqueidentifier NULL;

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Galleries]') AND [c].[name] = N'ParentPageId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Galleries] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Galleries] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Calendars]') AND [c].[name] = N'ParentPageId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Calendars] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Calendars] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CalendarContents]') AND [c].[name] = N'CalendarId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [CalendarContents] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [CalendarContents] ALTER COLUMN [CalendarId] uniqueidentifier NULL;

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Boards]') AND [c].[name] = N'ParentPageId');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Boards] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Boards] ALTER COLUMN [ParentPageId] uniqueidentifier NULL;

ALTER TABLE [Lists] ADD CONSTRAINT [FK_Lists_Pages_ParentPageId] FOREIGN KEY ([ParentPageId]) REFERENCES [Pages] ([Id]) ON DELETE NO ACTION;

ALTER TABLE [Pages] ADD CONSTRAINT [FK_Pages_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE SET NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250321161242_FixCascadePaths', N'9.0.2');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250321162044_FixCascadePaths2', N'9.0.2');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250321162308_FixCascadePaths3', N'9.0.2');

COMMIT;
GO

