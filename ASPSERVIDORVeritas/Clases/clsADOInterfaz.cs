using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Xml.Serialization;

namespace ASPSERVIDORVeritas.Clases
{
    public class clsADOInterfaz
    {
        //private string cadena = @"Data Source=KATSURAGI;Initial Catalog=VeritasDocumentos;Integrated Security=True";//;Encrypt=True
        private string cadena = @"Data Source=SERVER\FINANZAS;Initial Catalog = VeritasDocumentos; User ID = sa; Password=Done2016+";
        //private string cadena = @"Data Source=SERVER\FINANZAS;Initial Catalog = VeritasDocumentos; User ID = sa; Password=Done2016+";
        //private string cadena = @"Data Source = .\CONTPAQI; Initial Catalog = VeritasDocumentos; User ID = sa; Password=PueblaIT76";
        private SqlConnection conexion { set; get; }
        private SqlCommand comando { set; get; }
        private SqlDataReader lector { set; get; }

        private string PathFacturas { set; get; } = @"D:\DirectorioVeritas\ProcesadosInspecciones\";
        private string PathContratos { set; get; } = @"D:\DirectorioVeritas\ProcesadosTesting\";


        public clsADOInterfaz()
        {
            PathFacturas = getPath("FACTURAS");
            PathContratos = getPath("CONTRATOS");
        }

        private string getPath(string nombre)
        {
            string res = string.Empty;
            abrirConexion();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT valor FROM dbo.TB_Parametros WHERE nombre = '" + nombre + "'";
            SqlDataReader r = comando.ExecuteReader();
            if (r.Read())
            {
                res = r["valor"].ToString();
            }
            conexion.Close();
            return res;
        }

        public string inicio()
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_GenerarReporte";
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }

        #region usuario
        public int ValidaUsuario(string usuario, string contraseña, int opc)
        {
            lector = null;
            try
            {
                int respuesta = 0;
                abrirConexion();
                comando.CommandText = "SP_ValidaUsusarios";
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@Contraseña", contraseña);
                comando.Parameters.AddWithValue("@bandera", opc);
                lector = comando.ExecuteReader();
                if (lector.Read()) { respuesta = Convert.ToInt32(lector[0].ToString()); }
                conexion.Close();
                return respuesta;
            }
            catch (Exception exe) { return 0; }
        }

