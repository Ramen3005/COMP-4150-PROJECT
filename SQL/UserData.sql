CREATE TABLE [dbo].[UserData] (
    [UserID]   INT           IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserName] VARCHAR (20)  NOT NULL,
    [Password] VARCHAR (25)  NOT NULL,
    [Premium]  BIT           NOT NULL,
    [FullName] NVARCHAR (50) NULL,
);