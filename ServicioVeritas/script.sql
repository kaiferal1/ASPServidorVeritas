USE [master]
GO
/****** Object:  Database [VeritasDocumentos]    Script Date: 5/28/2018 10:09:47 AM ******/
CREATE DATABASE [VeritasDocumentos]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VeritasDocumentos', FILENAME = N'D:\Microsoft SQL Server\MSSQL12.CONTPAQI\MSSQL\DATA\VeritasDocumentos.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VeritasDocumentos_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL12.CONTPAQI\MSSQL\DATA\VeritasDocumentos_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VeritasDocumentos] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VeritasDocumentos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VeritasDocumentos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET ARITHABORT OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VeritasDocumentos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VeritasDocumentos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VeritasDocumentos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VeritasDocumentos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VeritasDocumentos] SET  MULTI_USER 
GO
ALTER DATABASE [VeritasDocumentos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VeritasDocumentos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VeritasDocumentos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VeritasDocumentos] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VeritasDocumentos] SET DELAYED_DURABILITY = DISABLED 
GO
USE [VeritasDocumentos]
GO
/****** Object:  Table [dbo].[TB_ArchivoNoReporte]    Script Date: 5/28/2018 10:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ArchivoNoReporte](
	[id_Archivo] [int] NULL,
	[NumeroReporte] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Archivos]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Archivos](
	[idArchivo] [int] IDENTITY(1,1) NOT NULL,
	[lineaNegocio] [varchar](100) NULL,
	[nombre] [varchar](max) NULL,
	[Estatus] [varchar](100) NULL,
	[Observaciones] [varchar](200) NULL,
	[TipoArchivo] [varchar](100) NULL,
	[Folio] [varchar](50) NULL,
	[ECorrejido] [varchar](100) NULL,
	[fechaArchivo] [datetime] NULL,
	[id_Cliente] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ClienteCombinado]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ClienteCombinado](
	[Id_Cliente] [int] IDENTITY(1,1) NOT NULL,
	[CodigoCliente] [varchar](max) NULL,
	[Nombre] [varchar](max) NULL,
	[RFC] [varchar](100) NULL,
	[FormatoCoreo] [int] NULL,
	[AplicaContrato] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_ClienteCorreos]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_ClienteCorreos](
	[id_Cliente] [int] NOT NULL,
	[Correo] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoD1]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoD1](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[cabecera] [varchar](50) NULL,
	[Campovacio1] [varchar](50) NULL,
	[NumeroReporte] [varchar](max) NULL,
	[Descripcion_Prueba] [varchar](max) NULL,
	[Campovacio2] [varchar](50) NULL,
	[Cantidad] [money] NULL,
	[PrecioUnitario] [money] NULL,
	[PrecioSubTotal] [money] NULL,
	[TotalConDescuentoCargo] [money] NULL,
	[Descuento] [money] NULL,
	[CargoExtra] [money] NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE0]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE0](
	[ID_registro] [int] IDENTITY(1,1) NOT NULL,
	[NombreArchivo] [varchar](50) NULL,
	[Cabecera] [varchar](50) NULL,
	[campoVacio1] [varchar](200) NULL,
	[nombreEmpresa] [varchar](max) NULL,
	[RFC] [nvarchar](200) NULL,
	[Calle] [varchar](max) NULL,
	[No_interior] [varchar](50) NULL,
	[No_exterior] [varchar](50) NULL,
	[Colonia] [varchar](max) NULL,
	[Municipio] [varchar](max) NULL,
	[CampoVacio2] [varchar](50) NULL,
	[Estado] [varchar](max) NULL,
	[Pais] [varchar](200) NULL,
	[CodigoPostal] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE1]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE1](
	[id_E2] [int] IDENTITY(1,1) NOT NULL,
	[nombreArchivo] [varchar](50) NULL,
	[FoliFactura] [varchar](100) NULL,
	[fecha] [datetime] NULL,
	[formadePago] [varchar](200) NULL,
	[campovacio1] [varchar](50) NULL,
	[campovacio2] [varchar](50) NULL,
	[totalFactura] [money] NULL,
	[SubTotal] [money] NULL,
	[campovacio3] [varchar](50) NULL,
	[campovacio4] [varchar](50) NULL,
	[numerico1] [varchar](50) NULL,
	[numerico2] [varchar](50) NULL,
	[metododePago] [varchar](200) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE3]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE3](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[Cabecera] [varchar](50) NULL,
	[Campovacio1] [varchar](50) NULL,
	[Campovacio2] [varchar](50) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE4]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE4](
	[id_Registro] [int] NULL,
	[nombreArchivo] [varchar](50) NULL,
	[cabecera] [varchar](50) NULL,
	[RFC] [varchar](max) NULL,
	[RazonSocialCliente] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE5]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE5](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[nombreArchivo] [varchar](50) NULL,
	[cabecera] [varchar](50) NULL,
	[calle] [varchar](max) NULL,
	[numeroInterior] [varchar](50) NULL,
	[numeroExterior] [varchar](50) NULL,
	[Colonia] [varchar](max) NULL,
	[Campovacio1] [varchar](50) NULL,
	[Municipio] [varchar](max) NULL,
	[Estado] [varchar](max) NULL,
	[Pais] [varchar](100) NULL,
	[CodigoPostal] [varchar](50) NULL,
	[EmailCliente] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE6]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE6](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[cabecera] [varchar](50) NULL,
	[NumeroCuenta] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE8]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE8](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[NombreArchivo] [varchar](50) NULL,
	[Cabecera] [varchar](50) NULL,
	[CampoVacio1] [varchar](50) NULL,
	[monedaU] [varchar](100) NULL,
	[TipoCambio] [varchar](100) NULL,
	[CampoVacio2] [varchar](50) NULL,
	[CampoVacio3] [varchar](50) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoE9]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoE9](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[cabecera] [varchar](50) NULL,
	[cuentaXPagar] [varchar](100) NULL,
	[ContactoCliente] [varchar](200) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoEC1]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoEC1](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[nombreArchivo] [varchar](50) NULL,
	[cabecera] [varchar](50) NULL,
	[numerito] [varchar](50) NULL,
	[nota] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_DocumentoEC15]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_DocumentoEC15](
	[id_Registro] [int] IDENTITY(1,1) NOT NULL,
	[nombrearchivo] [varchar](50) NULL,
	[cabecera] [varchar](50) NULL,
	[totalpyl] [varchar](max) NULL,
	[id_Archivo] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Logins]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Logins](
	[id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](100) NULL,
	[Contraseña] [varchar](100) NULL,
	[FechaCreacion] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Parametros]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Parametros](
	[id_Parametro] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[valor] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Plantillas]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Plantillas](
	[iD_Template] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Asunto] [varchar](100) NULL,
	[Cuerpo] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TB_Servicio]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_Servicio](
	[id_Error] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Proceso] [varchar](100) NULL,
	[FechaArchivo] [datetime] NULL,
	[NombreArchivo] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_AGREGACorreoCliente]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[SP_AGREGACorreoCliente]
	@RFC varchar(200),
	@correo varchar(max)
AS
BEGIN
		declare @id_Cliente int = (select top(1) id_Cliente from TB_ClienteCombinado where RFC =@RFC )
		IF(NOT EXISTS(SELECT * FROM dbo.TB_ClienteCorreos WHERE id_Cliente = @id_Cliente AND Correo = @correo))
		BEGIN
			insert into TB_ClienteCorreos values (@id_Cliente,@correo) 
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Asigna_Clientes_DatosExtra]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Asigna_Clientes_DatosExtra] 
	
AS
BEGIN
 --   declare @RFCClienteCMPQ nvarchar(200)

	--ALTER table #TemporalCliente (id_contpaq int ,RFCClienteContpaq)

	--insert into #TemporalCliente --selec tabla de contpaq y conexion para extraer los datos de acuerdo a lo planeado
	--while (exists (select *from #TemporalCliente ))
	--begin
	--	--agregar cada uno de los clientes agregados y diseñar interfaz para modificar los campos extra 
	--	-- en dado caso que no solo asignar desde excel o algo asi cholo
	--end
	select ''
END
GO
/****** Object:  StoredProcedure [dbo].[sp_buscaContrato]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_buscaContrato]
	 @idArchivo int 
AS
BEGIN
	 select distinct NumeroReporte from TB_ArchivoNoReporte where id_Archivo= @idArchivo
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BuscarVal]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_BuscarVal] 
	@bandera int=0,
	@val varchar(Max)=''
AS
BEGIN
	
	
	if(@bandera = 1)--cliente
	begin 
		Select [Id_Cliente]
      ,[Nombre]
	  ,[RFC]
	  ,[AplicaContrato]
	  ,[FormatoCoreo] 
	  as 'FORMATOCORREO'
	  from dbo.TB_ClienteCombinado where nombre like '%'+@val+'%'
		or RFC like '%'+@val+'%' or FormatoCoreo like '%'+@val+'%'
		ORDER by nombre
	end
	
	if(@bandera = 2)--errores
	begin
		select ID_ERROR AS 'FOLIO ERROR',DESCRIPCION AS 'DESCRIPCION', PROCESO AS 'ESTATUS PROCESO',NombreArchivo as 'Archivo',FORMAT( FechaArchivo, 'dd/MM/yyyy', 'en-US' ) as 'Fecha'
		 from dbo.TB_Servicio where Descripcion like '%'+@val+'%'
      or Proceso like '%'+@val+'%'
      or NombreArchivo like '%'+@val+'%'
		
	end

	if(@bandera = 3)---enviados
	begin
		select idArchivo,
		a.nombre as 'aNombre',
		c.Nombre as 'cNombre',
		c.RFC
		from dbo.TB_Archivos a inner join dbo.TB_ClienteCombinado c on
		a.id_Cliente=c.Id_Cliente
		where Estatus='Enviado' and a.nombre like '%'+@val+'%' or c.nombre like '%'+@val+'%'
		or Estatus like '%'+@val+'%' or Observaciones like '%'+@val+'%'
		or Folio like '%'+@val+'%'
	end

	if(@bandera = 4)--no enviados
	begin
		select idArchivo,
		a.nombre as 'aNombre',
		c.Nombre as 'cNombre',
		c.RFC
		from dbo.TB_Archivos a inner join dbo.TB_ClienteCombinado c on
		a.id_Cliente=c.Id_Cliente
		where Estatus= 'Procesado' and a.nombre like '%'+@val+'%' or c.nombre like '%'+@val+'%'
		or Estatus like '%'+@val+'%' or Observaciones like '%'+@val+'%'
		or Folio like '%'+@val+'%'
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ConsultaCodCliProv]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ConsultaCodCliProv]
	@RFC varchar(200),
	@nombreCliente varchar(max)
AS
BEGIN
	

	
	if(@RFC = 'XEXX010101000')
	begin
		select CCODIGOCLIENTE from [.\contpaqi].[adPRUEBAS_Bureau_Verit].dbo.admClientes WHERE CRAZONSOCIAL like '%'+@nombreCliente+'%'
	end
	else
	begin
		select CCODIGOCLIENTE from [.\contpaqi].[adPRUEBAS_Bureau_Verit].dbo.admClientes WHERE CRFC=@RFC
	end

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ConsultaParametrosProv]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ConsultaParametrosProv]
@CodProv varchar(max)
AS
BEGIN
	select 
	 CMETODOPAG ,
     CUSOCFDI ,
     CTEXTOEXTRA2 
	from adPRUEBAS_Bureau_Verit.dbo.admClientes
	where CCODIGOCLIENTE=@CodProv
END
GO
/****** Object:  StoredProcedure [dbo].[SP_EDITACLIENTE]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_EDITACLIENTE] 
	@RFC VARCHAR(200),
	@APLICONTRA INT,
	@FCORREO int
	--@Correo varchar(max)
AS
BEGIN
	
		update TB_ClienteCombinado set   AplicaContrato=@APLICONTRA,FormatoCoreo =@FCORREO
		where RFC=@RFC
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GenerarReporte]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GenerarReporte] 
	@ClientesRegistrados	INT = 0,
	@Errores				INT = 0,
	@FacturasTesting		INT = 0,
	@CreditoTesting			INT = 0,
	@FacturasInspeccion		INT = 0,
	@CreditoInspeccion		INT = 0
AS
BEGIN
	SELECT @ClientesRegistrados = COUNT(Id_Cliente) FROM dbo.TB_ClienteCombinado --WHERE
	SELECT @Errores				= COUNT(id_Error) FROM dbo.TB_Servicio --WHERE
	SELECT @FacturasTesting		= COUNT(idArchivo) FROM dbo.TB_Archivos WHERE Estatus='Procesado'
	SELECT @CreditoTesting		= COUNT(idArchivo) FROM dbo.TB_Archivos WHERE Estatus='Procesado'
	SELECT @FacturasInspeccion	= COUNT(idArchivo) FROM dbo.TB_Archivos WHERE Estatus='Enviado'
	SELECT @CreditoInspeccion	= COUNT(idArchivo) FROM dbo.TB_Archivos WHERE Estatus='Enviado'
	SELECT @ClientesRegistrados 'ClientesRegistrados',
	@Errores 'Errores',
	@FacturasTesting 'FacturasTesting',
	@CreditoTesting 'CreditoTesting',
	@FacturasInspeccion 'FacturasInspeccion',
	@CreditoInspeccion 'CreditoInspeccion'
END

GO
/****** Object:  StoredProcedure [dbo].[SP_ManejaParametros]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ManejaParametros]
	@bandera int,
	@nombre varchar(50),
	@RutaDir varchar(max)
AS
BEGIN

	--Modificacion de rutas
	if(@bandera =0 )
	begin
		if(exists(select * from TB_Parametros where valor=@RutaDir and nombre != @nombre))
		begin	
			select 12/0
			
		end
		else
		begin
			update TB_Parametros set valor=@RutaDir where nombre=@nombre
			select 'DATOS MODIFICADOS CORRECTAMENTE'
		end
	end	
	--Caso de muestra de parametros
	if(@bandera =1)
	begin 
		select 
		nombre as 'DESCRIPCION',
		valor AS 'RUTA DIRECTORO'
		from TB_Parametros
		ORDER BY nombre 
	end
	if(@bandera =2)
	begin
		select valor from TB_Parametros where nombre=@nombre
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ManejaPlantilla]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ManejaPlantilla]
	/*
	1		Lista todos los elementos
	2		Ingresa un nuevo registro
	3		Edita un  registro seleccionado
	4		Busca un registro en espesifico
	*/
	@nombre varchar(50)='',
	@asunto varchar(50)='',
	@cuerpo nvarchar(max)='',
	@id		int=0,
	@bandera int
