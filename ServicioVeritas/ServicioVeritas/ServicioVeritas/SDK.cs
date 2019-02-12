using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace ServicioVeritas
{
    class SDK
    {
        

        public const int kLongCodigo = 30 + 1;
        public const int kLongNombre = 60 + 1;
        public const int kLongSerie = 12;
        public const int kLongNombreProducto = 255 + 1;
        public const int kLongFecha  = 23 + 1;
        public const int kLongAbreviatura  = 3 + 1;
        public const int kLongCodValorClasif = 3 + 1;
        public const int kLongTextoExtra = 50 + 1;
        public const int kLongNumSerie = 11 + 1;
        public const int kLongReferencia = 20 + 2;
        public const int kLongSeries = 30 + 1;
        public const int kLongDescripcion = 60 + 1;
        public const int kLongNumeroExtInt = 6 + 1;
        public const int kLongCodigoPostal = 6 + 1;
        public const int kLongTelefono = 15 + 1;
        public const int kLongEmailWeb   = 50 + 1;
        public const int kLongRFC = 20 + 1;
        public const int kLongCURP = 20 + 1;
        public const int kLongDesCorta = 20 + 1;
        public const int kLongDenComercial = 50 + 1;
        public const int kLongRepLegal = 50 + 1;
        public const int kLongNumeroExpandido = 30 + 1;
        public const int kLongMsgError = 512;



        // Funciones para inicializar el SDK
        [DllImport("KERNEL32")]
        public static extern int SetCurrentDirectory(string pPtrDirActual);



        // 'Public Declare Function fInicializaSDK Lib "MGW_SDK.DLL" () As Integer
        [DllImport("MGWServicios.DLL")]
        public static extern int fEditaDireccion();
        [DllImport("MGWServicios.DLL")]
        public static extern int fSetNombrePAQ(String aNombrePAQ);
        [DllImport("MGWServicios.DLL")]
        public static extern void fTerminaSDK();
        [DllImport("MGWServicios.DLL")]
        public static extern void fError(int NumeroError, StringBuilder Mensaje, int Longitud);

        [DllImport("MGWSERVICIOS.dll")]
        public static extern void fInicioSesionSDK(string aUsuario, string aContrasena);

        //' Funciones para Abrir y Cerrar la empresa

        [DllImport("MGWServicios.DLL")]
        public static extern void fInicioSesionSDKCONTPAQi(string aUsuario, string aContrasenia);

        [DllImport("MGWServicios.DLL")]
        public static extern int fAbreEmpresa(string Directorio);

        [DllImport("MGWServicios.DLL")]
        public static extern void fCierraEmpresa();

        //funciones de documento ajenas a primera parde del skd

        [DllImport("MGWServicios.DLL")]
        public static extern int fSetDatoProducto(string aCampo,string aValor);

        [DllImport("MGWServicios.DLL")]
        public static extern int fGuardaProducto();

       [DllImport("MGWServicios.DLL")]
        public static extern int fBuscaProducto(string aCodProducto);
       //Funciones de documento
        [DllImport("MGWServicios.DLL")]
        public static extern int fGuardaDocumento();

        [DllImport("MGWServicios.DLL")]
        public static extern int fAltaDocumento(ref int aIdDocumento, ref tDocumento aDocumento);

        [DllImport("MGWServicios.DLL")]
        public static extern int fSiguienteFolio(string aCodigoConcepto, string aSerie,ref  double aFolio);

        [DllImport("MGWServicios.DLL")]
        public static extern int fLeeDatoCteProv(string aCampo,string aValor,int aLen);
        [DllImport("MGWServicios.DLL")]
        public static extern int fBuscaIdCteProv(string lCodCteProv);

        [DllImport("MGWServicios.DLL")]
        public static extern int fBuscarDocumento(string aCodConcepto, string aSerie, string aFolio);
        [DllImport("MGWServicios.DLL")]
        public static extern int fSetDatoDocumento(string aCampo,string aValor);


         //funciones de Movimiento
        [DllImport("MGWServicios.DLL")]
        public static extern int fGuardaMovimiento();

        [DllImport("MGWServicios.DLL")]
        public static extern int fAltaMovimiento(Int32 aIdDocumento, ref Int32 lIdMovimiento, ref tMovimiento lMovimiento);

        [DllImport("MGWServicios.DLL")]
        public static extern int fInsertaMovimiento();

        [DllImport("MGWServicios.DLL")]
        public static extern int fSetDatoMovimiento(string aCampo, string aValor);


        //Funciones Timbrado  Funciones de Timbrado
        [DllImport("MGWServicios.DLL")]
        public static extern int fEntregEnDiscoXML(string aCodConcepto, string aSerie, Double aFolio, int aFormato, string aFormatoAmig);

        //[DllImport("MGWServicios.DLL")]
        //public static extern int fInicializaLicenseInfo(byte lSistema);

        //[DllImport("MGWServicios.DLL")]
        //public static extern int fTimbraNominaXML(string aRutaXML,string aCodConcepto, string aUUID, string aRutaDDA ,string aRutaResultado,string aPass,string aRutaFormato );

        //[DllImport("MGWServicios.DLL")]
        // public static extern int fTimbraXML (string aRutaXML ,string  aCodConcepto , string aUUID ,string aRutaDDA ,string aRutaResultado ,string aPass ,string aRutaFormato ) ;
        

        [DllImport("MGWServicios.DLL")]
        public static extern int fEmitirDocumento(string aCodConcepto, string aSerie, Double aFolio, string aPassword, string aArchivoAdicional);

        // public static extern faltamovi


        //************************************************ Declaración de funciones *****************************************


        [DllImport("MGWServicios.DLL")]
        public static extern int fAltaCteProv(ref int aIdCteProv, ref tCteProv astCteProv);


        [DllImport("MGWServicios.DLL")]
        public static extern Int32 fEditaCteProv();

        [DllImport("MGWServicios.DLL")]
        public static extern Int32 fSetDatoCteProv(string aCampo, string aValor);

        [DllImport("MGWServicios.DLL")]
        public static extern Int32 fGuardaCteProv();

        [DllImport("MGWServicios.DLL")]
        public static extern int fAltaDireccion(ref int tipoDireccion, ref tDireccion tDireccion);
        [DllImport("MGWServicios.DLL")]
        public static extern int fGuardaDireccion();
        [DllImport("MGWServicios.DLL")]
        public static extern int fSetDatoDireccion(string aCampo, string aValor);
       
        //*************************************************Implementación de código ******************************************

        //agegar metodo del sdk para modificar producto o movimiento

        public static string rError(int aError)
        {
            StringBuilder aMensaje = new StringBuilder(512);

            //      ' Recupera el mensaje de error del SDK
            fError(aError, aMensaje, 350);
            return aMensaje.ToString();
        }

        //Finaliza apartado de Insercion Documento
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tDocumento
        {
            public Double aFolio;
            public int aNumMoneda;
            public Double aTipoCambio;
            public Double aImporte;
            public Double aDescuentoDoc1;
            public Double aDescuentoDoc2;
            public int aSistemaOrigen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodConcepto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongSerie)]
            public String aSerie;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongFecha)]
            public String aFecha;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodigoCteProv;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodigoAgente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongReferencia)]
            public String aReferencia;
            public int aAfecta;
            public int aGasto1;
            public int aGasto2;
            public int aGasto3;
        }
        //    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        //public struct tMovimiento
        //{
        //    public int aConsecutivo;
        //    public Double aUnidades;
        //    public Double aPrecio;
        //    public Double aCosto;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
        //    public string aCodProdSer;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
        //    public string aCodAlmacen;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongReferencia)]
        //    public string aReferencia;
        //    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
        //    public string aCodClasificacion;
        //}
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tMovimiento
        {
            public int aConsecutivo;
            public Double aUnidades;
            public Double aPrecio;
            public Double aCosto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodProdSer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodAlmacen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongReferencia)]
            public String aReferencia;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String aCodClasificacion;
        }


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tCteProv
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String cCodigoCliente;//[ kLongCodigo + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongNombre)]
            public String cRazonSocial;//[ kLongNombre + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongFecha)]
            public String cFechaAlta;//[ kLongFecha + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongRFC)]
            public String cRFC;//[ kLongRFC + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCURP)]
            public String cCURP;//[ kLongCURP + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDenComercial)]
            public String cDenComercial;//[ kLongDenComercial + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongRepLegal)]
            public String cRepLegal;//[ kLongRepLegal + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongNombre)]
            public String cNombreMoneda;//[ kLongNombre + 1 ];
            public int cListaPreciosCliente;
            public double cDescuentoMovto;
            public int cBanVentaCredito; // 0 = No se permite venta a crédito, 1 = Se permite venta a crédito
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente1;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente2;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente3;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente4;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente5;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionCliente6;//[ kLongCodValorClasif + 1 ];
            public int cTipoCliente; // 1 - Cliente, 2 - Cliente/Proveedor, 3 - Proveedor
            public int cEstatus; // 0. Inactivo, 1. Activo
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongFecha)]
            public String cFechaBaja;//[ kLongFecha + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongFecha)]
            public String cFechaUltimaRevision;//[ kLongFecha + 1 ];
            public double cLimiteCreditoCliente;
            public int cDiasCreditoCliente;
            public int cBanExcederCredito; // 0 = No se permite exceder crédito, 1 = Se permite exceder el crédito
            public double cDescuentoProntoPago;
            public int cDiasProntoPago;
            double cInteresMoratorio;
            public int cDiaPago;
            public int cDiasRevision;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDesCorta)]
            public String cMensajeria;//[ kLongDesCorta + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public String cCuentaMensajeria;//[ kLongDescripcion + 1 ];
            public int cDiasEmbarqueCliente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String cCodigoAlmacen;//[ kLongCodigo + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String cCodigoAgenteVenta;//[ kLongCodigo + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public String cCodigoAgenteCobro;//[ kLongCodigo + 1 ];
            public int cRestriccionAgente;
            public double cImpuesto1;
            public double cImpuesto2;
            public double cImpuesto3;
            public double cRetencionCliente1;
            public double cRetencionCliente2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor1;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor2;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor3;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor4;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor5;//[ kLongCodValorClasif + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodValorClasif)]
            public String cCodigoValorClasificacionProveedor6;//[ kLongCodValorClasif + 1 ];
            public double cLimiteCreditoProveedor;
            public int cDiasCreditoProveedor;
            public int cTiempoEntrega;
            public int cDiasEmbarqueProveedor;
            public double cImpuestoProveedor1;
            public double cImpuestoProveedor2;
            public double cImpuestoProveedor3;
            public double cRetencionProveedor1;
            public double cRetencionProveedor2;
            public int cBanInteresMoratorio; // 0 = No se le calculan intereses moratorios al cliente, 1 = Si se le calculan intereses moratorios al cliente.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTextoExtra)]
            public String cTextoExtra1;//[ kLongTextoExtra + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTextoExtra)]
            public String cTextoExtra2;//[ kLongTextoExtra + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTextoExtra)]
            public String cTextoExtra3;//[ kLongTextoExtra + 1 ];
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTextoExtra)]
            public String cFechaExtra;//[ kLongFecha + 1 ];
            public double cImporteExtra1;
            public double cImporteExtra2;
            public double cImporteExtra3;
            public double cImporteExtra4;

        }
        public struct tDireccion
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigo)]
            public string cCodCteProv;
            public int cTipoCatalogo; // 1=Clientes y 2=Proveedores
            public int cTipoDireccion; // 1=Domicilio Fiscal, 2=Domicilio Envio
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cNombreCalle;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongNumeroExpandido)]
            public string cNumeroExterior;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongNumeroExpandido)]
            public string cNumeroInterior;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cColonia;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongCodigoPostal)]
            public string cCodigoPostal;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTelefono)]
            public string cTelefono1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTelefono)]
            public string cTelefono2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTelefono)]
            public string cTelefono3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongTelefono)]
            public string cTelefono4;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongEmailWeb)]
            public string cEmail;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongEmailWeb)]
            public string cDireccionWeb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cCiudad;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cEstado;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cMunicipio;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cPais;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = kLongDescripcion)]
            public string cTextoExtra;
        }
        
    }
    //clase de dato que ene el documento se marca como abstrctopara isnercion de documentos




}
