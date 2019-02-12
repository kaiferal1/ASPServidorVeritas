using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioVeritas
{
    public class clsConst
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="frm"></param>
        /// <param name="path"></param>
        static public void saveLog(string error, string frm)
        {
            try
            {
                using (System.IO.StreamWriter write = new System.IO.StreamWriter("txtLog.txt", true))
                {
                    //StreamWriter write = new StreamWriter(clsConst.pathLog, true);
                    write.WriteLine("----------------------------------------------------------------------\r\n"
                        + "Fecha:              " + DateTime.Now.ToString() + " \r\n"
                        + "Formulario/Metodo:  " + frm + " \r\n"
                        + "Error:              " + error + " \r\n"
                        + "----------------------------------------------------------------------\r\n");
                    write.Flush();
                    write.Close();
                }
            }
            catch (Exception exe) { clsConst.saveLog(exe.Message, ""); }
            finally { GC.Collect(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public string readLog()
        {
            string respuesta = string.Empty;
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader("txtLog.txt"))
                {
                    respuesta = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
                respuesta = exe.Message; }
            return respuesta;
        }


    }
}