AS
BEGIN
	if(@bandera=1)
	begin
		select iD_Template,Nombre FROM dbo.TB_Plantillas
	end
	if(@bandera=2)
	begin
		insert into tb_Plantillas(Nombre,Asunto,Cuerpo) VALUES(@nombre,@asunto,@cuerpo) 
	end
	if(@bandera=3)
	begin
		update dbo.TB_Plantillas set Nombre=@nombre,Asunto=@asunto,Cuerpo= @cuerpo
		where iD_Template=@id
	end
	if(@bandera=4)
	begin
		select iD_Template,Nombre,Asunto,Cuerpo FROM dbo.TB_Plantillas where iD_Template=@id
	end
	if(@bandera=5)
	begin
		delete dbo.TB_Plantillas where iD_Template=@id
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ManejaServicio]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ManejaServicio] 
	@descripcion varchar(max) = '',
	@badera varchar(10) ='',
	@NombreArchivo varchar(100)='',
    @Fecha datetime ='',
	@id int = 0
AS
BEGIN
	
	if(@badera=0)
	begin
		--******** =	CASO DE INSERCION    =*********--
		-- ES DECIR EXISTE ALGUN ERROR POR EL CUAL EL SERVICIO SE VA A DETENER --
		
		INSERT INTO TB_Servicio SELECT  @descripcion,'DETENIDO',@Fecha,@NombreArchivo
	end
	ELSE
	BEGIN
		IF(@badera =1)
		BEGIN
			--****** = CASO DE MUESTRA = ******--
			
			SELECT top(7)  ID_ERROR AS 'FOLIO ERROR',DESCRIPCION AS 'DESCRIPCION', PROCESO AS 'ESTATUS PROCESO',NombreArchivo as 'Archivo',FORMAT( FechaArchivo, 'dd/MM/yyyy', 'en-US' ) as 'Fecha' FROM TB_SERVICIO  where Proceso='DETENIDO' order by id_Error desc		END
		ELSE
		BEGIN
			IF(@badera=2)
			BEGIN
				--****** = ES CASO DE MODIFICACION = ******--
				-- ESTE CASO SE OCUPARA EN EL BOTON REINICIAR PROCESO 
				--UPDATE TB_Servicio SET Proceso = 'EN EJECUCION' --aqui va un where??
				print 'esto es temporal hasta saber que where se ocupara o con que where se esta filtrando la informacion'
			END
			ELSE
			BEGIN
				IF(@badera=3)
				BEGIN
					--****** = EN CASO DE VERIFICACIONES AUTOMATICAS = ******--
					-- SOLO  PARA VERIFICACION Y CONTINUIDAD EN EL PROCESO 

					if(exists (select * from TB_Servicio where Proceso='DETENIDO'))
					BEGIN
					--SELECCIONAMOS SI TENEMOS  ALGUN ERROR DENTRO DEL PROCESO MANDAMOS A DECIRLE QUE ESTE DETENIDO
						SELECT 'DETENIDO'
					END
					ELSE
					BEGIN
					-- EN CASO CONTRARIO ES DECIR NO HAY DOCUMENTOS DETENIEDOS O YA ESTAN CORREGIDOS SE MANDA BANDERA DE EJECUCION Y 
					-- QUE SIGA EL PROCESO
						SELECT 'EJECUCION'
					END
				END
			END
		END

	END
		
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Manejo_Archivos]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Manejo_Archivos] 
	@lineaN varchar(50)='',
	@nombre varchar(max)='',
	@estatus varchar(100)='',
	@fecha	datetime='',
	@NumeroReporte varchar(200)='',
	@bandera int=0,
	@cliente varchar(300)='',
	@id int =0
