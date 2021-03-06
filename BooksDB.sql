USE [BooksDB]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 11/18/2021 5:15:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Author] [varchar](100) NOT NULL,
	[Price] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([BookID], [Title], [Author], [Price]) VALUES (2, N'Sonar Tori', N'Rabindronath Thakur*', 250)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
/****** Object:  StoredProcedure [dbo].[BookAddOrEdit]    Script Date: 11/18/2021 5:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[BookAddOrEdit]
@BookID INT,
@Title VARCHAR(50),
@Author VARCHAR(50),
@Price INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements.
	SET
	NOCOUNT ON;
	
	IF @BookID = 0
BEGIN
		INSERT
	INTO
	Books (Title,
	Author,
	Price)
VALUES(@Title,
@Author,
@Price)
END
ELSE
BEGIN
	UPDATE
	Books
SET
		Title = @Title,
		Author = @Author,
		Price = @Price
WHERE
	BookID = @BookID
END
END
GO
/****** Object:  StoredProcedure [dbo].[BookDeleteByID]    Script Date: 11/18/2021 5:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BookDeleteByID] 
	-- Add the parameters for the stored procedure here
	@BookID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE Books
	WHERE BookID = @BookID
END
GO
/****** Object:  StoredProcedure [dbo].[BookViewAll]    Script Date: 11/18/2021 5:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BookViewAll]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from 
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * 
	FROM Books
END
GO
/****** Object:  StoredProcedure [dbo].[BookViewByID]    Script Date: 11/18/2021 5:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[BookViewByID] 
	-- Add the parameters for the stored procedure here
	@BookID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM Books
	WHERE BookID = @BookID
END
GO
