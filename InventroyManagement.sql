USE [InventroyManagement]
GO
/****** Object:  User [IMUser]    Script Date: 28/08/2023 17:38:05 ******/
CREATE USER [IMUser] FOR LOGIN [IMUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IMUser]
GO
/****** Object:  Table [dbo].[Bar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[Address] [nvarchar](400) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Bar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Beer]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[PercentageAlcoholByVolume] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Beer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BeerBar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeerBar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BeerId] [int] NOT NULL,
	[BarId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_BeerBar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BeerBrewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeerBrewery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BeerId] [int] NOT NULL,
	[BreweryId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_BeerBrewery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brewery](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](120) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Brewery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logger]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [nvarchar](50) NULL,
	[LoggedDate] [datetime] NOT NULL,
	[ExLevel] [nvarchar](10) NULL,
	[ExMessage] [nvarchar](200) NULL,
	[MethodName] [nvarchar](25) NULL,
	[Detail] [nvarchar](max) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BeerBar]  WITH CHECK ADD  CONSTRAINT [FK_BeerBar_Bar] FOREIGN KEY([BarId])
REFERENCES [dbo].[Bar] ([Id])
GO
ALTER TABLE [dbo].[BeerBar] CHECK CONSTRAINT [FK_BeerBar_Bar]
GO
ALTER TABLE [dbo].[BeerBar]  WITH CHECK ADD  CONSTRAINT [FK_BeerBar_Beer] FOREIGN KEY([BeerId])
REFERENCES [dbo].[Beer] ([Id])
GO
ALTER TABLE [dbo].[BeerBar] CHECK CONSTRAINT [FK_BeerBar_Beer]
GO
ALTER TABLE [dbo].[BeerBrewery]  WITH CHECK ADD  CONSTRAINT [FK_BeerBrewery_Beer] FOREIGN KEY([BeerId])
REFERENCES [dbo].[Beer] ([Id])
GO
ALTER TABLE [dbo].[BeerBrewery] CHECK CONSTRAINT [FK_BeerBrewery_Beer]
GO
ALTER TABLE [dbo].[BeerBrewery]  WITH CHECK ADD  CONSTRAINT [FK_BeerBrewery_Brewery] FOREIGN KEY([BreweryId])
REFERENCES [dbo].[Brewery] ([Id])
GO
ALTER TABLE [dbo].[BeerBrewery] CHECK CONSTRAINT [FK_BeerBrewery_Brewery]
GO
/****** Object:  StoredProcedure [dbo].[GetBar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBar]( @id int=0)
as

SELECT [Id]
      ,[Name]
      ,[Address]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
  FROM [dbo].[Bar]
     WHERE ((Id=@Id and isnull(@Id,0) <> 0) or (isnull(@Id,0)=0))
GO
/****** Object:  StoredProcedure [dbo].[GetBeer]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBeer]
( 
	@id int=0,
	@ltAlcoholByVolume decimal(4,2) = 0.0,
	@gtAlcoholByVolume decimal(4,2)=0.0
)
as 
Select * from Beer 
where (((id=@id and isnull(@id,0) <> 0) or (isnull(@id,0)=0))
and (((PercentageAlcoholByVolume >= @ltAlcoholByVolume 
and isnull(@ltAlcoholByVolume,0) <> 0 ) or (isnull(@ltAlcoholByVolume,0)=0))
and ((PercentageAlcoholByVolume <= @gtAlcoholByVolume 
and isnull(@gtAlcoholByVolume,0) <> 0 ) or (isnull(@gtAlcoholByVolume,0)=0)))
)


GO
/****** Object:  StoredProcedure [dbo].[GetBeerBar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBeerBar]( @Barid int=0,@BeerId int =0)
as
SELECT BB.[Id]
      ,[BeerId]
	  ,BE.[Name] as BeerName
      ,[BarId]
	  ,BA.[Name] as BarName
      ,BB.[IsActive]
      ,BB.[CreatedBy]
      ,BB.[CreatedDate]
      ,BB.[UpdatedBy]
      ,BB.[UpdatedDate]
  FROM [dbo].[BeerBar]BB  JOIN BAR BA ON BB.BarId = BA.Id
  JOIN Beer BE ON BB.BeerId = BE.Id
   WHERE ((BB.BarId=@BarId and isnull(@BarId,0) <> 0) or (isnull(@BarId,0)=0))
   AND ((BB.BeerId=@BeerId and isnull(@BeerId,0) <> 0) or (isnull(@BeerId,0)=0))
GO
/****** Object:  StoredProcedure [dbo].[GetBeerBrewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBeerBrewery]( @Breweryid int=0,@BeerId int =0)
as
SELECT BB.[Id]
      ,[BeerId]
	  ,BE.[Name] as BeerName
      ,[BreweryId]
	  ,BA.[Name] as BreweryName
      ,BB.[IsActive]
      ,BB.[CreatedBy]
      ,BB.[CreatedDate]
      ,BB.[UpdatedBy]
      ,BB.[UpdatedDate]
  FROM [dbo].[BeerBrewery]BB  JOIN Brewery BA ON BB.BreweryId = BA.Id
  JOIN Beer BE ON BB.BeerId = BE.Id
  WHERE ((BreweryId=@Breweryid and isnull(@Breweryid,0) <> 0) or (isnull(@Breweryid,0)=0))
   AND ((BeerId=@BeerId and isnull(@BeerId,0) <> 0) or (isnull(@BeerId,0)=0))

GO
/****** Object:  StoredProcedure [dbo].[GetBrewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetBrewery]( @id int=0)
as

SELECT [Id]
      ,[Name]
      ,[IsActive]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[UpdatedBy]
      ,[UpdatedDate]
  FROM [dbo].[Brewery]
    WHERE ((Id=@id and isnull(@id,0) <> 0) or (isnull(@id,0)=0))

GO
/****** Object:  StoredProcedure [dbo].[InsertBeerBar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertBeerBar](@Beerid int , @BarId int,@IsActive bit,@CreatedBy int)
as


Declare @NewId int =0 
if not exists(select * from Beer where id = @Beerid)
	BEGIN
		SET @NewId = -1
	END 
	if not exists(select * from Bar where id = @Barid)
	BEGIN
		SET @NewId = case when @NewId = -1 then -4 else -2 end 
	END
	if exists(select * from BeerBar where BarId = @Barid and BeerId= @Beerid)
	BEGIN
		SET @Newid= -6
	END
IF @NewId =0 
	BEGIN

		INSERT INTO [dbo].[BeerBar]
				   ([BeerId]
				   ,[BarId]
				   ,[IsActive]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   )
			 VALUES
				   (@BeerId
				   ,@BarId
				   ,@IsActive
				   ,@CreatedBy
				   ,GETDATE()
				   )
		SET @NewId = SCOPE_IDENTITY();
	END
Select @NewId as [NewId]
GO
/****** Object:  StoredProcedure [dbo].[InsertBeerBrewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertBeerBrewery](@Beerid int , @BreweryId int,@IsActive bit,@CreatedBy int)
as

Declare @NewId int =0 
if not exists(select * from Beer where id = @Beerid)
	BEGIN
		SET @NewId = -1
	END 
	if not exists(select * from Brewery where id = @BreweryId)
	BEGIN
		SET @NewId = case when @NewId = -1 then -5 else -3 end 
	END
	if exists(select * from BeerBrewery where BreweryId = @BreweryId and BeerId= @Beerid)
	BEGIN
		SET @Newid= -7
	END
IF @NewId =0 
	BEGIN

INSERT INTO [dbo].[BeerBrewery]
           ([BeerId]
           ,[BreweryId]
           ,[IsActive]
           ,[CreatedBy]
           ,[CreatedDate]
           )
     VALUES
           (@BeerId
           ,@BreweryId
           ,@IsActive
           ,@CreatedBy
           ,GETDATE())

SET @NewId = SCOPE_IDENTITY();
END
Select @NewId as [NewId]
GO
/****** Object:  StoredProcedure [dbo].[InsertLog]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertLog](@RequestId nvarchar(50), @ExLevel nvarchar(10),@ExMessage nvarchar(200),@MethodName nvarchar(25),@Detail nvarchar(MAX))
as
INSERT INTO [dbo].[Logger]
           ([RequestId]
           ,[LoggedDate]
           ,[ExLevel]
           ,[ExMessage]
           ,[MethodName]
           ,[Detail])
     VALUES
           (@RequestId,
           getdate(),
           @ExLevel, 
           @ExMessage,
           @MethodName,
           @Detail )
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateBar]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertUpdateBar](@id int , @Name nvarchar(120),@Address nvarchar(400),@IsActive bit,@CreatedBy int,@UpdatedBy int)
as

Declare @NewId int =0 
if not exists(select * from Bar where id = @id)
	BEGIN
		SET @NewId = -2
	END

IF (isnull(@id,0)=0)
	BEGIN
			INSERT INTO [dbo].[Bar]
					   ([Name]
					   ,[Address]
					   ,[IsActive]
					   ,[CreatedBy]
					   ,[CreatedDate]
					   )
				 VALUES
					   (@Name
					   ,@Address
					   ,@IsActive
					   ,@CreatedBy
					   ,getdate())
		SET @NewId = SCOPE_IDENTITY();
	END
ELSE 
	BEGIN
			IF @NewId =0 
				BEGIN

					UPDATE [dbo].[Bar]
					SET [Name] = @Name
					  ,[Address] = @Address
					  ,[IsActive] = @IsActive
					  ,[UpdatedBy] = @UpdatedBy
					  ,[UpdatedDate] = getdate()
					 WHERE Id = @Id
			
					SET @Newid = @id
				END
	END
	Select @NewId as [NewId]
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateBeer]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertUpdateBeer](@id int , @Name nvarchar(120),@PercentageAlcoholByVolume float,
@IsActive bit,@CreatedBy int,@UpdatedBy int)

as
Declare @NewId int =0 
if not exists(select * from Beer where id = @id)
	BEGIN
		SET @NewId = -1
	END

IF(isnull(@Id,0)=0)
	BEGIN
		INSERT INTO [dbo].[Beer]
				   ([Name]
				   ,[PercentageAlcoholByVolume]
				   ,[IsActive]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   )
			 VALUES
				   (@Name
				   ,@PercentageAlcoholByVolume
				   ,@IsActive
				   ,@CreatedBy
				   ,getdate())
	SET @NewId = SCOPE_IDENTITY();
	END
ELSE
	BEGIN
		IF @NewId =0 
				BEGIN

						UPDATE [dbo].[Beer]
						   SET [Name] = @Name
							  ,[PercentageAlcoholByVolume] = @PercentageAlcoholByVolume
							  ,[IsActive] = @IsActive
							  ,[UpdatedBy] = UpdatedBy
							  ,[UpdatedDate] = getdate()
						 WHERE Id = @Id
					SET @Newid = @id
				END
	END
	Select @NewId as [NewId]
GO
/****** Object:  StoredProcedure [dbo].[InsertUpdateBrewery]    Script Date: 28/08/2023 17:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertUpdateBrewery](@id int , @Name nvarchar(120),@IsActive bit,@CreatedBy int,@UpdatedBy int)
as

Declare @NewId int =0 

if not exists(select * from Brewery where id = @id)
	BEGIN
		SET @NewId = -3
	END

IF(isnull(@Id,0)=0)
	BEGIN
		INSERT INTO [dbo].[Brewery]
				   ([Name]
				   ,[IsActive]
				   ,[CreatedBy]
				   ,[CreatedDate]
				   )
			 VALUES
				   (@Name
				   ,@IsActive
				   ,@CreatedBy
				   ,GETDATE())
		SET @NewId = SCOPE_IDENTITY();
	END
ELSE
	BEGIN
		IF @NewId =0 
				BEGIN

					   UPDATE [dbo].[Brewery]
						   SET [Name] = @Name
							  ,[IsActive] = @IsActive
							  ,[UpdatedBy] = @UpdatedBy
							  ,[UpdatedDate] = GETDATE()
						 WHERE Id = @Id
					SET @Newid = @id
			END
	END
	Select @NewId as [NewId]

GO