AS
BEGIN

	if(@bandera=0)
	begin 
			--insercion  de datos principal
			declare  @quees varchar(10)=SUBSTRING(@nombre,1,1)
			if((@quees='9') or @quees='7')
			begin
				insert into TB_Archivos select @lineaN,@nombre,@estatus,'','Nota de Credito',@nombre,'',@fecha,(select top(1) id_Cliente from TB_ClienteCombinado where CodigoCliente=@cliente)
			end
			else
			begin 
				insert into TB_Archivos select @lineaN,@nombre,@estatus,'','Factura',@nombre,'',@fecha,(select top(1) id_Cliente from TB_ClienteCombinado where CodigoCliente=@cliente)	
			end
	end
	
	if(@bandera=1)
	begin 
		-- en casop de insertcion de reporte se tiene que agrergar los D1 se va a ejecutar la insercion cada que se decee

			 insert into TB_ArchivoNoReporte select idArchivo,@NumeroReporte  from TB_Archivos where nombre=@nombre and fechaArchivo = @fecha
		end
	
	if(@bandera =2)
	begin
		update TB_Archivos set Estatus ='Enviado' where nombre=@nombre
	end

	if(@bandera = 3)
	begin
		select idArchivo,
		a.nombre as 'aNombre',
		c.Nombre as 'cNombre',
		c.RFC
		from dbo.TB_Archivos a inner join dbo.TB_ClienteCombinado c on
		a.id_Cliente=c.Id_Cliente
		where Estatus= @estatus--'Procesado' --'Enviado'
	end

	if(@bandera = 4)
	begin
		select
		p.Asunto,p.Cuerpo,a.nombre, c.Nombre,c.RFC
		from dbo.TB_Archivos a 
		inner join dbo.TB_ClienteCombinado c on a.id_Cliente = c.Id_Cliente
		left join dbo.TB_Plantillas p on c.FormatoCoreo = p.iD_Template
		where a.idArchivo= @id
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_MuestraClientes]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[SP_MuestraClientes]

