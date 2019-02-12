using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ServicioVeritas
{
    class clsADO
    {
        //public string cadena = clsIni.stringConectionServer();// @"Data Source=;Initial Catalog = ; User ID = ; Password=";
        SqlConnection conexion = new SqlConnection(@"Data Source = .\CONTPAQI; Initial Catalog = VeritasDocumentos; User ID = sa; Password=PueblaIT76");
        SqlCommand comando = new SqlCommand();
        private void abrirConexion()
        {
            //conexion.ConnectionString = 
            try
            {
                conexion.Open();
                comando.Connection = conexion;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
            }
        }
        private void ClouseConexion()
        { 
            conexion.Close();
        }

        // **** inico de consultaas para extraccion de dato cliente 
        public SqlDataReader RecuperaParametrosCliente(string CodProv)
        {
            try
            {
                string Respuesta = "";
                SqlDataReader read = null;
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_ConsultaParametrosProv";
                comando.Parameters.AddWithValue("@CodProv", CodProv);
             
                read = comando.ExecuteReader();
             
                //ClouseConexion();
                return read;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                return null;
            }
        }
        public string RecuperaCodCliProv(string RFC,string NombreCliente)
        {
            try
            {
                string Respuesta = "";
                SqlDataReader read = null;
                 abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_ConsultaCodCliProv";
                comando.Parameters.AddWithValue("@RFC",RFC);
                comando.Parameters.AddWithValue("@nombreCliente", NombreCliente);
                read = comando.ExecuteReader();
                if (read.Read())
                {
                    Respuesta = read[0].ToString();
                }
                ClouseConexion();
                return Respuesta;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                return "-1" ;
            }
        }
       //**** termina extraccion de datos cliente 

        //apartado usable para Funciones de servicio
        public void InsertaErrorServicio( string mensaqje,string nombreArchivo,DateTime FechaArchivo)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_ManejaServicio";
                comando.Parameters.AddWithValue("@descripcion",mensaqje);
                comando.Parameters.AddWithValue("@badera",0);
                comando.Parameters.AddWithValue("@NombreArchivo", nombreArchivo);
                comando.Parameters.AddWithValue("@Fecha",FechaArchivo);
                comando.ExecuteNonQuery();
                ClouseConexion();
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
            }
        }
        public void InsertaErrorServicioP(string mensaqje, string nombreArchivo, DateTime FechaArchivo)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_ManejaServicio";
                comando.Parameters.AddWithValue("@descripcion", mensaqje);
                comando.Parameters.AddWithValue("@badera", 4);
                comando.Parameters.AddWithValue("@NombreArchivo", nombreArchivo);
                comando.Parameters.AddWithValue("@Fecha", FechaArchivo);
                comando.ExecuteNonQuery();
                ClouseConexion();
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
            }
        }
        public string VerificaServicio()
        {
            try
            {
                SqlDataReader REDER = null;
                string respuesta = string.Empty;
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_ManejaServicio";
                comando.Parameters.AddWithValue("@descripcion", string.Empty);
                comando.Parameters.AddWithValue("@badera", 3);
                comando.Parameters.AddWithValue("@NombreArchivo", string.Empty);
                comando.Parameters.AddWithValue("@Fecha", string.Empty);
                REDER = comando.ExecuteReader();
                if (REDER.Read())
                {
                    respuesta = REDER[0].ToString();
                }
                
                ClouseConexion();
                return respuesta;
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                ClouseConexion();
                return exe.Message;
            }
        }
        //Termina aplicacion  de Servicxio

        public string insertaArchivos(ref clsArchivo arreglo)
        {
            try
            {
               
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                comando.CommandText = "SP_Manejo_Archivos";
                comando.Parameters.AddWithValue("@LN", arreglo.nombre);
                comando.Parameters.AddWithValue("@nombre", arreglo.nombre);
                comando.Parameters.AddWithValue("@peso", arreglo.peso);
                comando.Parameters.AddWithValue("@estatus", arreglo.estatus);
                comando.Parameters.AddWithValue("@error",arreglo.Error);
                comando.Parameters.AddWithValue("@Observaciones", arreglo.observaciones);
                comando.Parameters.AddWithValue("@TipoArchivo", arreglo.tipoArchivo);
                comando.Parameters.AddWithValue("@Folio", arreglo.folio);
                comando.Parameters.AddWithValue("@ECorrejido", arreglo.Ecorrejido);
                comando.Parameters.AddWithValue("@fechaArchivo", arreglo.fecha);
                comando.Parameters.AddWithValue("@bandera", 0);
                comando.ExecuteNonQuery();
                ClouseConexion();
                return "Se realizo la operacion";
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                return exe.Message;
            }
        }

        public void InsertaErrorDocumento(string Error, string NombreArchivo)
        {

        }

        public void insertaDocumentoE0(clsDocumentoE0 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.Cabecera);
                comando.Parameters.AddWithValue("", encabezado.campovacio1);
                comando.Parameters.AddWithValue("", encabezado.nombreEmpresa);
                comando.Parameters.AddWithValue("", encabezado.RFC);
                comando.Parameters.AddWithValue("", encabezado.Calle);
                comando.Parameters.AddWithValue("", encabezado.NumInterior);
                comando.Parameters.AddWithValue("", encabezado.NumExterior);
                comando.Parameters.AddWithValue("", encabezado.Colonia);
                comando.Parameters.AddWithValue("", encabezado.Municipio);
                comando.Parameters.AddWithValue("", encabezado.CampoVacio2);
                comando.Parameters.AddWithValue("", encabezado.Estado);
                comando.Parameters.AddWithValue("", encabezado.Pais);
                comando.Parameters.AddWithValue("", encabezado.CodigoPostal);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE1(clsDocumentoE1 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.folioFactura);
                comando.Parameters.AddWithValue("", encabezado.fecha);
                comando.Parameters.AddWithValue("", encabezado.FormadePago);
                comando.Parameters.AddWithValue("", encabezado.campovacio1);
                comando.Parameters.AddWithValue("", encabezado.campovacio2);
                comando.Parameters.AddWithValue("", encabezado.totalFactura);
                comando.Parameters.AddWithValue("", encabezado.subTotal);
                comando.Parameters.AddWithValue("", encabezado.campovacio3);
                comando.Parameters.AddWithValue("", encabezado.campovacio4);
                comando.Parameters.AddWithValue("", encabezado.numerico1);
                comando.Parameters.AddWithValue("", encabezado.numerico2);
                comando.Parameters.AddWithValue("", encabezado.metodoDePago);
                comando.Parameters.AddWithValue("", encabezado.EstadoPais);
                comando.Parameters.AddWithValue("", encabezado.ReferenciaCliente);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE3(clsDocumentoE3 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombrearchivo);
                comando.Parameters.AddWithValue("", encabezado.cabezera);
                comando.Parameters.AddWithValue("", encabezado.campovacio1);
                comando.Parameters.AddWithValue("", encabezado.campovacio2);
                comando.ExecuteNonQuery();
                ClouseConexion();
            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE4(clsDocumentoE4 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.RFC);
                comando.Parameters.AddWithValue("", encabezado.RazonSocialCliente);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE5(clsDocumentoE5 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombrearchibo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.calle);
                comando.Parameters.AddWithValue("", encabezado.Numinterior);
                comando.Parameters.AddWithValue("", encabezado.NumExterior);
                comando.Parameters.AddWithValue("", encabezado.Colonia);
                comando.Parameters.AddWithValue("", encabezado.Campovacio1);
                comando.Parameters.AddWithValue("", encabezado.Municipio);
                comando.Parameters.AddWithValue("", encabezado.Estado);
                comando.Parameters.AddWithValue("", encabezado.Pais);
                comando.Parameters.AddWithValue("", encabezado.CodigoPostal);
                comando.Parameters.AddWithValue("", encabezado.EmailCliente);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE6(clsDocumentoE6 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.nombre_Cuenta);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE8(clsDocumentoE8 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.NombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.CampoVacio1);
                comando.Parameters.AddWithValue("", encabezado.monedaU);
                comando.Parameters.AddWithValue("", encabezado.TipoCambio);
                comando.Parameters.AddWithValue("", encabezado.campovacio2);
                comando.Parameters.AddWithValue("", encabezado.campovacio3);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoE9(clsDocumentoE9 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.NombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.CuentaXpagar);
                comando.Parameters.AddWithValue("", encabezado.ContactoCliente);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }

        public void insertaDocumentoEC1(clsDocumentoEC1 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.NombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.numerito);
                comando.Parameters.AddWithValue("", encabezado.nota);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }
        public void insertaDocumentoEC15(clsDocumentoEC15 encabezado, DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.totalPyL);
                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }
        public void insertaDocumentoD1(clsDocumentoD1 encabezado,DateTime fecha)
        {
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "";
                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("", encabezado.nombreArchivo);
                comando.Parameters.AddWithValue("", encabezado.cabecera);
                comando.Parameters.AddWithValue("", encabezado.CampoVacio1);
                comando.Parameters.AddWithValue("", encabezado.NumeroReporte);
                comando.Parameters.AddWithValue("", encabezado.Descripcion_Prueba);
                comando.Parameters.AddWithValue("", encabezado.CampoVacio2);
                comando.Parameters.AddWithValue("", encabezado.Cantidad);
                comando.Parameters.AddWithValue("", encabezado.PrecioUnitario);
                comando.Parameters.AddWithValue("", encabezado.PrecioSubTotal);
                comando.Parameters.AddWithValue("", encabezado.TotalCDoCar);
                comando.Parameters.AddWithValue("", encabezado.Descuento);
                comando.Parameters.AddWithValue("", encabezado.CargoExtra);

                comando.ExecuteNonQuery();
                ClouseConexion();

            }
            catch (Exception exe)
            {
                //error de programacion o conexion a las bd nada mas 
            }
        }
    }
}
