using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace ServicioVeritas
{
    class clsDocumento
    {
        //clase pada el manejo de los documentos  aqui se requiere leer los archivos  u asignarlkos a una nueva matriz
        // Variable3s de tipo docuimento e interaccion de documento para a insercion de datos en contpaq
        SDK.tDocumento documentoParametro = new SDK.tDocumento();
        InserionesSDKContpaq insercion = new InserionesSDKContpaq();
       // SDK.tMovimiento movimiento = new SDK.tMovimiento();
        int id_Documento = 0;
        //-------------------******************************Termina  declaracion de variables SDK ****************************----------------
        string mensajeobteido=string.Empty;
        // inicia declaracion de variables para el manejo de archivos 
        string Cabecera;
        string RFC;
        string Calle;
        string numero;
        string Numnterior;
        string Col;
        string mun;
        string ciudad;
        string pais;
        string CodPostal;
        int ARchivoValido;
        clsADO aceso = new clsADO();
        List<List<string>> matriz = new List<List<string>>();
        clsDocumentoE0 PrimeraFila = new clsDocumentoE0();
        clsDocumentoE1 segundaFila = new clsDocumentoE1();
        clsDocumentoE3 terceraFila = new clsDocumentoE3();
        clsDocumentoE4 cuartaFila = new clsDocumentoE4();
        clsDocumentoE5 quintafila = new clsDocumentoE5();
        clsDocumentoE6 sextafila = new clsDocumentoE6();
        clsDocumentoEC15 septimaFila = new clsDocumentoEC15();
        clsDocumentoE8 novenaFila = new clsDocumentoE8();
        clsDocumentoE9 decimaFila = new clsDocumentoE9();
        clsDocumentoEC1 ultimaFila = new clsDocumentoEC1();

        List<clsDocumentoD1> ARRAYD1Fila = new List<clsDocumentoD1>();
        //listado de Movimientos D1 para insertar dentro del Documento
        int D1indice = 0;
        

        int seMovioArchivo = 0;
        //termina Declaracion de variables para manejo de archivos

        public  int LeeeArchivo(string nombreArchivo,DateTime fechaArchivo)
        {
            string direccion;
            int regresa = 0;
            seMovioArchivo = 0;
            //Definimos una lista de lista de cadena, de está manera, como no sabemos
            //las longitudes de la matriz va a ser dinamica tanto para filas como para columnas
           
            direccion = @"D:\DirectorioVeritas\Testing\" + nombreArchivo;
            //Leemos todas las lineas del fichero con está función
            string[] strLineas = File.ReadAllLines(direccion);
            string[] campos;

            //Recorremos todas las lineas del fichero
            int i = 0;
            
            foreach (string linea in strLineas)
            {
                //Declaramos una lista para los campos de la linea concreta
                //que estamos recorriendo
                List<string> lineaMatriz = new List<string>();
                //Partimos la linea con el caracter separador ";" indicado
                campos = linea.Split("|".ToCharArray());
                //Agregamos todos los campos obtenidos al partir la linea a la 
                //fila de la matriz
                lineaMatriz.AddRange(campos.ToList());
                //agregamos a la matriz la fila leida.
                matriz.Add(lineaMatriz);
               // MessageBox.Show(" " + matriz.Count.ToString());
                i++;
               
            }
            int Filas = matriz.Count;
            int columnas = 0;
            string Premensaje = prevalidaARchivo(Filas);
            if (Premensaje != "")
            {
                MueveArchivo(nombreArchivo);
                aceso.InsertaErrorServicioP(Premensaje +" EN ARCHIVO  "+ nombreArchivo,nombreArchivo,fechaArchivo);
                enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " El proceso  de lectura se detendra hasta la correccion de este problema. " +Premensaje);

                seMovioArchivo = 1;
            }
            else
            {
               ARRAYD1Fila.Clear();
                for (int o = 0; o < Filas; o++)
                {
                    if (seMovioArchivo == 1)
                    {
                        return seMovioArchivo;
                    }
                    else
                    {

                        regresa = insertaEntablaBD(matriz[o][0].ToString(), columnas, nombreArchivo, o, fechaArchivo);

                    }
                    //columnas = matriz[o].Count;
                    //MessageBox.Show(matriz[o].Count.ToString());
                    //for (int z = 0; z < columnas; z++)
                    //{
                    //   // MessageBox.Show(" "+matriz[o][z]);
                    //}
                }



                id_Documento= DocumentoContpaq(fechaArchivo);

                if (id_Documento != 0)
                {
                    ARRAYD1Fila.Clear();
                    matriz.Clear();
                    mensajeobteido = string.Empty;
                }
                else
                {
                    MueveArchivo(nombreArchivo);
                    aceso.InsertaErrorServicioP("ERROR AL PROCESAR CONTPAQ" + " EN ARCHIVO  " + nombreArchivo, nombreArchivo, fechaArchivo);
                    enviaEmailError("ERROR AL PROCESAR CONTPAQ " + "EN ARCHIVO  " + nombreArchivo+" "+nombreArchivo+" "+ fechaArchivo +" " +mensajeobteido);
                    seMovioArchivo = 1;
                }
                
                //foreach (clsDocumentoD1 objetoo in ARRAYD1Fila)
                //{
                //    movimiento.aCodAlmacen = "1";
                //    movimiento.aConsecutivo = contaddor;
                //    movimiento.aCodProdSer = objetoo.NumeroReporte.ToString();
                //    movimiento.aUnidades =Convert.ToDouble( objetoo.Cantidad);
                //    movimiento.aPrecio = Convert.ToDouble(objetoo.PrecioUnitario);
                //    insercion.AgregaMovimiento(id_Documento);
                //    contaddor++;
                //}
                // despues de insertar  los datos de documento insertar movimientos en contpaq
            }
            return regresa;
        }
       
        public string prevalidaARchivo(int Filas)
        {
           
            string Error = "";
            int columnasVallidado = 0;
            string Texto = "";
            for (int o = 0; o < Filas; o++)
            {

                Texto = matriz[o][0].ToString();
                if (seMovioArchivo == 0)
                {
                    if (Texto.Equals("E0"))
                    {
                        columnasVallidado = matriz[o].Count;
                        if (columnasVallidado == 14)
                        {
                           
                        }
                        else
                        {
                            Error = "ERROR EN E0 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                        }
                    }
                    else
                    {
                        if (Texto.Equals("E1"))
                        {
                            columnasVallidado = matriz[o].Count;
                            if (columnasVallidado == 16)
                            {
                               
                            }
                            else
                            {
                                Error = "ERROR EN E1 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                            }

                            //es de factura
                        }
                        else
                        {
                            if (Texto.Equals("E3"))
                            {

                                columnasVallidado = matriz[o].Count;
                                if (columnasVallidado == 4)
                                {
                                   
                                }
                                else
                                {
                                    Error = "ERROR EN E3 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                }


                            }
                            else
                            {
                                if (Texto.Equals("E4"))
                                {
                                    //Datps de Cliente aparentemente


                                    columnasVallidado = matriz[o].Count;
                                    if (columnasVallidado == 4)
                                    {
                                      
                                    }
                                    else
                                    {
                                        Error = "ERROR EN E4 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                    }

                                }
                                else
                                {
                                    if (Texto.Equals("E5"))
                                    {
                                        columnasVallidado = matriz[o].Count;
                                        if (columnasVallidado == 12)
                                        {
                                           
                                        }
                                        else
                                        {
                                            Error = "ERROR EN E5 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                        }
                                        //son datos complementarios del cliente
                                    }
                                    else
                                    {
                                        if (Texto.Equals("E6"))
                                        {
                                            columnasVallidado = matriz[o].Count;
                                            if (columnasVallidado == 3)
                                            {
                                               
                                            }
                                            else
                                            {
                                                Error = "ERROR EN E6 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                            }
                                        }
                                        else
                                        {
                                            if (Texto.Equals("EC15"))
                                            {

                                                columnasVallidado = matriz[o].Count;
                                                if (columnasVallidado == 3)
                                                {
                                                    
                                                }
                                                else
                                                {
                                                    Error = "ERROR EN EC15 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                }
                                            }
                                            else
                                            {
                                                if (Texto.Equals("D1"))
                                                {
                                                    columnasVallidado = matriz[o].Count;
                                                    if (columnasVallidado == 12)
                                                    {
                                                       
                                                    }
                                                    else
                                                    {
                                                        Error = "ERROR EN D1 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                    }

                                                    //es movimiento 
                                                }
                                                else
                                                {
                                                    if (Texto.Equals("E8"))
                                                    {

                                                        columnasVallidado = matriz[o].Count;
                                                        if (columnasVallidado == 7)
                                                        {
                                                           
                                                            
                                                        }
                                                        else
                                                        {

                                                            Error = "ERROR EN E8 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Texto.Equals("E9"))
                                                        {
                                                            columnasVallidado = matriz[o].Count;
                                                            if (columnasVallidado == 4)
                                                            {
                                                                
                                                            }
                                                            else
                                                            {

                                                                Error = "ERROR EN E9 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (Texto.Equals("EC1"))
                                                            {
                                                                columnasVallidado = matriz[o].Count;
                                                                if (columnasVallidado == 4)
                                                                {

                                                                }
                                                                else
                                                                {

                                                                    Error = "ERROR EN EC1 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (Texto.Equals("E302"))
                                                                {
                                                                    columnasVallidado = matriz[o].Count;
                                                                    if (columnasVallidado == 5)
                                                                    {

                                                                    }
                                                                    else
                                                                    {

                                                                        Error = "ERROR EN E302 EL FORMATO DE LA  FILA NO ACORDE A LO ESTABLECIDO";
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
            return Error;   
        }

        public int insertaEntablaBD(string Texto,int columnas,string nombreArchivo, int o,DateTime fecha )
        {
            try
            {
                if (seMovioArchivo == 0)
                {
                    if (Texto.Equals("E0"))
                    {
                        // es cabexcera inserta en cabecera
                       
                        PrimeraFila.nombreArchivo = nombreArchivo;
                        columnas = matriz[o].Count;
                        if (columnas == 14)
                        {
                            PrimeraFila.Cabecera = matriz[o][0].ToString(); ;
                            PrimeraFila.campovacio1 = matriz[o][1].ToString();
                            PrimeraFila.nombreEmpresa = matriz[o][2].ToString();
                            PrimeraFila.RFC = matriz[o][3].ToString();
                            PrimeraFila.Calle = matriz[o][4].ToString();
                            PrimeraFila.NumInterior = matriz[o][5].ToString();
                            PrimeraFila.NumExterior = matriz[o][6].ToString();
                            PrimeraFila.Colonia = matriz[o][7].ToString();
                            PrimeraFila.Municipio = matriz[o][8].ToString();
                            PrimeraFila.CampoVacio2 = matriz[o][9].ToString();
                            PrimeraFila.Estado = matriz[o][10].ToString();
                            PrimeraFila.Pais = matriz[o][11].ToString();
                            PrimeraFila.CodigoPostal = matriz[o][12].ToString();
                            // aceso.insertaDocumentoCabeza(PrimeraFila);

                            //llamar a metodo para insertar datos completos
                           // aceso.insertaDocumentoE0(PrimeraFila,fecha);

                        }
                        else
                        {
                            MueveArchivo(nombreArchivo);
                            aceso.InsertaErrorServicioP("ERROR DETECTADO FILA: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                            enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       "EL ARCHIVO TIENE UN ERROR EN LA FILA E0 VALIDE .");

                            seMovioArchivo = 1;
                        }
                    }
                    else
                    {
                        if (Texto.Equals("E1"))
                        {

                           
                            segundaFila.nombreArchivo = nombreArchivo;
                            columnas = matriz[o].Count;
                            if (columnas == 16)
                            {
                                segundaFila.cabecera = matriz[o][0].ToString();
                                segundaFila.folioFactura = matriz[o][1].ToString();
                                segundaFila.fecha = Convert.ToDateTime(matriz[o][2]);
                                segundaFila.FormadePago = matriz[o][3].ToString();
                                segundaFila.campovacio1 = matriz[o][4].ToString();
                                segundaFila.campovacio2 = matriz[o][5].ToString();
                                segundaFila.totalFactura = Convert.ToDouble(matriz[o][6].ToString());
                                segundaFila.subTotal = Convert.ToDouble(matriz[o][7].ToString());
                                segundaFila.campovacio3 = matriz[o][8].ToString();
                                segundaFila.campovacio4 = matriz[o][9].ToString();
                                segundaFila.numerico1 = matriz[o][10].ToString();
                                segundaFila.numerico2 = matriz[o][11].ToString();
                                segundaFila.metodoDePago = matriz[o][12].ToString();
                                segundaFila.EstadoPais = matriz[o][13].ToString();
                                segundaFila.ReferenciaCliente = matriz[o][14].ToString();
                                // llamar metodo de base de datos para insertar seguinda fila 
                               // aceso.insertaDocumentoE1(segundaFila, fecha);

                            }
                            else
                            {
                                MueveArchivo(nombreArchivo);
                                aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                seMovioArchivo = 1;
                            }

                            //es de factura
                        }
                        else
                        {
                            if (Texto.Equals("E3"))
                            {
                                // regularmente vacios pero verificar 
                              
                                terceraFila.nombrearchivo = nombreArchivo;
                                columnas = matriz[o].Count;
                                if (columnas == 4)
                                {
                                    terceraFila.cabezera = matriz[o][0].ToString();
                                    terceraFila.campovacio1 = matriz[o][1].ToString();
                                    terceraFila.campovacio2 = matriz[o][2].ToString();
                                    //evaluamos e insertamos tercera fila se hace asi apra poder insertar cuantas E numero exitan
                                        

                                }
                                else
                                {
                                    MueveArchivo(nombreArchivo);
                                    aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                    enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                    seMovioArchivo = 1;
                                }


                            }
                            else
                            {
                                if (Texto.Equals("E4"))
                                {
                                    //Datps de Cliente aparentemente
                                   
                                    cuartaFila.nombreArchivo = nombreArchivo;
                                    columnas = matriz[o].Count;
                                    if (columnas == 4)
                                    {
                                        cuartaFila.cabecera = matriz[o][0].ToString();
                                        cuartaFila.RFC = matriz[o][1].ToString();
                                        cuartaFila.RazonSocialCliente = matriz[o][2].ToString();
                                    }
                                    else
                                    {
                                        MueveArchivo(nombreArchivo);
                                        aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                        enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                        seMovioArchivo = 1;
                                    }

                                }
                                else
                                {
                                    if (Texto.Equals("E5"))
                                    {
                                        
                                        quintafila.nombrearchibo = nombreArchivo;
                                        columnas = matriz[o].Count;
                                        if (columnas == 12)
                                        {
                                            quintafila.cabecera = matriz[o][0].ToString();
                                            quintafila.calle = matriz[o][1].ToString();
                                            quintafila.Numinterior = matriz[o][2].ToString();
                                            quintafila.NumExterior = matriz[o][3].ToString();
                                            quintafila.Colonia = matriz[o][4].ToString();
                                            quintafila.Campovacio1 = matriz[o][5].ToString();
                                            quintafila.Municipio = matriz[o][6].ToString();
                                            quintafila.Estado = matriz[o][7].ToString();
                                            quintafila.Pais = matriz[o][8].ToString();
                                            quintafila.CodigoPostal = matriz[o][9].ToString();
                                            quintafila.EmailCliente = matriz[o][10].ToString();

                                        }
                                        else
                                        {
                                            MueveArchivo(nombreArchivo);
                                            aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                            enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                            seMovioArchivo = 1;
                                        }
                                        //son datos complementarios del cliente
                                    }
                                    else
                                    {
                                        if (Texto.Equals("E6"))
                                        {
                                            
                                            sextafila.nombreArchivo = nombreArchivo;
                                            columnas = matriz[o].Count;
                                            if (columnas == 3)
                                            {
                                                sextafila.cabecera = matriz[o][0].ToString();
                                                sextafila.nombre_Cuenta = matriz[o][1].ToString();

                                            }
                                            else
                                            {
                                                MueveArchivo(nombreArchivo);
                                                aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                                seMovioArchivo = 1;
                                            }
                                        }
                                        else
                                        {
                                            if (Texto.Equals("EC15"))
                                            {
                                                //total con letra y moneda
                                               
                                                septimaFila.nombreArchivo = nombreArchivo;
                                                columnas = matriz[o].Count;
                                                if (columnas == 3)
                                                {
                                                    septimaFila.cabecera = matriz[o][0].ToString();
                                                    septimaFila.totalPyL = matriz[o][1].ToString();
                                                }
                                                else
                                                {
                                                    MueveArchivo(nombreArchivo);
                                                    aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                    enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                                    seMovioArchivo = 1;
                                                }
                                            }
                                            else
                                            {
                                                if (Texto.Equals("D1"))
                                                {

                                                    clsDocumentoD1 ProductoNuevo = new clsDocumentoD1();
                                                    ProductoNuevo.nombreArchivo = nombreArchivo;
                                                    columnas = matriz[o].Count;
                                                    if (columnas == 12)
                                                    {
                                                        ProductoNuevo.cabecera = matriz[o][0].ToString();
                                                        ProductoNuevo.CampoVacio1 = matriz[o][1].ToString();
                                                        ProductoNuevo.NumeroReporte = matriz[o][2].ToString();
                                                        ProductoNuevo.Descripcion_Prueba =sextafila.nombre_Cuenta+" "+ matriz[o][2].ToString()+ " "+matriz[o][3].ToString();
                                                        ProductoNuevo.CampoVacio2 = matriz[o][4].ToString();
                                                        ProductoNuevo.Cantidad = matriz[o][5].ToString();
                                                        ProductoNuevo.PrecioUnitario = matriz[o][6].ToString();
                                                        ProductoNuevo.PrecioSubTotal = matriz[o][7].ToString();
                                                        ProductoNuevo.TotalCDoCar = matriz[o][8].ToString();
                                                        ProductoNuevo.Descuento = matriz[o][9].ToString();
                                                        ProductoNuevo.CargoExtra = matriz[o][10].ToString();

                                                        ARRAYD1Fila.Add(ProductoNuevo);

                                                    }
                                                    else
                                                    {
                                                        MueveArchivo(nombreArchivo);
                                                        aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                        enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                                        seMovioArchivo = 1;
                                                    }

                                                    //es movimiento 
                                                }
                                                else
                                                {
                                                    if (Texto.Equals("E8"))
                                                    {
                                                        // es moneda
                                                        
                                                        novenaFila.NombreArchivo = nombreArchivo;
                                                        columnas = matriz[o].Count;
                                                        if (columnas == 7)
                                                        {
                                                            novenaFila.cabecera = matriz[o][0].ToString();
                                                            novenaFila.CampoVacio1 = matriz[o][1].ToString();
                                                            novenaFila.monedaU = matriz[o][2].ToString();
                                                            novenaFila.TipoCambio = matriz[o][3].ToString();
                                                            novenaFila.campovacio2 = matriz[o][4].ToString();
                                                            novenaFila.campovacio3 = matriz[o][5].ToString();
                                                        }
                                                        else
                                                        {
                                                            MueveArchivo(nombreArchivo);
                                                            aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                            enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                                            seMovioArchivo = 1;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Texto.Equals("E9"))
                                                        {
                                                            //cuenta y contacto del cliente
                                                            
                                                            decimaFila.NombreArchivo = nombreArchivo;
                                                            columnas = matriz[o].Count;
                                                            if (columnas == 4)
                                                            {
                                                                decimaFila.cabecera = matriz[o][0].ToString();
                                                                decimaFila.CuentaXpagar = matriz[o][1].ToString();
                                                                decimaFila.ContactoCliente = matriz[o][2].ToString();

                                                            }
                                                            else
                                                            {
                                                                MueveArchivo(nombreArchivo);
                                                                aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                                enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                                                                seMovioArchivo = 1;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (Texto.Equals("EC1"))
                                                            {
                                                                
                                                                ultimaFila.NombreArchivo = nombreArchivo;
                                                                columnas = matriz[o].Count;
                                                                if (columnas == 4)
                                                                {
                                                                    ultimaFila.cabecera = matriz[o][0].ToString();
                                                                    ultimaFila.numerito = matriz[o][1].ToString();
                                                                    ultimaFila.nota = matriz[o][2].ToString();


                                                                   
                                                                }
                                                                else
                                                                {
                                                                    MueveArchivo(nombreArchivo);
                                                                    aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                                                                    enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + ""+
                                                                        " El proceso  de lectura se detendra hasta la correccion de este problema.");

                                                                    seMovioArchivo = 1;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (Texto.Equals("E302"))
                                                                {
                                                                    columnas = matriz[o].Count;
                                                                    if (columnas == 5)
                                                                    {

                                                                    }
                                                                    else
                                                                    {

                                                                       // seMovioArchivo = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    return seMovioArchivo;
                    //if() si se movio archivo != 1 ocea hace no se movio el archivo aqui no hay errores aparentes dentro de esta  parte del archivo generamos 
                    // metoido para poder insertar los datos del gato en el oso
                    //metodo inserta BD tanto documento como insertar datos de la bd si le pasamos fecha solo insertar partes independietnes la insercion del documento 
                    // se insertaria priemero
                }
                else
                {
                    seMovioArchivo = 1;
                   // aceso.InsertaErrorServicioP("error en la fila: " + o + " y columna "+ Texto);
                    return seMovioArchivo;
                }
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                // mover archivo a errores
                MueveArchivo(nombreArchivo);
                aceso.InsertaErrorServicioP("error en la fila: " + Texto + "EL ARCHIVO NO CUMPLE CON LOS REQUERIMIENTOS " + nombreArchivo, nombreArchivo, fecha);
                enviaEmailError("ERROR DETECTADO EN EL ARCHIVO: " + nombreArchivo + " EN LA FILA: " + o + "Y EN LA COLUMNA: " + Texto + "" +
                                                                       " El proceso  de lectura se detendra hasta la correccion de este problema.");
                seMovioArchivo = 1;
                return seMovioArchivo;
            }
        }
        public void MueveArchivo(string nombreArchivo)
        {
            if (File.Exists(@"D:\DirectorioVeritas\Testing\" + nombreArchivo))
            {

                if (!File.Exists(@"D:\DirectorioVeritas\CorregirTesting\" + nombreArchivo))
                {
                    File.Move(@"D:\DirectorioVeritas\Testing\" + nombreArchivo, @"D:\DirectorioVeritas\CorregirTesting\" + nombreArchivo);
                }
                else
                {
                    //MessageBox.Show("Ya existe el archivo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
               // MessageBox.Show("El archivo no fue encontrado en el directorio o fue movido previamente", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void enviaEmailError(string error)
        {
            
            // Crea el mensaje estableciendo quién lo manda y quién lo recibe
            SmtpClient cliente = new SmtpClient();
            MailMessage mensaje = new MailMessage();
            mensaje.To.Clear();
            mensaje.Body = "";
            mensaje.Subject = "";
            mensaje.Body = error;
            mensaje.Subject = "Lectura de archivo Erronea";
            mensaje.IsBodyHtml = true;
            mensaje.To.Add("sami.h.m101@gmail.com,miguel.cazarez@mx.bureauveritas.com");
            mensaje.From = new MailAddress("Facturacion.Mexico@mx.bureauveritas.com");

            //limpiamos destinatario cuerpo y etcv


            //Envía el mensaje.
            cliente.Host = "usbvrelay.bureauveritas.com";
            cliente.Port = 25;
            cliente.EnableSsl = false;
            //Añade credenciales si el servidor lo requiere.
            cliente.Credentials = new NetworkCredential("", "");
            
            cliente.Send(mensaje);
        }

        public int ExtraeIndice(string cadena)
        {
            int indice = 0;
            int contador = 0;
            if (cadena.Contains("/"))
            {

                indice = cadena.Count();
                if (indice != 0)
                {
                    while (cadena[contador].ToString() != "/")
                    {
                        contador = contador + 1;
                    }
                }
                return contador + 2;
            }
            else
            {
                aceso.InsertaErrorServicioP("No se encontró / en el Código del cliente, en la Fila E1 archivo:", cuartaFila.nombreArchivo, DateTime.Now);
                enviaEmailError("No se encontró / en el Código del cliente, en la Fila E1 archivo: " + cuartaFila.nombreArchivo);

                seMovioArchivo = 1;
                return 0;
            }

          
        }
        public int indiceCodigo(string codigo)
        {
            int indice = 0;
            int contador = 0;

            indice = codigo.Count();
            if (indice != 0)
            {

                while (contador != indice)
                {
                    if (contador == 0)
                    {
                        if (codigo[contador].ToString() == "1")
                        {
                            //SI EL PRIMERO NUMERO ES 1 PASA AL SIGUEINTE
                            contador++;
                        }
                        else
                        {
                            return contador;
                        }

                    }
                    else
                    {
                        //QUIERE DECIR QUE NO ES EL PRIMER REGISTRO 
                        if (codigo[contador].ToString() == "0")
                        {
                            contador++;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
            }
            return contador;
            // return contador + 2;

        }
        public int  DocumentoContpaq(DateTime fechaArchivo)
        {
            //insercion de datos recordar si es  Dato 301 es testing FActura de Testing 
            
            string CMETODOPAG = string.Empty, CUSOCFDI = string.Empty, CTEXTOEXTRA2 = string.Empty, CCANTPARCI = string.Empty, Moneda = string.Empty;
            if (novenaFila.monedaU == "USD")
            {
                documentoParametro.aNumMoneda = 2;
                documentoParametro.aTipoCambio = Convert.ToDouble(novenaFila.TipoCambio);
            }
            else
            {
                documentoParametro.aNumMoneda = 1;
                documentoParametro.aTipoCambio = 1;
            }
            //
            documentoParametro.aSistemaOrigen = 1;
            SDK.tDireccion DireccionCli = new SDK.tDireccion();
            //insercion de folio
            documentoParametro.aFolio = Convert.ToDouble(segundaFila.folioFactura);
            //termina folio
            //documentoParametro.aFecha = fechaArchivo.ToString("MM/dd/yyyy");
            documentoParametro.aFecha = segundaFila.fecha.ToString("MM/dd/yyyy");

            documentoParametro.aAfecta = 1;

            /****datos para testing forma de insertcion de datos testing */

            if (segundaFila.folioFactura[0].ToString().Equals("9"))
            {
                // es nmota de credito Testing 
                documentoParametro.aCodConcepto = "801";
                documentoParametro.aSerie = "B";
            }
                //verifgicacmos que sea factura Testing
                if (segundaFila.folioFactura[0].ToString().Equals("1"))
                {
                    documentoParametro.aCodConcepto = "401";
                    documentoParametro.aSerie = "F";
                }
                /*---------------termino de testing-----------------*/
                /*=======inicio de insercion de datos Inspecciones==========*/
            if (segundaFila.folioFactura[0].ToString().Equals("8"))
            {
                documentoParametro.aCodConcepto = "402";
                documentoParametro.aSerie = "I";
            }
            if (segundaFila.folioFactura[0].ToString().Equals("7"))
            {
                documentoParametro.aCodConcepto = "802";
                documentoParametro.aSerie = "C";
            }
            /*termina inspecciones*/

            
           int diagonalEn= ExtraeIndice(segundaFila.ReferenciaCliente);
            string nuevaCadenaCod = string.Empty;
            int ultimoEndiagon = segundaFila.ReferenciaCliente.Length - diagonalEn;
            nuevaCadenaCod = segundaFila.ReferenciaCliente.Substring(diagonalEn, ultimoEndiagon);
            int iniciaCodigo=indiceCodigo(nuevaCadenaCod);
            
            int ultimoIDcod = nuevaCadenaCod.Length - iniciaCodigo;
            string codigoCliente = nuevaCadenaCod.Substring(iniciaCodigo, ultimoIDcod);
            if (seMovioArchivo == 0)
            {
                if (cuartaFila.RFC.Count() == 19)
                {
                    //rfc Largo s d 13 

                    //documentoParametro.aCodigoCteProv = cuartaFila.RFC.Substring(6, 13);

                    string resultado = aceso.RecuperaCodCliProv(codigoCliente, cuartaFila.RazonSocialCliente);
                    RFC = cuartaFila.RFC.Substring(6, 13);
                    if (resultado != string.Empty)
                    {
                        if (resultado.Equals("-1"))
                        {
                            aceso.InsertaErrorServicioP("No se Encontro Cliente", cuartaFila.nombreArchivo, fechaArchivo);
                            enviaEmailError("No se encontro el cliente del documento " + cuartaFila.nombreArchivo);

                            seMovioArchivo = 1;
                        }
                        else
                        {
                            documentoParametro.aCodigoCteProv = resultado;
                            if (cuartaFila.RFC.Substring(6, 13) == "XEXX010101000")
                            {
                                documentoParametro.aNumMoneda = 2;
                            }
                        }
                    }
                    else
                    {
                        DireccionCli.cCiudad = quintafila.Colonia;
                        DireccionCli.cPais = quintafila.Pais;
                        DireccionCli.cEstado = quintafila.Estado;
                        DireccionCli.cCodigoPostal = quintafila.CodigoPostal;
                        DireccionCli.cNombreCalle = quintafila.calle;
                        DireccionCli.cNumeroExterior = "0";
                        DireccionCli.cMunicipio = quintafila.Municipio;
                        DireccionCli.cTipoDireccion = 1;
                        DireccionCli.cTipoCatalogo = 1;
                        int InicioIndice = ExtraeIndice(segundaFila.ReferenciaCliente);
                        DireccionCli.cCodCteProv = segundaFila.ReferenciaCliente.Substring(InicioIndice, 7);
                        if (insercion.AgregaCliente(segundaFila.ReferenciaCliente.Substring(InicioIndice, 7), cuartaFila.RazonSocialCliente, cuartaFila.RFC.Substring(6, 13), documentoParametro.aSerie, DireccionCli) != string.Empty)
                        {
                            documentoParametro.aCodigoCteProv = aceso.RecuperaCodCliProv(codigoCliente, cuartaFila.RazonSocialCliente);

                        }
                        else {
                            aceso.InsertaErrorServicioP("No se Encontro Cliente o ya existe", cuartaFila.nombreArchivo, fechaArchivo);
                            enviaEmailError("No se encontro el cliente del documento " + cuartaFila.nombreArchivo);

                            seMovioArchivo = 1;
                        }
                    }

                    //documentoParametro.aCodigoCteProv = "840615KKA";

                }
                else
                {
                    if (cuartaFila.RFC.Count() == 17)
                    {
                        //es corto el rfc es d 12
                        //documentoParametro.aCodigoCteProv = cuartaFila.RFC.Substring(5, 12);
                        //documentoParametro.aCodigoCteProv = "840615KKA";
                        string resultado = aceso.RecuperaCodCliProv(codigoCliente, cuartaFila.RazonSocialCliente);
                        RFC = cuartaFila.RFC.Substring(5, 12);
                        if (resultado != string.Empty)
                        {
                            if (resultado.Equals("-1"))
                            {
                                aceso.InsertaErrorServicioP("No se Encontro Cliente", cuartaFila.nombreArchivo, fechaArchivo);
                                enviaEmailError("No se encontro el cliente");

                                seMovioArchivo = 1;
                            }
                            else
                            {
                                //cambio realizado para prueba
                                if (resultado != "")
                                {
                                    documentoParametro.aCodigoCteProv = resultado;
                                    if (cuartaFila.RFC.Substring(5, 12) == "XEXX010101000")
                                    {
                                        documentoParametro.aNumMoneda = 2;

                                    }
                                }
                                else
                                {
                                    aceso.InsertaErrorServicioP("No se Encontro Cliente", cuartaFila.nombreArchivo, fechaArchivo);
                                    enviaEmailError("No se encontro el cliente");

                                    seMovioArchivo = 1;
                                }
                                //documentoParametro.aCodigoCteProv = "840615KKA";
                            }
                        }
                        else
                        {
                            DireccionCli.cCiudad = quintafila.Colonia;
                            DireccionCli.cPais = quintafila.Pais;
                            DireccionCli.cEstado = quintafila.Estado;
                            DireccionCli.cCodigoPostal = quintafila.CodigoPostal;
                            DireccionCli.cNombreCalle = quintafila.calle;
                            DireccionCli.cNumeroExterior = "0";
                            DireccionCli.cNumeroInterior = quintafila.Numinterior;
                            DireccionCli.cMunicipio = quintafila.Municipio;
                            DireccionCli.cTipoDireccion = 1;
                            DireccionCli.cTipoCatalogo = 1;
                            int InicioIndice = ExtraeIndice(segundaFila.ReferenciaCliente);
                            int Final = segundaFila.ReferenciaCliente.Length - InicioIndice;
                            DireccionCli.cCodCteProv = segundaFila.ReferenciaCliente.Substring(InicioIndice, Final);
                            if (insercion.AgregaCliente(segundaFila.ReferenciaCliente.Substring(InicioIndice, Final), cuartaFila.RazonSocialCliente, cuartaFila.RFC.Substring(5, 12), documentoParametro.aSerie, DireccionCli) != string.Empty)
                            {
                                documentoParametro.aCodigoCteProv = aceso.RecuperaCodCliProv(codigoCliente, cuartaFila.RazonSocialCliente);
                            }
                            else
                            {
                                aceso.InsertaErrorServicioP("No se Encontro Cliente o ya existe", cuartaFila.nombreArchivo, fechaArchivo);
                                enviaEmailError("No se encontro el cliente del documento " + cuartaFila.nombreArchivo);

                                seMovioArchivo = 1;
                            }
                        }


                        /// documentoParametro.aCodigoCteProv = "840615KKA";
                    }
                    else
                    {
                        aceso.InsertaErrorServicioP("Formato de E4 campo RFC incorrecto longitud adecuada 17 a 19 caracteres", cuartaFila.nombreArchivo, fechaArchivo);
                        enviaEmailError("Formato de E4 campo RFC incorrecto longitud adecuada 17 a 19 caracteres " + cuartaFila.nombreArchivo);

                        seMovioArchivo = 1;
                    }
                }
            }


            //antes de insertar verificar 3 datos del  cliente

            //metodo para insercion de datos del cliente

            SqlDataReader regreso = aceso.RecuperaParametrosCliente(documentoParametro.aCodigoCteProv);
            if (regreso.Read())
            {
                CMETODOPAG = regreso[0].ToString();
                CUSOCFDI = regreso[1].ToString();
                CTEXTOEXTRA2 = regreso[2].ToString();
                Moneda = regreso[3].ToString();
                if (CUSOCFDI == "p01")
                {
                    CUSOCFDI = "P01";
                }
                if (CTEXTOEXTRA2 == "pue" || CTEXTOEXTRA2 == "Pue" || CTEXTOEXTRA2 == "pUe" || CTEXTOEXTRA2 == "puE" || CTEXTOEXTRA2 == "PUe" || CTEXTOEXTRA2 == "pUE"
                    || CTEXTOEXTRA2 == "PuE")
                {
                    CTEXTOEXTRA2 = "PUE";
                }
                if (CTEXTOEXTRA2 == "PPD" || CTEXTOEXTRA2 == "Ppd" || CTEXTOEXTRA2 == "pPd" || CTEXTOEXTRA2 == "ppD" || CTEXTOEXTRA2 == "PPd" || CTEXTOEXTRA2 == "pPD"
                    || CTEXTOEXTRA2 == "PpD")
                {
                    CTEXTOEXTRA2 = "PPD";
                }






                //si la moneneda = 1  son pesos
                // si la moneda =2 son dolares

                if (Moneda == "1" && novenaFila.monedaU == "USD")
                {
                    aceso.InsertaErrorServicioP("El cliente No tiene el formato de moneda correcto " + documentoParametro.aCodigoCteProv, cuartaFila.nombreArchivo, fechaArchivo);
                    enviaEmailError("El cliente No tiene el formato de moneda correcto " + documentoParametro.aCodigoCteProv + cuartaFila.nombreArchivo);

                    seMovioArchivo = 1;
                }

                if (CMETODOPAG == "")
                {
                    CMETODOPAG = "99";
                }

                if (CUSOCFDI == "")
                {
                    CUSOCFDI = "P01";

                }
                if (CTEXTOEXTRA2 == "")
                {
                    CTEXTOEXTRA2 = "PPD";
                }

                if (CTEXTOEXTRA2 == "PUE")
                {
                    CCANTPARCI = "1";
                }
                else
                {
                    CCANTPARCI = "2";
                }
            }
            // insertamos documento en contpaq
            if (seMovioArchivo == 1)
            {
                return 0;
            }
            else
            {
                return insercion.AgregarDocumentoFT(ref documentoParametro, ARRAYD1Fila,ref mensajeobteido,CMETODOPAG,CUSOCFDI,CTEXTOEXTRA2,RFC, CCANTPARCI);
            }
        }
    }
}