@bandera int = 0,
@Pagina int =0,
@RegistrosporPagina int = 0,
@RFC varchar(200)=''
AS
BEGIN

	if(@bandera=1)
	begin
		SELECT  [TB_ClienteCombinado].[Id_Cliente]
      ,[Nombre]
	  ,[RFC]
	  ,[AplicaContrato]
	  ,[FormatoCoreo] 
	  as 'FORMATOCORREO',
	  [TB_ClienteCorreos].[Correo]
		FROM [dbo].[TB_ClienteCombinado]
		inner join [dbo].[TB_ClienteCorreos]
		on [dbo].[TB_ClienteCombinado].Id_Cliente= [dbo].[TB_ClienteCorreos].id_Cliente
		order by [Nombre]
	end

	if(@bandera=2)
	begin
		SELECT COUNT(Nombre) as Cantidad
		FROM [dbo].[TB_ClienteCombinado]
	end

	if(@bandera=3)
	begin
		SELECT * FROM (
		SELECT ROW_NUMBER()Over(Order by dbo.TB_ClienteCombinado.RFC Asc) As RowNum,
			dbo.TB_ClienteCombinado.Nombre,
			dbo.TB_ClienteCombinado.RFC,
			dbo.TB_ClienteCombinado.AplicaContrato,
			dbo.TB_ClienteCombinado.FormatoCoreo,
			dbo.[TB_ClienteCorreos].Correo
		FROM [dbo].[TB_ClienteCombinado]
		inner join [dbo].[TB_ClienteCorreos]
		on [dbo].[TB_ClienteCombinado].Id_Cliente= [dbo].[TB_ClienteCorreos].id_Cliente
		)
		AS ResultadoPaginado
		WHERE RowNum BETWEEN (@Pagina - 1) * (@RegistrosporPagina + 1) AND @Pagina * @RegistrosporPagina
	end
	
	if(@bandera=4)
	begin
		select
		[TB_ClienteCorreos].Correo,
		TB_Archivos.idArchivo ,
		TB_Archivos.nombre,
		tb_plantillas.Asunto,
		tb_plantillas.cuerpo,
		TB_ClienteCombinado.AplicaContrato
		from TB_ClienteCombinado
		inner join TB_Archivos 
		on TB_Archivos.id_Cliente = TB_ClienteCombinado.Id_Cliente
		inner join tb_plantillas
		on tb_plantillas.id_Template=TB_ClienteCombinado.FormatoCoreo
		inner join [dbo].[TB_ClienteCorreos]
		on [dbo].[TB_ClienteCombinado].Id_Cliente= [dbo].[TB_ClienteCorreos].id_Cliente
		where  TB_ClienteCombinado.RFC=@RFC
		
	end

	if(@bandera=5)
	begin
		SELECT  [TB_ClienteCombinado].[Id_Cliente]
      ,[Nombre]
	  ,[RFC]
	  ,[AplicaContrato]
	  ,[FormatoCoreo] 
		FROM [dbo].[TB_ClienteCombinado]
		order by [Nombre]
	end
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_MuestraClientesSingular]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_MuestraClientesSingular] 
	@id_Cliente int,
	@RFC nvarchar(max)
