using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;

namespace ServicioVeritas
{
    /// <summary>
    /// Clase encargada de leer archivo .ini
    /// </summary>
   // public class Util
   // {
   //     /// <summary>
   //     /// Metodo que lee una un valor del archivo .ini
   //     /// </summary>
   //     /// <param name="section">Nombre de la seccion a la que se buscara un valor</param>
   //     /// <param name="key">Nombre del valor que se leera</param>
   //     /// <param name="def">En caso de Error Muestra esta cadena</param>
   //     /// <param name="retVal">Objeto tipo Cadena de texto que regresa el valor buscado</param>
   //     /// <param name="size">Entero que representa el tamaño del valor que se regresara</param>
   //     /// <param name="filePath">Ubicacion del archivo en el que se buscara el valor</param>
   //     /// <returns></returns>
   //     [DllImport("kernel32")]
   //     public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

   //     /// <summary>
   //     /// Metodo que lee una un valor del archivo .ini
   //     /// </summary>
   //     /// <param name="section">Nombre de la seccion a la que se buscara un valor</param>
   //     /// <param name="key">Nombre del valor que se leera</param>
   //     /// <param name="val">Valor que se guardara en el archivo .ini</param>
   //     /// <param name="filePath">Ubicacion del archivo en el que se buscara el valor</param>
   //     /// <returns></returns>
   //     [DllImport("kernel32")]
   //     public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
   // }

   //public class clsIni
   // {
   //     /// <summary>
   //     /// \\SYSCONFGJRT.ini
   //     /// </summary>
   //     public const string Path = "\\SYSCONFIG.ini";

   //     /// <summary>
   //     /// Metodo que leera solo un valor
   //     /// </summary>
   //     /// <param name="seccion">Seccion que se leera</param>
   //     /// <param name="key">Nombre del la llave que tiene asignado el valor</param>
   //     /// <param name="nomFile">Nombre del archivo .ini</param>
   //     /// <returns>Regresa el valor obtenido o el mensaje de ERROR</returns>
   //     public static string read(string seccion, string key, string nomFile)
   //     {
   //         StringBuilder cantidad = new StringBuilder();
   //         string path = Environment.CurrentDirectory + nomFile;
   //         if (File.Exists(path)) { Util.GetPrivateProfileString(seccion, key, "ERROR", cantidad, cantidad.Capacity+2, path); }
   //         return cantidad.ToString();
   //     }

   //     /// <summary>
   //     /// Modificar Valores
   //     /// </summary>
   //     /// <param name="Seccion">Seccion que se buscara</param>
   //     /// <param name="Key">Nombre del la llave que se le asignara valor</param>
   //     /// <param name="Valor">Nuevo valor que se asignara</param>
   //     /// <param name="nomFile">Nombre del archivo .ini</param>
   //     /// <returns></returns>
   //     public static bool write(string Seccion, string Key, string Valor, string nomFile)
   //     {
   //         bool resp = true;
   //         string path = Environment.CurrentDirectory + nomFile;
   //         try { Util.WritePrivateProfileString(Seccion, Key, Valor, Path); }
   //         catch (Exception exe) { resp = false; clsConst.saveLog(exe.Message, ""); }
   //         return resp;
   //     }

   //     /// <summary>
   //     /// Crea la cadena de conexion de la base de datos
   //     /// </summary>
   //     /// <returns>Regresa la cadena de conexion a la base de datos</returns>
   //     public static string stringConectionServer()
   //     {
   //         string db = "BASEDEDATOS";
   //         string cCadena = "Initial Catalog = " + read(db, "DATABASE", Path) +
   //             "; Data Source = " + read(db, "SERVER", Path) +
   //             "; User Id = " + read(db, "USERID", Path) +
   //             "; Password = " + read(db, "USERPWD", Path) + ";";
   //         return cCadena;
   //     }

   //     /// <summary>
   //     /// Obtiene la cadena de conexion que se encuantras en el archivo app.config
   //     /// </summary>
   //     /// <param name="connectionStringName">Ocupar constante nameStringConnection</param>
   //     /// <returns>Regresa la cadena de conexion que se encuantra en el archivo app.config</returns>
   //     private static string GetConnectionString(string connectionStringName)
   //     {
   //         Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
   //         ConnectionStringSettings connStringSettings = appconfig.ConnectionStrings.ConnectionStrings[connectionStringName];
   //         return connStringSettings.ConnectionString;
   //     }

   //     /// <summary>
   //     /// Guarda una nueva cadena de conexion en donde se encontraba la otra
   //     /// </summary>
   //     /// <param name="connectionStringName">Ocupar constante nameStringConnection</param>
   //     /// <param name="connectionString">Nueva cadena de conexion que se guardara en el archivo app.config</param>
   //     private static void SaveConnectionString(string connectionStringName, string connectionString)
   //     {
   //         Configuration appconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
   //         appconfig.ConnectionStrings.ConnectionStrings[connectionStringName].ConnectionString = connectionString;
   //         appconfig.Save();
   //     }
   // }
}
