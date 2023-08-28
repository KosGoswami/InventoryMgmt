
--DROP DATABASE [InventroyManagement]

USE [master]
GO

CREATE DATABASE [InventroyManagement]

CREATE LOGIN [IMUser] WITH PASSWORD=N'IMPwd',
	DEFAULT_DATABASE=[InventroyManagement],
	DEFAULT_LANGUAGE=[us_english],
	CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;
 GO
 
ALTER LOGIN [IMUser] ENABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [IMUser]
GO

USE [InventroyManagement] 
GO

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
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
	 ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
	IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
	ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
		[Detail] [nvarchar](MAX) NULL,
	 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
	ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

	CREATE PROCEDURE [dbo].[GetBar]
	(
		@id int=0
	)
	AS
	SELECT	[Id]
			,[Name]
			,[Address]
			,[IsActive]
			,[CreatedBy]
			,[CreatedDate]
			,[UpdatedBy]
			,[UpdatedDate]
	FROM	
			[dbo].[Bar]
	WHERE
			((Id=@Id AND ISNULL(@Id,0) <> 0) 
				or (ISNULL(@Id,0)=0))
	GO 

	/****** Object:  StoredProcedure [dbo].[GetBeer]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[GetBeer]
	( 
		@id int=0,
		@ltAlcoholByVolume decimal(4,2) = 0.0,
		@gtAlcoholByVolume decimal(4,2)=0.0
	)
	AS 
		SELECT	[Id]
				,[Name]
				,[PercentageAlcoholByVolume]
				,[IsActive]
				,[CreatedBy]
				,[CreatedDate]
				,[UpdatedBy]
				,[UpdatedDate]
		FROM	
				Beer 
		WHERE	
				(((id=@id AND ISNULL(@id,0) <> 0) OR (ISNULL(@id,0)=0))
				AND (((PercentageAlcoholByVolume >= @ltAlcoholByVolume 
				AND ISNULL(@ltAlcoholByVolume,0) <> 0 ) OR (ISNULL(@ltAlcoholByVolume,0)=0))
				AND ((PercentageAlcoholByVolume <= @gtAlcoholByVolume 
				AND ISNULL(@gtAlcoholByVolume,0) <> 0 ) OR (ISNULL(@gtAlcoholByVolume,0)=0)))
				)
	GO

	/****** Object:  StoredProcedure [dbo].[GetBeerBar]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[GetBeerBar]
	(
		@Barid int=0,
		@BeerId int =0
	)
	AS
	SELECT	BB.[Id]
			,[BeerId]
			,BE.[Name] AS BeerName
			,[BarId]
			,BA.[Name] AS BarName
			,BB.[IsActive]
			,BB.[CreatedBy]
			,BB.[CreatedDate]
			,BB.[UpdatedBy]
			,BB.[UpdatedDate]
	FROM	
			[dbo].[BeerBar]BB JOIN BAR BA ON BB.BarId = BA.Id
			JOIN Beer BE ON BB.BeerId = BE.Id
	WHERE
			((BB.BarId=@BarId AND ISNULL(@BarId,0) <> 0) OR (ISNULL(@BarId,0)=0))
				AND ((BB.BeerId=@BeerId AND ISNULL(@BeerId,0) <> 0) OR (ISNULL(@BeerId,0)=0))
	GO

	/****** Object:  StoredProcedure [dbo].[GetBeerBrewery]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[GetBeerBrewery]
	(
		@Breweryid int=0,
		@BeerId int =0
	)
	AS
	SELECT	BB.[Id]
			,[BeerId]
			,BE.[Name] AS BeerName
			,[BreweryId]
			,BA.[Name] AS BreweryName
			,BB.[IsActive]
			,BB.[CreatedBy]
			,BB.[CreatedDate]
			,BB.[UpdatedBy]
			,BB.[UpdatedDate]
	  FROM 
			[dbo].[BeerBrewery]BB  JOIN Brewery BA ON BB.BreweryId = BA.Id
				JOIN Beer BE ON BB.BeerId = BE.Id
	  WHERE 
			((BreweryId=@Breweryid AND ISNULL(@Breweryid,0) <> 0) OR (ISNULL(@Breweryid,0)=0))
				AND ((BeerId=@BeerId AND ISNULL(@BeerId,0) <> 0) OR (ISNULL(@BeerId,0)=0))
	GO

	/****** Object:  StoredProcedure [dbo].[GetBrewery]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[GetBrewery]
	(
		@id int=0
	)
	AS
	SELECT	[Id]
			,[Name]
			,[IsActive]
			,[CreatedBy]
			,[CreatedDate]
			,[UpdatedBy]
			,[UpdatedDate]
	FROM	
			[dbo].[Brewery]
	WHERE
			((Id=@id AND ISNULL(@id,0) <> 0) 
				OR (ISNULL(@id,0)=0))

	GO
	/****** Object:  StoredProcedure [dbo].[InsertBeerBar]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[InsertBeerBar]
	(
		@Beerid INT,
		@BarId int,
		@IsActive bit,
		@CreatedBy int
	)
	AS
	DECLARE @NewId int =0 

		IF EXISTS(SELECT Id FROM BeerBar WHERE BarId = @Barid AND BeerId= @Beerid)
		BEGIN
			SET @Newid= -6
		END
		ELSE IF NOT EXISTS(SELECT Id FROM Beer WHERE id = @Beerid)
		BEGIN
			SET @NewId = -1
		END 
		ELSE IF NOT EXISTS(SELECT Id FROM Bar WHERE id = @Barid)
		BEGIN
			SET @NewId = CASE WHEN @NewId = -1 THEN -4 ELSE -2 END 
		END

		IF @NewId =0 
		BEGIN
				INSERT INTO	[dbo].[BeerBar]
							([BeerId]
							,[BarId]
							,[IsActive]
							,[CreatedBy]
							,[CreatedDate]
							)
				 VALUES (
							@BeerId
						   ,@BarId
						   ,@IsActive
						   ,@CreatedBy
						   ,GETDATE()
					   )
				
				SET @NewId = SCOPE_IDENTITY();
		END

	SELECT @NewId AS [NewId]
	GO

	/****** Object:  StoredProcedure [dbo].[InsertBeerBrewery]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	
	CREATE PROCEDURE [dbo].[InsertBeerBrewery]
	(
		@Beerid int ,
		@BreweryId int,
		@IsActive bit,
		@CreatedBy int
	)
	AS
		DECLARE @NewId int =0 
	
		IF EXISTS(SELECT Id FROM BeerBrewery 
				WHERE BreweryId = @BreweryId AND BeerId= @Beerid)
		BEGIN
			SET @Newid= -7
		END
		ELSE IF NOT EXISTS(SELECT Id FROM Beer WHERE id = @Beerid)
		BEGIN
			SET @NewId = -1
		END 
		ELSE IF NOT EXISTS(SELECT Id FROM Brewery WHERE id = @BreweryId)
		BEGIN
			SET @NewId = cASe when @NewId = -1 then -5 else -3 end 
		END
		
		IF @NewId =0 
		BEGIN

		INSERT INTO	[dbo].[BeerBrewery]
				(	[BeerId]
				   ,[BreweryId]
				   ,[IsActive]
				   ,[CreatedBy]
				   ,[CreatedDate]
				)
		 VALUES (
					@BeerId
					,@BreweryId
					,@IsActive
					,@CreatedBy
					,GETDATE()
				)

		SET @NewId = SCOPE_IDENTITY();
		END

	SELECT @NewId AS [NewId]
	GO

	/****** Object:  StoredProcedure [dbo].[InsertLog]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[InsertLog]
	(
		@RequestId nvarchar(50),
		@ExLevel nvarchar(10),
		@ExMessage nvarchar(200),
		@MethodName nvarchar(25),
		@Detail nvarchar(MAX)
	)
	AS
		INSERT INTO	[dbo].[Logger]
				(	[RequestId]
				   ,[LoggedDate]
				   ,[ExLevel]
				   ,[ExMessage]
				   ,[MethodName]
				   ,[Detail]
				)
		 VALUES
			   (
					@RequestId,
					GETDATE(),
					@ExLevel, 
					@ExMessage,
					@MethodName,
					@Detail
				)
	GO

	/****** Object:  StoredProcedure [dbo].[InsertUpdateBar]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[InsertUpdateBar]
	(
		@id int,
		@Name nvarchar(120),
		@Address nvarchar(400),
		@IsActive bit,
		@CreatedBy int,
		@UpdatedBy int)
	AS
		DECLARE @NewId int =0 
	
		IF NOT EXISTS(SELECT Id FROM Bar WHERE id = @id)
		BEGIN
			SET @NewId = -2
		END

		IF (ISNULL(@id,0)=0)
		BEGIN
			INSERT INTO	[dbo].[Bar]
					(	[Name]
						,[Address]
						,[IsActive]
						,[CreatedBy]
						,[CreatedDate]
					)
			VALUES
				   (		@Name
						   ,@Address
						   ,@IsActive
						   ,@CreatedBy
						   ,GETDATE())
		
			SET @NewId = SCOPE_IDENTITY();
		END
		ELSE 
		BEGIN
			IF @NewId =0 
			BEGIN
				UPDATE	[dbo].[Bar]
				SET
						[Name] = @Name
						,[Address] = @Address
						,[IsActive] = @IsActive
						,[UpdatedBy] = @UpdatedBy
						,[UpdatedDate] = GETDATE()
				WHERE
						Id = @Id
			
				SET @Newid = @id
			END
		END
		SELECT @NewId AS [NewId]
	GO

	/****** Object:  StoredProcedure [dbo].[InsertUpdateBeer]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO
	CREATE PROCEDURE [dbo].[InsertUpdateBeer]
	(
		@id int ,
		@Name nvarchar(120),
		@PercentageAlcoholByVolume float,
		@IsActive bit,
		@CreatedBy int,
		@UpdatedBy int
	)
	AS
		DECLARE @NewId int =0 
		
		IF NOT EXISTS(SELECT Id FROM Beer WHERE id = @id)
		BEGIN
			SET @NewId = -1
		END

		IF(ISNULL(@Id,0)=0)
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
					   ,GETDATE())
		SET @NewId = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			IF @NewId =0 
			BEGIN
					UPDATE	[dbo].[Beer]
					SET		[Name] = @Name
							,[PercentageAlcoholByVolume] = @PercentageAlcoholByVolume
							,[IsActive] = @IsActive
							,[UpdatedBy] = UpdatedBy
							,[UpdatedDate] = GETDATE()
					WHERE	
							Id = @Id
						
					SET	@Newid = @id;
			END
		END
		SELECT @NewId AS [NewId]
	GO

	/****** Object:  StoredProcedure [dbo].[InsertUpdateBrewery]    Script Date: 28/08/2023 17:38:05 ******/
	SET ANSI_NULLS ON
	GO
	SET QUOTED_IDENTIFIER ON
	GO

	CREATE PROCEDURE [dbo].[InsertUpdateBrewery]
	(
		@id int,
		@Name nvarchar(120),
		@IsActive bit,
		@CreatedBy int,
		@UpdatedBy int
	)
	AS
		DECLARE @NewId int =0 

		IF NOT EXISTS(SELECT id FROM Brewery WHERE id = @id)
		BEGIN
			SET @NewId = -3
		END

		IF(ISNULL(@Id,0)=0)
		BEGIN
			INSERT INTO	[dbo].[Brewery]
					(
						[Name]
						,[IsActive]
						,[CreatedBy]
						,[CreatedDate]
					)
				VALUES
					(
						@Name
						,@IsActive
						,@CreatedBy
						,GETDATE()
					)

			SET @NewId = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			IF @NewId =0 
			BEGIN
				UPDATE	[dbo].[Brewery]
				SET	
						[Name] = @Name
						,[IsActive] = @IsActive
						,[UpdatedBy] = @UpdatedBy
						,[UpdatedDate] = GETDATE()
				WHERE	
						Id = @Id;

				SET @Newid = @id;
			END
		END
		SELECT @NewId AS [NewId]
	GO