AS
BEGIN
	select 
	   
      [Nombre]    
      ,[RFC]
      ,[AplicaContrato]
      ,[FormatoCoreo] AS 'FORMATO CORREO',
	  [Correo]
  FROM [VeritasDocumentos].[dbo].[TB_ClienteCombinado] a
  left join  [TB_ClienteCorreos] b
  on  a.Id_Cliente = b.id_Cliente
  WHERE rfc = @RFC AND a.id_Cliente = @id_Cliente
  order by [Nombre]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Reinicia_Serivicio]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Reinicia_Serivicio]
	
AS
BEGIN
	declare @id_Error int = 0,@nombreArchivo varchar(100)=''
	
	
	
	if(exists (select * from TB_Servicio where Proceso='DETENIDO'))
	begin
		set @id_Error = (select top(1) id_Error from TB_Servicio where Proceso='DETENIDO')
		set @nombreArchivo = (select NombreARchivo from TB_Servicio  where id_Error=@id_Error)
		update TB_Servicio set Proceso='EJECUCION' where id_Error=@id_Error 
		update TB_Archivos set ECorrejido='CORREGIDO' where nombre=@nombreArchivo
	end
	else
	begin
	   select 'No existen datos con proceso detenido'
	end 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ValidaUsusarios]    Script Date: 5/28/2018 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- ALTER date: <ALTER Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ValidaUsusarios]
	@usuario varchar(100),
	@Contraseña varchar(100),
	@bandera int
AS
BEGIN
	/*
	 recibe bandera 1
	 validacion
	 bandera 2
	insercion
	bnadera 3 muestra
	bandera 4 edicion
	*/
	if(@bandera=0)
	begin
	 --es caso de validar usuario
		if(exists(select * from TB_Logins where usuario=@usuario and Contraseña=@Contraseña))
		begin
			select 1
		end
		else
		begin
			select 0
		end
	end
	else
	begin
		if(@bandera=1)
		begin
			-- caso de insercion
			insert into TB_Logins  select  @usuario,@Contraseña,GETDATE()
		end
		else
		begin	
				--Cambio de contraseña 
				if(@bandera=2)
				begin
					update tb_Logins set Contraseña=@Contraseña where Usuario=@usuario
				end
				else
				if(@bandera =3)
				begin
				--caso de muestra de informacion 
					select usuario as 'Nombre Usuario',contraseña as 'Contraseña',fechaCreacion as 'Fecha' from TB_Logins order by Usuario
				end
				

		end

	end
	 
END
GO
USE [master]
GO
ALTER DATABASE [VeritasDocumentos] SET  READ_WRITE 
GO
