using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPSERVIDORVeritas.Clases;
using System.Web.Services;
using System.Net;
using System.Data;
using System.Net.Mail;

namespace ASPSERVIDORVeritas
{
    public partial class ServidorMetodhs : System.Web.UI.Page
    {
        private static clsADOInterfaz interfaz = new clsADOInterfaz();

        protected void Page_Load(object sender, EventArgs e)
        { }

        #region Navegacion

        /*Inicio*/
        [WebMethod]
        public static string consultarValores()
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
            string res = interfaz.buscarValEnviado(val);
            return res;
        }

        [WebMethod]
        public static string buscarEnEnviadas(string val)
        {
            string res = interfaz.buscarValNoEnviado(val);
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

        //[WebMethod]
        //public static string muestraPagina()
        //{
        //    string res = interfaz.MuestraClientes(2, 2);
        //    return res;
        //}

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

        #endregion

        #region Factura        

        [WebMethod]
        public static string vistaPrevia(string id) {
            string res = "{\"a\":" + interfaz.vistaPrevia(int.Parse(id)) + ",\"b\":" + interfaz.correos(id) + "}";
            return res;
        }

        [WebMethod]
        public static bool enviarMail(string rfc,string[] correo)
        {
            //bool res = interfaz.enviarCorreo(rfc,correo);
            return true;
        }

        #endregion










        #region Usuario
        [WebMethod]
        public static int Login(string usuario, string contrasena, int bandera)
        {
            int res = interfaz.ValidaUsuario(usuario, contrasena, bandera);
            if (res == 1) { }
            return res;
        }



        [WebMethod]
        public static string consultaUsuarios()
        {
            string res = interfaz.ValidaUsuario(string.Empty, string.Empty);
            return res;
        }
        #endregion
    }
}