USE [netproje]
GO

/****** Object:  Table [dbo].[transactions]    Script Date: 19.04.2020 21:23:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[transactions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [money] NULL,
	[explanation] [nvarchar](150) NULL,
	[type] [nvarchar](50) NULL,
	[userid] [int] NULL,
 CONSTRAINT [PK_transactions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [netproje]
GO

/****** Object:  Table [dbo].[users]    Script Date: 19.04.2020 21:24:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[id] [int] NULL,
	[name] [nvarchar](150) NULL,
	[password] [nvarchar](150) NULL,
	[username] [nvarchar](150) NULL
) ON [PRIMARY]
GO



CREATE PROCEDURE loginUser @username nvarchar(150), @password nvarchar(150)
AS
begin
declare @loged BIT;
declare @userr nvarchar(150);
declare @id int;
set @loged=0;
 SELECT @userr=users.name , @id=id FROM users WHERE username = @username and password=@password;
if @userr is not null 
begin
SELECT * FROM users WHERE username = @username and password=@password;
end
else 
PRINT 'failed'
end

exec loginuser 'teyusuftu','s123y123';

ALTER TABLE transactions AUTO_INCREMENT=100;