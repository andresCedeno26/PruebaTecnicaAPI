USE [master]
GO
/****** Object:  Database [PruebaDVP]    Script Date: 8/1/2025 19:02:07 ******/
CREATE DATABASE [PruebaDVP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaDVP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PruebaDVP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaDVP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PruebaDVP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PruebaDVP] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaDVP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaDVP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaDVP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaDVP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaDVP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaDVP] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaDVP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaDVP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaDVP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaDVP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaDVP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaDVP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaDVP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaDVP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaDVP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaDVP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaDVP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaDVP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaDVP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaDVP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaDVP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaDVP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaDVP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaDVP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaDVP] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaDVP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaDVP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaDVP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaDVP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaDVP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PruebaDVP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PruebaDVP] SET QUERY_STORE = ON
GO
ALTER DATABASE [PruebaDVP] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PruebaDVP]
GO
/****** Object:  User [rcedeno]    Script Date: 8/1/2025 19:02:07 ******/
CREATE USER [rcedeno] FOR LOGIN [rcedeno] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [rcedeno]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[idPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](250) NOT NULL,
	[Apellidos] [varchar](250) NOT NULL,
	[Identificacion] [varchar](15) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[TipoIdentificacion] [varchar](50) NOT NULL,
	[FechaCreacion]  AS (getdate()),
	[NombreCompleto]  AS (([Nombres]+' ')+[Apellidos]),
	[IdentificacionTI]  AS ([Identificacion]+[TipoIdentificacion]),
	[FechaModificacion] [datetime] NULL,
	[FechaAnulacion] [datetime] NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[idPersona] [int] NOT NULL,
	[Usuario] [varchar](25) NOT NULL,
	[Pass] [varchar](500) NOT NULL,
	[FechaCreacion]  AS (getdate()),
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([idPersona], [Nombres], [Apellidos], [Identificacion], [Email], [TipoIdentificacion], [FechaModificacion], [FechaAnulacion], [Estado]) VALUES (5, N'Admin', N'Administrador', N'1234567785', N'admin@gmail.com', N'Cedula', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([idUsuario], [idPersona], [Usuario], [Pass], [Estado]) VALUES (6, 5, N'admin', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Personas] ADD  CONSTRAINT [DF_Personas_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Usuarios] ADD  CONSTRAINT [DF_Usuarios_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Personas] FOREIGN KEY([idPersona])
REFERENCES [dbo].[Personas] ([idPersona])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Personas]
GO
/****** Object:  StoredProcedure [dbo].[pAnulaPersona]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pAnulaPersona]
    @iJson nvarchar(max),
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
		@idPersona int

    BEGIN TRANSACTION;

    BEGIN TRY

		set @idPersona = CAST(JSON_VALUE(@iJson, '$.idPersona')as varchar(500));

		update Personas
		set Estado = 0,
			FechaAnulacion = GETDATE()
		where idPersona = @idPersona

		update Usuarios
		set Estado = 0
		where idPersona = @idPersona

        COMMIT TRANSACTION;
        SET @idError = 0;
        SET @Mensaje = 'OK';

    END TRY
    BEGIN CATCH

        ROLLBACK TRANSACTION;
        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[pConsultarPersonas]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pConsultarPersonas]
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
as 
begin
    SET NOCOUNT ON;

	begin try

		select NombreCompleto,
			   Identificacion,
			   Email,
			   Usuario,
			   p.idPersona,
			   Nombres,
			   Apellidos,
			   Identificacion,
			   Email,
			   TipoIdentificacion,
			   Usuario,
			   Pass = ''
		from Personas p
			 inner join Usuarios u on u.idPersona = p.idPersona
		where p.Estado = 1
		
        SET @idError = 0;
        SET @Mensaje = 'OK';
	end try
	begin catch

        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

	end catch
end 
GO
/****** Object:  StoredProcedure [dbo].[pCrearPersona]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pCrearPersona]
    @iJson nvarchar(max),
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @Nombres varchar(250),
        @Apellidos varchar(250),
        @Identificacion varchar(15),
        @Email varchar(150),
        @TipoIdentificacion varchar(50),
        @Usuario varchar(20),
        @Pass varchar(500)

    BEGIN TRANSACTION;

    BEGIN TRY

		set @Nombres = CAST(JSON_VALUE(@iJson, '$.Nombres')as varchar(250));
		set @Apellidos = CAST(JSON_VALUE(@iJson, '$.Apellidos')as varchar(250));
		set @Identificacion = CAST(JSON_VALUE(@iJson, '$.Identificacion')as varchar(15));
		set @Email = CAST(JSON_VALUE(@iJson, '$.Email')as varchar(150));
		set @TipoIdentificacion = CAST(JSON_VALUE(@iJson, '$.TipoIdentificacion')as varchar(50));
		set @Usuario = CAST(JSON_VALUE(@iJson, '$.Usuario')as varchar(25));
		set @Pass = CAST(JSON_VALUE(@iJson, '$.Pass')as varchar(500));

		if exists (select top 1 1 from Personas p where p.Identificacion = @Identificacion and Estado = 1)
		begin
			ROLLBACK TRANSACTION;
			SET @idError = 1;
			SET @Mensaje = 'Existe una persona con la identificacion ingresada.';	
			return
		end

		if exists (select top 1 1 from Usuarios p where p.Usuario = @Usuario and Estado = 1)
		begin
			ROLLBACK TRANSACTION;
			SET @idError = 1;
			SET @Mensaje = 'Existe una persona con usuario ingresado.';		
			return
		end

       INSERT INTO [dbo].[Personas]
           ([Nombres],[Apellidos],[Identificacion],[Email],[TipoIdentificacion],[Estado])
	   VALUES
           (@Nombres, @Apellidos, @Identificacion, @Email, @TipoIdentificacion, 1)

		INSERT INTO [dbo].[Usuarios]
           ([idPersona],[Usuario],[Pass])
		VALUES
           (scope_identity(),@Usuario,@Pass)

        COMMIT TRANSACTION;
        SET @idError = 0;
        SET @Mensaje = 'OK';

    END TRY
    BEGIN CATCH

        ROLLBACK TRANSACTION;
        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[pModificarPersona]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pModificarPersona]
    @iJson nvarchar(max),
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @Nombres varchar(250),
        @Apellidos varchar(250),
        @Identificacion varchar(15),
        @Email varchar(150),
        @TipoIdentificacion varchar(50),
        @Usuario varchar(20),
        @Pass varchar(500),
		@idPersona int

    BEGIN TRANSACTION;

    BEGIN TRY

		set @Nombres = CAST(JSON_VALUE(@iJson, '$.Nombres')as varchar(250));
		set @Apellidos = CAST(JSON_VALUE(@iJson, '$.Apellidos')as varchar(250));
		set @Identificacion = CAST(JSON_VALUE(@iJson, '$.Identificacion')as varchar(15));
		set @Email = CAST(JSON_VALUE(@iJson, '$.Email')as varchar(150));
		set @TipoIdentificacion = CAST(JSON_VALUE(@iJson, '$.TipoIdentificacion')as varchar(50));
		set @Usuario = CAST(JSON_VALUE(@iJson, '$.Usuario')as varchar(25));
		set @Pass = CAST(JSON_VALUE(@iJson, '$.Pass')as varchar(500));
		set @idPersona = CAST(JSON_VALUE(@iJson, '$.idPersona')as varchar(500));

		if exists (select top 1 1 from Personas p where p.Identificacion = @Identificacion and Estado = 1 and idPersona <> @idPersona)
		begin
			ROLLBACK TRANSACTION;
			SET @idError = 1;
			SET @Mensaje = 'Existe una persona con la identificacion ingresada.';	
			return
		end

		if exists (select top 1 1 from Usuarios p where p.Usuario = @Usuario and Estado = 1 and idPersona <> @idPersona)
		begin
			ROLLBACK TRANSACTION;
			SET @idError = 1;
			SET @Mensaje = 'Existe una persona con usuario ingresado.';		
			return
		end

		update Personas
		set Nombres = @Nombres,
			Apellidos = @Apellidos,
			Identificacion = @Identificacion,
			Email = @Email,
			TipoIdentificacion = @TipoIdentificacion,
			FechaModificacion = GETDATE()
		where idPersona = @idPersona

		update Usuarios
		set Usuario = @Usuario,
			Pass = @Pass
		where idPersona = @idPersona

        COMMIT TRANSACTION;
        SET @idError = 0;
        SET @Mensaje = 'OK';

    END TRY
    BEGIN CATCH

        ROLLBACK TRANSACTION;
        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[pObtenerPersonas]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pObtenerPersonas]
    @iJson nvarchar(max),
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
as 
begin

    SET NOCOUNT ON;

    DECLARE 
		@idPersona int

	begin try

		set @idPersona = CAST(JSON_VALUE(@iJson, '$.idPersona')as varchar(500));

		select Nombres,
			   Apellidos,
			   Identificacion,
			   Email,
			   TipoIdentificacion
			   Usuario,
			   Pass,
			   p.idPersona
		from Personas p
			 inner join Usuarios u on u.idPersona = p.idPersona
		where p.Estado = 1
			  and p.idPersona = @idPersona
		
        SET @idError = 0;
        SET @Mensaje = 'OK';
	end try
	begin catch

        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

	end catch
end 
GO
/****** Object:  StoredProcedure [dbo].[pValidaLogin]    Script Date: 8/1/2025 19:02:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pValidaLogin]
    @iJson nvarchar(max),
    @idError int OUTPUT,
    @Mensaje varchar(150) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
		@Usuario varchar(20),
        @Pass varchar(500)


    BEGIN TRY

		set @Usuario = CAST(JSON_VALUE(@iJson, '$.Usuario')as varchar(25));
		set @Pass = CAST(JSON_VALUE(@iJson, '$.Pass')as varchar(500));

		declare @idPersona int = 0

		select @idPersona = u.idPersona
		from Usuarios u
		where u.Usuario = @Usuario
			  and u.Pass = @Pass
			  and u.Estado = 1


		if (isnull(@idPersona,0) = 0)
		begin
			set @idError = 1
			set @Mensaje = 'Usuario o contraseña invalido.'
			return
		end

		select NombreCompleto,
			   Identificacion,
			   Email
		from Personas p
		where idPersona = @idPersona

        SET @idError = 0;
        SET @Mensaje = 'OK';

    END TRY
    BEGIN CATCH

        SET @idError = 1;
        SET @Mensaje = ERROR_MESSAGE(); 

    END CATCH;
END;
GO
USE [master]
GO
ALTER DATABASE [PruebaDVP] SET  READ_WRITE 
GO
