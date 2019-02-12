using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace ASPSERVIDORVeritas.web
{
    public partial class webServices : System.Web.UI.Page
    {
        private static string rfc = string.Empty;
        private static Clases.clsADOInterfaz interfaz = new Clases.clsADOInterfaz();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpFileCollection listFile = Request.Files;
                NameValueCollection form = Request.Form;

                string rfc = form["rfc"], correos = form["correos"], asunto = form["asunto"], cuerpo = form["cuerpo"], archivos = form["archivos"];

                interfaz.enviarCorreo(correos, asunto, cuerpo, rfc, archivos, listFile);

            }
        }

        private static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }

        #region Navegacion

        /*Inicio*/
        [WebMethod]
        public static string inicio()
        {
            string res = interfaz.inicio();
            return res;
        }

        /*Clientes*/
        [WebMethod]
        public static string buscarEnClientes(string val)
        {
            string res = interfaz.buscarEnClientes(val);
            return res;
        }

        [WebMethod]
        public static string MuestraClientes()
        {
            string res = interfaz.MuestraClientes();
            return res;
        }

        /*Errores*/
        [WebMethod]
        public static string MuestraErrores()
        { return interfaz.MuestraErroresArchivos(); }

        [WebMethod]
        public static string buscarEnArchivos(string val)
        {
            string res = interfaz.buscarValArchivo(val);
            return res;
        }

        /*Plantillas*/
        [WebMethod]
        public static string listadoTempletes()
        {
            string res = interfaz.listarPlantillas();
            return res;
        }

        /*Facturas*/
        [WebMethod]
        public static string facturas()
        {
            string res = "{\"enviados\":" + interfaz.listarArchivosEnviados() +
                ",\"noenviados\":" + interfaz.listarArchivosNoEnviados() + "}";
            return res;
        }

        [WebMethod]
        public static string buscarEnNoEnviadas(string val)
        {
            string res = interfaz.buscarValNoEnviado(val);
            return res;
        }

        [WebMethod]
        public static string buscarEnEnviadas(string val)
        {
            string res = interfaz.buscarValEnviado(val);
            return res;
        }
        /**/
        #endregion

        #region inicio
        #endregion

        #region Cliente

        [WebMethod]
        public static string MuestraClientesSingular(int id_Cliente, string RFC)
        {
            string res = interfaz.MuestraClientesSingular(id_Cliente, RFC);
            return res;
        }

        [WebMethod]
        public static bool editarCliente(int contrato, string RFC, int formato)
        {
            bool res = interfaz.EditaCliente(RFC, contrato, formato);
            return res;
        }

        [WebMethod]
        public static string consultarAsignarMail(string rfc)
        {
            string res = interfaz.consultarAsignarCorreo(rfc);
            return res;
        }

        [WebMethod]
        public static string asignarMail(string rfc, string[] newcorreo)
        {
            string res = interfaz.asignarCorreo(rfc, newcorreo);
            return res;
        }

        [WebMethod]
        public static string obtenerPlantillas()
        {
            string res = interfaz.listarPlantillas();
            return res;
        }

        #endregion

        #region Servicio

        [WebMethod]
        public static string ReiniciaServicio(string idError, string archivo)
        {
            //cambiar a void si se cree neesario
            // regresa una  respuesta de la BD una cadena mensaje  esta enviarla a la aplicacion en el diseño de sistema
            return interfaz.ReiniciaServicio(idError, archivo);
        }

        [WebMethod]
        public static string cargarArchivo(string archivo)
        { return interfaz.archivo(archivo); }

        [WebMethod]
        public static string guardarArchivo(string archivo, string contenido)
        { return interfaz.guardarArchivo(archivo, contenido); }

        #endregion

        #region Plantillas

        [WebMethod]
        public static string modificarTemplete(string id, string nombre, string asunto, string cuerpo, string bandera)
        {
            string res = interfaz.modificarPlantilla(id, nombre, asunto, cuerpo, int.Parse(bandera));
            return res;
        }

        [WebMethod]
        public static string obtenerTemplete(string id)
        {
            string res = interfaz.unaPlantilla(int.Parse(id));
            return res;
        }

        [WebMethod]
        public static bool deleteTemplete(string id)
        {
            bool res = interfaz.deleteTemplete(int.Parse(id));
            return res;
        }

        #endregion

        #region Factura        

        [WebMethod]
        public static string vistaPrevia(string id)
        {
            string res = "{\"a\":" + interfaz.vistaPrevia(int.Parse(id));
            res += ",\"b\":" + interfaz.correos(id) + ",\"c\":";
            res += interfaz.archivoAdjunto(id) + " }";
            return res;
        }

        [WebMethod]
        public static bool enviarMail(string correos, string asunto, string cuerpo, string rfc, string archivos)
        {
            //bool res = interfaz.enviarCorreo(correos, asunto, cuerpo, rfc,archivos);
            return true;
        }

        #endregion
    }
}