        public string ValidaUsuario(string usuario, string contraseña)
        {
            DataTable respuesta = new DataTable();
            int bandera = 3;
            try
            {
                abrirConexion();
                comando.CommandText = "SP_ValidaUsusarios";
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@Contraseña", contraseña);
                comando.Parameters.AddWithValue("@bandera", bandera);
                respuesta.Load(comando.ExecuteReader());
                respuesta.Columns["contraseña"].ColumnName = "passw";
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }
        #endregion

        #region cliente

        public bool EditaCliente(string rfc, int contrato, int formato)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "SP_EDITACLIENTE";
                comando.Parameters.AddWithValue("@RFC", rfc);
                comando.Parameters.AddWithValue("@APLICONTRA", contrato);
                comando.Parameters.AddWithValue("@FCORREO", formato);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception exe) { return false; }
        }

        public string MuestraClientes(int pagina, int cantidad)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "SP_MuestraClientes";
                comando.Parameters.AddWithValue("@bandera", 3);
                comando.Parameters.AddWithValue("@Pagina", pagina);
                comando.Parameters.AddWithValue("@RegistrosporPagina", cantidad);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }

        public string MuestraClientes()
        {
            try
            {
                DataTable respuesta = new DataTable();
                SqlDataReader read = null;
                abrirConexion();
                comando.CommandText = "SP_MuestraClientes";
                comando.Parameters.AddWithValue("@bandera", 5);
                read = comando.ExecuteReader();
                respuesta.Load(read);
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }

        public string MuestraClientesSingular(int id_Cliente, string RFC)
        {
            try
            {
                string json = string.Empty, correos = string.Empty;
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_MuestraClientesSingular";
                comando.Parameters.AddWithValue("@id_Cliente", id_Cliente);
                comando.Parameters.AddWithValue("@RFC", RFC);
                respuesta.Load(comando.ExecuteReader());
                respuesta.Columns["FORMATO CORREO"].ColumnName = "FormatoCoreo";
                if (respuesta.Rows.Count != 0)
                {
                    foreach (DataRow row in respuesta.Rows) { correos += ", \"" + row["Correo"].ToString() + "\""; }
                    correos = correos.Remove(0, 1);
                    json = "{ \"Nombre\":\"" + respuesta.Rows[0]["Nombre"].ToString() + "\","
                            + "\"RFC\":\"" + respuesta.Rows[0]["RFC"].ToString() + "\","
                            + "\"AplicaContrato\":\"" + respuesta.Rows[0]["AplicaContrato"].ToString() + "\","
                            + "\"FormatoCoreo\":\"" + respuesta.Rows[0]["FormatoCoreo"].ToString() + "\", \"Correo\":[" + correos + "] }";
                }
                conexion.Close();
                return json; // convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string convertCorreosJSON(DataTable dt)
        {
            List<string> Correo = new List<string>();
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            foreach (DataRow row in dt.Rows) { Correo.Add(row["Correo"].ToString()); }
            return serializer.Serialize(Correo);
        }

        public string consultarAsignarCorreo(string rfc)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "declare @id_Cliente int = (select top(1) id_Cliente from TB_ClienteCombinado where RFC ='" + rfc + "')"
                                        + "SELECT Correo FROM dbo.TB_ClienteCorreos WHERE id_Cliente = @id_Cliente";
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public List<string> lista(string rfc)
        {
            List<string> correo = new List<string>();
            try
            {
                abrirConexion();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "declare @id_Cliente int = (select top(1) id_Cliente from TB_ClienteCombinado where RFC ='" + rfc + "' )"
                    + " SELECT id_Cliente,Correo FROM dbo.TB_ClienteCorreos WHERE id_Cliente = @id_Cliente ";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                { correo.Add(read["Correo"].ToString()); }
                conexion.Close();
            }
            catch (Exception e) { }
            return correo;
        }

        public string asignarCorreo(string rfc, string[] correo)
        {
            try
            {
                abrirConexion();
                conexion.Close();
                comando.CommandType = CommandType.Text;
                List<string> listaOld = lista(rfc);
                bool flag = false;
                foreach (string neww in correo)
                {
                    flag = true;
                    for (int x = 0; x < listaOld.Count; x++)
                    {
                        if (neww == listaOld[x])
                        {
                            flag = false;
                            listaOld.Remove(neww);
                            break;
                        }
                    }
                    if (flag)
                    {
                        conexion.Open();
                        comando.CommandText = "declare @id_Cliente int = (select top(1) id_Cliente from TB_ClienteCombinado where RFC ='" + rfc + "' ) "
                        + "insert into TB_ClienteCorreos values(@id_Cliente, '" + neww + "')";
                        comando.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
                conexion.Open();
                foreach (string it in listaOld)
                {
                    comando.CommandText = "declare @id_Cliente int = (select top(1) id_Cliente from TB_ClienteCombinado where RFC ='" + rfc + "' ) " +
                        "DELETE dbo.TB_ClienteCorreos WHERE id_Cliente = @id_Cliente AND Correo ='" + it + "'";
                    comando.ExecuteNonQuery();
                }
                conexion.Close();
                return "todo bien";
            }
            catch (Exception exe)
            { return exe.Message; }
        }

        #endregion

        #region Plantilla

        public string listarPlantillas()
        {
            DataTable respuesta = new DataTable();
            try
            {
                abrirConexion();
                comando.CommandText = "dbo.SP_ManejaPlantilla";
                comando.Parameters.AddWithValue("@bandera", 1);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string modificarPlantilla(string id, string nombre, string asunto, string cuerpo, int bandera)
        {
            DataTable respuesta = new DataTable();
            try
            {
                abrirConexion();
                comando.CommandText = "dbo.SP_ManejaPlantilla";
                comando.Parameters.AddWithValue("@bandera", bandera);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@asunto", asunto);
                comando.Parameters.AddWithValue("@cuerpo", cuerpo);
                comando.Parameters.AddWithValue("@id", id);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string unaPlantilla(int id)
        {
            DataTable respuesta = new DataTable();
            try
            {
                abrirConexion();
                comando.CommandText = "dbo.SP_ManejaPlantilla";
                comando.Parameters.AddWithValue("@bandera", 4);
                comando.Parameters.AddWithValue("@id", id);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public bool deleteTemplete(int id)
        {
            DataTable respuesta = new DataTable();
            try
            {
                abrirConexion();
                comando.CommandText = "dbo.SP_ManejaPlantilla";
                comando.Parameters.AddWithValue("@bandera", 5);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (Exception exe) { return false; }
        }

        #endregion

        #region Servicios
        public string ReiniciaServicio(string idError, string archivo)
        {
            try
            {
                string respuesta = "";
                abrirConexion();
                comando.CommandText = "SP_Reinicia_Serivicio";
                //comando.Parameters.AddWithValue("@id_Error", idError);
                //comando.Parameters.AddWithValue("@nombreArchivo", archivo);
                respuesta = comando.ExecuteNonQuery().ToString();
                conexion.Close();
                return respuesta;
            }
            catch (Exception exe)
            {
                return exe.Message;
            }
        }

        public string MuestraErroresArchivos()
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "SP_ManejaServicio";
                comando.Parameters.AddWithValue("@descripcion", string.Empty);
                comando.Parameters.AddWithValue("@badera", 1);
                comando.Parameters.AddWithValue("@NombreArchivo", string.Empty);
                comando.Parameters.AddWithValue("@Fecha", string.Empty);
                respuesta.Load(comando.ExecuteReader());
                respuesta.Columns["FOLIO ERROR"].ColumnName = "FOLIO";
                respuesta.Columns["ESTATUS PROCESO"].ColumnName = "ESTATUS";
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe)
            {
                return null;
            }
        }

        public string archivo(string txt)
        {
            string inspecciones = getPath("CINSPECCIONES") + txt, testing = getPath("CTESTING") + txt;
            try
            {
                if (System.IO.File.Exists(inspecciones))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(inspecciones))
                    {
                        txt = sr.ReadToEnd();
                   
                        sr.Close();
                    }
                }
                else if (System.IO.File.Exists(testing))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(testing))
                    {
                        txt = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                else { txt = "EL archivo ya ha sido corregido favor de reprocesar"; }

                if (txt == "") { txt = "EL archivo ya ha sido corregido favor de reprocesar"; }
            }
            catch (Exception e)
            { txt = e.Message; }
            return txt;
        }

        public string guardarArchivo(string txt, string content)
        {
            string inspecciones = getPath("CINSPECCIONES") + txt, testing = getPath("CTESTING") + txt,
                inspec = getPath("INSPECCIONES") + txt, test = getPath("TESTING") + txt, res = string.Empty;
            try
            {
                if (File.Exists(inspecciones))
                {
                    using (StreamWriter write = new StreamWriter(inspecciones, false))
                    {
                        write.Write(content);
                        write.Flush();
                        write.Close();
                    }
                    if (File.Exists(inspec)) { File.Replace(inspecciones, inspec, null); }
                    else { File.Move(inspecciones, inspec); }
                    if (File.Exists(inspec))
                    { res = "modificacion"; }
                }
                else if (File.Exists(testing))
                {
                    using (StreamWriter write = new StreamWriter(testing, false))
                    {
                        write.Write(content);
                        write.Flush();
                        write.Close();
                    }
                    if (File.Exists(test)) { File.Replace(testing, test, null); }
                    else { File.Move(testing, test); }
                    if (File.Exists(test))
                    { res = "modificacion"; }
                }
            }
            catch (Exception e) { res = "error " + e.Message; }
            finally { GC.Collect(); }
            return res;
        }
        #endregion

        #region Facturas

        public string listarArchivosEnviados()
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_Manejo_Archivos";
                comando.Parameters.AddWithValue("@bandera", 3);
                comando.Parameters.AddWithValue("@estatus", "Enviado");
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string listarArchivosNoEnviados()
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_Manejo_Archivos";
                comando.Parameters.AddWithValue("@bandera", 3);
                comando.Parameters.AddWithValue("@estatus", "Procesado");
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string vistaPrevia(int id)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_Manejo_Archivos";
                comando.Parameters.AddWithValue("@bandera", 4);
                comando.Parameters.AddWithValue("@id", id);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);

            }
            catch (Exception exe) { return exe.Message; }
        }

        public string guardarFile(HttpFileCollection file, string rfc)
        {
            try
            {
                string error = string.Empty, path = @"/newFiles/" + rfc + "/";//); ;System.Web.Mvc.Server.MapPath(

                if (file != null && file.Count > 0)
                {
                    if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                    foreach (string nA in file.AllKeys)
                    {
                        HttpPostedFile a = file[nA];
                        a.SaveAs(path + a.FileName);
                    }
                }
                return error = "realizado de forma completa";
            }
            catch (Exception ex) { return ex.Message; }
        }

        public string correos(string id)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select Correo	from dbo.TB_Archivos a 	inner join dbo.TB_ClienteCombinado c on a.id_Cliente = c.Id_Cliente	inner join dbo.TB_ClienteCorreos cc ON cc.id_Cliente = c.Id_Cliente	where a.idArchivo =" + id;
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        public string archivoAdjunto(string id)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select distinct NumeroReporte from TB_ArchivoNoReporte WHERE id_Archivo =" + id;
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return exe.Message; }
        }

        private string quitarDuplicados(string val)
        {
            string sub = val.Substring(val.IndexOf("["), (val.IndexOf("]") + 1));
            return sub;
        }


        public bool enviarCorreo(string mails, string asunto, string cuerpo, string rfc, string archivos, HttpFileCollection files)
        {
            try
            {
                if (rfc.Contains(",")) { rfc = rfc.Substring(0, rfc.IndexOf(",")); }
                asunto = quitarDuplicados(asunto).Replace("[","").Replace("]", "");
                cuerpo = quitarDuplicados(cuerpo).Replace("[", "").Replace("]", "");
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<string> m = serializer.Deserialize<List<string>>(quitarDuplicados(mails));
                List<string> a = serializer.Deserialize<List<string>>(quitarDuplicados(archivos));

                string idarchivo = string.Empty, nombre = string.Empty, aplica = "1";
                DataTable respuesta = new DataTable();
                SmtpClient cliente = new SmtpClient();
                MailMessage mensaje = new MailMessage();
                abrirConexion();
                comando.CommandText = "SP_MuestraClientes";
                comando.Parameters.AddWithValue("@bandera", 4);
                comando.Parameters.AddWithValue("@RFC", rfc);
                respuesta.Load(comando.ExecuteReader());
                foreach (DataRow row in respuesta.Rows)
                {
                    idarchivo = row["idArchivo"].ToString();
                    nombre = row["nombre"].ToString();
                    aplica = row["AplicaContrato"].ToString();
                }
                if (int.Parse(aplica) == 1)
                {
                    //buscar archivo y adjuntarlo al mail
                    mensaje.Attachments.Clear();
                    System.IO.DirectoryInfo direc = new System.IO.DirectoryInfo(PathFacturas);
                    IEnumerable<System.IO.FileInfo> pdf = direc.EnumerateFiles("*.pdf", System.IO.SearchOption.TopDirectoryOnly);
                    IEnumerable<System.IO.FileInfo> xml = direc.EnumerateFiles("*.xml", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (string item in a)
                    {
                        foreach (System.IO.FileInfo file in pdf)
                        {
                            if (file.Name.Contains(nombre))
                            {
                                mensaje.Attachments.Add(new Attachment(file.FullName));
                                break;
                            }
                        }
                        foreach (System.IO.FileInfo file in xml)
                        {
                            if (file.Name.Contains(nombre))
                            {
                                mensaje.Attachments.Add(new Attachment(file.FullName));
                                break;
                            }
                        }
                    }
                    System.IO.DirectoryInfo news = new System.IO.DirectoryInfo(PathFacturas);
                    IEnumerable<System.IO.FileInfo> newsFiles = direc.EnumerateFiles("*.pdf, *.xml", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (System.IO.FileInfo file in newsFiles) { if (file.Name.Contains(nombre)) { mensaje.Attachments.Add(new Attachment(file.FullName)); } }

                    if (files != null && files.Count > 0)
                    {
                        foreach (string it in files.AllKeys)
                        {
                            HttpPostedFile i = files[it];
                            mensaje.Attachments.Add(new Attachment(i.InputStream, i.FileName));
                        }
                    }
                }
                // Crea el mensaje estableciendo quién lo manda y quién lo recibe
                mensaje.To.Clear();
                mensaje.Body = cuerpo;
                mensaje.Subject = asunto;
                mensaje.IsBodyHtml = true;
                foreach (string correo in m)
                {
                    mensaje.To.Add(correo);
                }
                mensaje.From = new MailAddress("Facturacion.Mexico@mx.bureauveritas.com");
                //Envía el mensaje.
                cliente.Host = "usbvrelay.bureauveritas.com";
                cliente.Port = 25;
                cliente.EnableSsl = false;
                //Añade credenciales si el servidor lo requiere.
                // AQUI NO SE ENVIA POR QUE ESTAMOS EN UNA COMPU DIFERENTE 
                // 
                // 
                cliente.Credentials = new NetworkCredential("", "");

                cliente.Send(mensaje);

                comando.Parameters.Clear();
                comando.CommandText = "SP_ManejaServicio";
                comando.Parameters.AddWithValue("@badera", 2);
                comando.Parameters.AddWithValue("@id", 2);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return true;
            }
            catch (Exception exe)
            { return false; }
        }



        #endregion

        #region Varios
        private void abrirConexion()
        {
            conexion = new SqlConnection();
            comando = new SqlCommand();
            conexion.ConnectionString = cadena;
            conexion.Close();
            conexion.Open();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Clear();
        }

        private void ClouseConexion()
        {
            conexion.Close();
        }

        private string convertDatatableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                { row.Add(col.ColumnName, dr[col]); }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
        #endregion

        public string buscarEnClientes(string val)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_BuscarVal";
                comando.Parameters.AddWithValue("@bandera", 1);
                comando.Parameters.AddWithValue("@val", val);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }
        public string buscarValArchivo(string val)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_BuscarVal";
                comando.Parameters.AddWithValue("@bandera", 2);
                comando.Parameters.AddWithValue("@val", val);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }
        public string buscarValEnviado(string val)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_BuscarVal";
                comando.Parameters.AddWithValue("@bandera", 3);
                comando.Parameters.AddWithValue("@val", val);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }
        public string buscarValNoEnviado(string val)
        {
            try
            {
                DataTable respuesta = new DataTable();
                abrirConexion();
                comando.CommandText = "dbo.SP_BuscarVal";
                comando.Parameters.AddWithValue("@bandera", 4);
                comando.Parameters.AddWithValue("@val", val);
                respuesta.Load(comando.ExecuteReader());
                conexion.Close();
                return convertDatatableToJson(respuesta);
            }
            catch (Exception exe) { return string.Empty; }
        }
    }
}

