using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace ServicioVeritas
{
    class InserionesSDKContpaq
    {


        string mensaje = "";
        public int lError;
        // nombvre del páquete que se va aejecutar
        string lNombrePAQ = "";
        // llave del sistema  
        string sLlaveSis = "";
        int id_Documento=0;
        double FolioDocumento = 0;

        //---------------------------------------------------
        SDK.tMovimiento movimiento = new SDK.tMovimiento();
        int id_Movimiento = 0;

        //public void AgregaMovimiento(int idDocumento)
        //{
        //    // movimiento.aCodAlmacen = "";
        //    movimiento.aCodAlmacen = "1";
        //    movimiento.aCodProdSer = "D1";
        //    movimiento.aConsecutivo = 1;
        //    movimiento.aPrecio = 100;
        //    movimiento.aUnidades = 1;

        //    sLlaveSis = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Computación en Acción, SA CV\CONTPAQ I COMERCIAL";
        //    object lRutaBinarios = Registry.GetValue(sLlaveSis, "DIRECTORIOBASE", null);
         
        //    lNombrePAQ = "CONTPAQ I COMERCIAL";


        //    SDK.SetCurrentDirectory(lRutaBinarios.ToString());
        //    SDK.fInicioSesionSDK("SUPERVISOR", "");//iniciamos Secion
        //    lError = SDK.fSetNombrePAQ(lNombrePAQ);
        //    if (lError != 0)
        //    {
        //        mensaje = SDK.rError(lError);

        //    }
        //    else
        //    {
        //        mensaje = "Se inicio el SDK";
        //    }
           

        //    //abrirmos Empresas
        //    lError = SDK.fAbreEmpresa(@"\\ITSERVER\Compac\Empresas\adPRUEBAS");
        //    if (lError != 0)
        //    {
        //        mensaje = mensaje + " " + SDK.rError(lError);
        //        //
        //    }
        //    else
        //    {
        //        mensaje = mensaje + "SE abrio Empresa";
        //    }
        //    // agregamosMoviimiento
        //    int idMto = 0;
        //    lError = SDK.fAltaMovimiento(1, ref idMto, ref movimiento);
        //    if (lError != 0)
        //    {
        //        SDK.rError(lError);
        //        return;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Movimiento Creado");

        //    }

        //    SDK.fCierraEmpresa();

        //    MessageBox.Show(mensaje);
        //}
        
        public int AgregarDocumentoFT(ref SDK.tDocumento DOCUMENTO, List<clsDocumentoD1> ARRAYD1Fila,ref string referenciamensaje,string CMETODOPAG,string CUSOCFDI, string CTEXTOEXTRA2,string RFC,string CCANTPARCI)
         {
           
            try
            {
                
               
                sLlaveSis = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Computación en Acción, SA CV\CONTPAQ I COMERCIAL";
                object lRutaBinarios = Registry.GetValue(sLlaveSis, "DIRECTORIOBASE", null);
                //lUUID = new string(Convert.ToChar(" "), SDK.kLongitudUUID);
                lNombrePAQ = "CONTPAQ I COMERCIAL";
                string Clave = "";
                Byte lLicencia = 1;

                string lUUID = "";

                SDK.SetCurrentDirectory(lRutaBinarios.ToString());
                SDK.fInicioSesionSDK("SUPERVISOR", "Acesa$2017");
                lError = SDK.fSetNombrePAQ(lNombrePAQ);
                if (lError != 0)
                {
                    mensaje = SDK.rError(lError);

                }
                else
                {
                    mensaje = "Se inicio el SDK";
                }

                SDK.fInicioSesionSDKCONTPAQi("SUPERVISOR", "");
                //abrirmos Empresas 
                lError = SDK.fAbreEmpresa(@"D:\Compac\Empresas\adBureau_Veritas");
                if (lError != 0)
                {
                    mensaje =  SDK.rError(lError);
                    //
                    
                }
                else
                {
                    mensaje =  "SE abrio Empresa";
                }

                Clave = "12345678a";
                //obtener el siguiente  folio de la aplicacion

                //lError = SDK.fSiguienteFolio(DOCUMENTO.aCodConcepto,DOCUMENTO.aSerie, ref FolioDocumento);
                //if (lError != 0)
                //{
                //    mensaje = mensaje + " " + SDK.rError(lError);
                //    //
                //}
                //else
                //{
                //    mensaje = mensaje + "FolioObtenido Exitosamente:" +DOCUMENTO.aFolio;
                //}
               // id_Documento = Convert.ToInt32(FolioDocumento);
                //DOCUMENTO.aFolio = FolioDocumento;
                int dato = 0;
                //damos de alta el documentop

                lError = SDK.fBuscarDocumento(DOCUMENTO.aCodConcepto, DOCUMENTO.aSerie, DOCUMENTO.aFolio.ToString());
                //if (id_Documento != 0)
                if (lError == 0)
                {
                    mensaje = "Documento ya existente";
                }
                else
                {
                    if (DOCUMENTO.aFolio != 0)
                    {
                        lError = SDK.fAltaDocumento(ref dato, ref DOCUMENTO);
                        if (lError != 0)
                        {
                            mensaje =  SDK.rError(lError);
                            //no seas mamon si existe el cliente
                        }
                        else
                        {
                            if (DOCUMENTO.aSerie == "T")
                            {
                                lError = SDK.fSetDatoDocumento("CCANTPARCI", "2");
                            }
                            else
                            {
                                lError = SDK.fSetDatoDocumento("CCANTPARCI", "1");
                            }
                            SDK.fSetDatoDocumento("CTIPOCAMBIO", DOCUMENTO.aTipoCambio.ToString());
                            SDK.fSetDatoDocumento("CMETODOPAG", CMETODOPAG);
                            SDK.fSetDatoDocumento("CUSOCFDI", CUSOCFDI);
                            //SDK.fSetDatoDocumento("CTEXTOEXTRA2", CTEXTOEXTRA2);
                            SDK.fSetDatoDocumento("CCANTPARCI", CCANTPARCI);

                            if (RFC.Equals("XEXX010101000"))
                            {
                                SDK.fSetDatoDocumento("CIMPUESTO1", "0.00");
                            }
                            else
                            {
                               // SDK.fSetDatoDocumento("CIMPUESTO1", "16.00");
                            }

                            //Si no tenemos Error Al insertar el Documento  
                            //recorrer eestructura de Moviemientos
                            foreach (clsDocumentoD1 objetoo in ARRAYD1Fila)
                            {
                                movimiento.aCodAlmacen = "1";
                                movimiento.aConsecutivo = 1;
                                //cambiar este dato por una constante

                                if (objetoo.Descripcion_Prueba.Contains("AUDIT FEE"))
                                {
                                    movimiento.aCodProdSer = "84111600";
                                }
                                else
                                {
                                    if (objetoo.Descripcion_Prueba.Contains("ASSESSMENT FEE"))
                                    {
                                        movimiento.aCodProdSer = "78141600";
                                    }
                                    else
                                    {
                                        if (objetoo.Descripcion_Prueba.Contains("GASTOS DE RECOLECCION"))
                                        {
                                            movimiento.aCodProdSer = "01010101G";
                                        }
                                        else
                                        {
                                            if (objetoo.Descripcion_Prueba.Contains("COSTO HOJA DE CATALOGO"))
                                            {
                                                movimiento.aCodProdSer = "01010101C";
                                            }
                                            else
                                            {
                                                movimiento.aCodProdSer = "81101703";
                                            }
                                        }
                                    }
                                }

                                //movimiento.aCodProdSer = objetoo.CampoVacio1;
                                movimiento.aUnidades = Convert.ToDouble(objetoo.Cantidad);
                                movimiento.aPrecio = Convert.ToDouble(objetoo.PrecioUnitario);
                                int idMto = 0;
                                lError = SDK.fAltaMovimiento(dato, ref idMto, ref movimiento);
                                if (lError != 0)
                                {
                                    mensaje = SDK.rError(lError);
                                }
                                else
                                {
                                    string observac = "";
                                   // string descuentometer = ((Convert.ToDouble(objetoo.PrecioUnitario) * Convert.ToDouble(objetoo.Descuento)) / 100).ToString();
                                    lError = SDK.fSetDatoMovimiento("COBSERVAMOV", objetoo.Descripcion_Prueba);
                                    lError = SDK.fSetDatoMovimiento("CPORCENTAJEDESCUENTO1", objetoo.Descuento);

                                    // lError = SDK.fSetDatoMovimiento("CIMPUESTO1",objetoo.CargoExtra);
                                    //
                                    if (lError != 0)
                                    {
                                        mensaje = SDK.rError(lError);
                                    }
                                    else
                                    {
                                        lError = SDK.fGuardaMovimiento();
                                        if (lError != 0)
                                        {
                                            mensaje =  SDK.rError(lError);
                                        }
                                        else
                                        {
                                            if (objetoo.CargoExtra != "0.00")
                                            {
                                                idMto = 0;
                                                SDK.tMovimiento movcargo = new SDK.tMovimiento();

                                                //asignacion de datos a a modificacion 26/05/2018
                                                movcargo.aCodAlmacen = movimiento.aCodAlmacen;
                                                movcargo.aConsecutivo = movimiento.aConsecutivo;
                                                movcargo.aUnidades = movimiento.aUnidades;
                                                //agregar validadcion sobre el codigo del  prodicto o servicio 
                                                //000001 

                                                if (objetoo.CargoExtra.Equals("100.00"))
                                                {
                                                    movcargo.aPrecio = movimiento.aPrecio;
                                                    movcargo.aCodProdSer = "000003";
                                                }
                                                else
                                                {
                                                    movcargo.aCodProdSer = "000001";
                                                    movcargo.aPrecio = (Convert.ToDouble(objetoo.PrecioUnitario) * Convert.ToDouble(objetoo.CargoExtra)) / 100;
                                                }

                                                
                                                
                                                //movcargo.aPrecio = (Convert.ToDouble(objetoo.PrecioUnitario) * Convert.ToDouble(objetoo.CargoExtra)) / 100;
                                                lError = SDK.fAltaMovimiento(dato, ref idMto, ref movcargo);
                                                if (lError != 0)
                                                {
                                                    mensaje =  SDK.rError(lError);
                                                }
                                                else
                                                {
                                                    //lError = SDK.fSetDatoMovimiento("COBSERVAMOV", objetoo.Descripcion_Prueba);
                                                    lError = SDK.fGuardaMovimiento();
                                                    if (lError != 0)
                                                    {
                                                        mensaje = SDK.rError(lError);
                                                    }
                                                }
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                    mensaje = mensaje + "|" + "Movimiento Creado  |";
                                    id_Documento = 10;
                                }

                            }
                        }
                    }
                }
//codigo para Timprar el documento
                //Provamos emitir documento
                //lError = SDK.fEmitirDocumento(DOCUMENTO.aCodConcepto, DOCUMENTO.aSerie, DOCUMENTO.aFolio, "12345678a", "");
                //if (lError != 0)
                //{
                //    mensaje = mensaje + " " + SDK.rError(lError);
                //}
                //else
                //{
                //    mensaje = mensaje + "|" + " XML Timbrado";
                //}

                //// Generando los PDF En disco de TEESTING 
                //lError = SDK.fEntregEnDiscoXML(DOCUMENTO.aCodConcepto, DOCUMENTO.aSerie, DOCUMENTO.aFolio, 1, "");
                //if (lError != 0)
                //{
                //    mensaje = mensaje + " " + SDK.rError(lError);
                //}
                //else
                //{
                //    mensaje = mensaje + "|" + " PDF Generado";
                //}

                //// Entrega Documento en XML
                //lError = SDK.fEntregEnDiscoXML(DOCUMENTO.aCodConcepto, DOCUMENTO.aSerie, DOCUMENTO.aFolio, 0, "");
                //if (lError != 0)
                //{
                //    mensaje = mensaje + " " + SDK.rError(lError);
                //}
                //else
                //{
                //    mensaje = mensaje + "|" + " XML Generado";
                //}

                clsConst.saveLog(mensaje, "");
                referenciamensaje = mensaje;
                SDK.fCierraEmpresa();
                SDK.fTerminaSDK();
                return id_Documento;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");

                return 0;
            }
        }

        public void CambiaConceptoMOV()
        {
            try
            {
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");

            }
        }



        public string  AgregaCliente(string cCodigoCliente,string cRazonSocial,string cRFC,string s,SDK.tDireccion lDireccion)
        {
            try
            {
                string CodProd = string.Empty;
                sLlaveSis = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Computación en Acción, SA CV\CONTPAQ I COMERCIAL";
                object lRutaBinarios = Registry.GetValue(sLlaveSis, "DIRECTORIOBASE", null);
                //lUUID = new string(Convert.ToChar(" "), SDK.kLongitudUUID);
                lNombrePAQ = "CONTPAQ I COMERCIAL";
                string Clave = "";
                Byte lLicencia = 1;

                string lUUID = "";

                SDK.SetCurrentDirectory(lRutaBinarios.ToString());
                SDK.fInicioSesionSDK("SUPERVISOR", "Acesa$2017");
                lError = SDK.fSetNombrePAQ(lNombrePAQ);
                if (lError != 0)
                {
                    mensaje = SDK.rError(lError);

                }
                else
                {
                    mensaje = "Se inicio el SDK";
                }

                SDK.fInicioSesionSDKCONTPAQi("SUPERVISOR", "");
                //abrirmos Empresas 
                lError = SDK.fAbreEmpresa(@"D:\Compac\Empresas\adBureau_Veritas");
                if (lError != 0)
                {
                    mensaje = mensaje + " " + SDK.rError(lError);
                    //

                }
                else
                {
                    mensaje = mensaje + "SE abrio Empresa";
                }

                Clave = "12345678a";

                SDK.tCteProv lCteProv = new SDK.tCteProv();
               //  SDK.tDireccion lDireccion = new SDK.tDireccion();

                lCteProv.cCodigoCliente = cCodigoCliente;
                lCteProv.cRazonSocial = cRazonSocial;
                lCteProv.cNombreMoneda = "Peso Mexicano";

                lCteProv.cRFC = cRFC;
                lCteProv.cTipoCliente = 1;
                int idCliente = 0;



                lError = SDK.fAltaCteProv(ref idCliente, ref lCteProv);
                if (lError != 0)
                {
                    SDK.rError(lError);

                }
                else
                {
                    SDK.fEditaCteProv();
                    SDK.fSetDatoCteProv("cUsoCFDI", "P01");
                    if (s == "T")
                    {
                        SDK.fSetDatoCteProv("CMETODOPAG", "99");
                        SDK.fSetDatoCteProv("CIMPUESTO1", "16");
                    }
                    if (s == "I")
                    {
                        SDK.fSetDatoCteProv("CMETODOPAG", "03");
                        SDK.fSetDatoCteProv("CIMPUESTO1", "16");
                    }
                    //SDK.fSetDatoCteProv("cUsoCFDI", "G03");
                    lError = SDK.fGuardaCteProv();
                    if (lError != 0)
                    {
                        SDK.rError(lError);
                    }
                    else
                    {
                        CodProd = "si lo hizo";
                    }


                    int tipoDir = 1;
                   
                    /// lDireccion.cCiudad = "Zapopan";
                    if (lDireccion.cPais == "MEXICO")
                    { lDireccion.cPais = "México"; }
                    //lDireccion.cCodigoPostal = "72400";
                    // lDireccion.cEstado = "Jalisco";
                    // lDireccion.cCodigoPostal = "45190";
                   //lDireccion.cCodCteProv = "CL004";
                   // lDireccion.cNombreCalle = "Calle";
                   // lDireccion.cNumeroExterior = "123";
                    lDireccion.cTipoDireccion = 1;
                    lDireccion.cTipoCatalogo = 1;
                    //lDireccion.cMunicipio = "Zapopan";




                    lError = SDK.fAltaDireccion(ref tipoDir, ref lDireccion);
                    if (lError != 0)
                    {
                        SDK.rError(lError);
                    }
                    else
                    {
                        //lError = SDK.fSetDatoDireccion("CNUMEROEXTERIOR","0");
                        SDK.fEditaDireccion();
                        SDK.fSetDatoDireccion("CPAIS",lDireccion.cPais);
                        //SDK.fSetDatoDireccion("CESTADO", lDireccion.cEstado.ToString());
                        SDK.fSetDatoDireccion("CCODIGOPOSTAL", lDireccion.cCodigoPostal);
                        //SDK.fSetDatoDireccion("CCOLONIA", lDireccion.cColonia.ToString());
                        SDK.fGuardaDireccion();
                        // MessageBox.Show("Direcciones guardadas"); 
                    }



                    //}

                }

                SDK.fCierraEmpresa();
                SDK.fTerminaSDK();
                return CodProd;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");

                return exe.Message;
            }
        }

    }


}
