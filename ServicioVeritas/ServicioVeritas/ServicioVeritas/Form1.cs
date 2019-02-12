using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ServicioVeritas
{
    public partial class Form1 : Form
    {
        
        clsArchivo[] ListaArcivos = new clsArchivo[100000];
        clsADO ACESA = new clsADO();
        Timer timer1 = new Timer();
        DirectoryInfo di = new DirectoryInfo(@"D:\DirectorioVeritas\Testing");
        clsDocumento doc = new clsDocumento();
        //SDK.tDocumento documentoParametro = new SDK.tDocumento();
        InserionesSDKContpaq insercion = new  InserionesSDKContpaq();
        //FileInfo informacion;
        string EstadoServicio = "";
        
        int counter = 0;
        int Lindice = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public void repetir()
        {

            try
            {
                int Repetir = 0;
               // int clic = 0;
                while (Repetir == 0)
                {
                    try
                    {
                        if (di.GetFiles().Length != 0)
                        {
                            Enlugardetimer();
                            
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(30000);
                        }
                    }
                    catch (Exception exe)
                    {
                        System.Threading.Thread.Sleep(30000);
                    }
                }
            }
            catch (Exception exe)
            {
                clsConst.saveLog(exe.Message, "");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //InitializeTimer();
            this.Hide();
            repetir();
            

        }
       
        public void Archivos()
        {



            foreach (var fi in di.GetFiles("*", SearchOption.TopDirectoryOnly).OrderBy(x => x.Name).ToList())
            {
                if (fi.Name != "")
                {
                    if (fi.Name[0].Equals("9"))
                    { ListaArcivos[Lindice].tipoArchivo = "Nota de Credito"; }
                    else {
                        if (fi.Name[0].Equals("1"))
                        { ListaArcivos[Lindice].tipoArchivo = "Factura"; }
                    }

                    ListaArcivos[Lindice] = new clsArchivo();
                    ListaArcivos[Lindice].nombre = fi.Name;

                    ListaArcivos[Lindice].peso = fi.Length;
                    ListaArcivos[Lindice].Error = "";
                    ListaArcivos[Lindice].observaciones = "";
                    ListaArcivos[Lindice].folio = fi.Name;
                    ListaArcivos[Lindice].Ecorrejido = fi.Name;
                    ListaArcivos[Lindice].fecha = fi.CreationTime;

                    Lindice++;
                }
                //MessageBox.Show(fi.Name);
                //MessageBox.Show(fi.Length.ToString());
            }

        }

        private void InitializeTimer()
        {
            // Run this procedure in an appropriate event.
            //counter = 0;
            timer1.Interval = 40000;// 1000 es un segundo 
            timer1.Enabled = true;
            timer1.Start();
            // Hook up timer's tick event handler.
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }
        
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            repetir();
            //EstadoServicio = ACESA.VerificaServicio();
            //if (EstadoServicio == "DETENIDO")
            //{
            //    // si el proceso esta detenido  limpia el arreglo 
            //    Lindice = 0;
            //    ListaArcivos = null;
            //    ListaArcivos=  new clsArchivo[1000];
            //}
            //else
            //{
            //    if (EstadoServicio == "EJECUCION")
            //    {
            //        if (counter >= 3)
            //        {
            //            // Exit loop code.
            //            timer1.Enabled = false;

            //            counter = 0;
            //            Lindice = 0;
            //            InitializeTimer();
            //        }
            //        else
            //        {
            //            // Run your procedure here.
            //            // make a evaluation for array []  listFiles xD me encanta cometnar en ingles se ve tan profecional 
            //            ProcesoArchivo();
            //            // Increment counter.
            //            counter = counter + 1;
            //        }
            //    }
            //    else
            //    {
            //        //ESTE ES CASO DE ERROR DE PROCESO MANDAR MENSAJE DE ERRRO 

            //       // MessageBox.Show("ERRRO INESPERADO VERIFICAR CON EL DESARROLADOR"+ACESA.VerificaServicio());
            //    }
            //}
        }







        public void ProcesoArchivo()
        {
            if (Lindice == 0)
            {
                Archivos();
            }
            else
            {
               
               
                foreach (var fi in di.GetFiles("*", SearchOption.AllDirectories).OrderBy(x => x.Name).ToList())
                {
                    int compara = 0;
                    string mensaje = "";
                    while (compara < Lindice)
                    {
                        if (ACESA.VerificaServicio() == "DETENIDO")
                        {
                            // si el proceso esta detenido  limpia el arreglo 
                            Lindice = 0;
                            ListaArcivos = null;
                            ListaArcivos = new clsArchivo[100000];
                        }
                        else
                        {
                            if (ACESA.VerificaServicio() == "EJECUCION")
                            {
                                if (ListaArcivos[compara].nombre == fi.Name)
                                {
                                    if (ListaArcivos[compara].peso == fi.Length)
                                    {
                                        //esta listo para insertar en contpaq en BD y luego cambiarlo de carpeta
                                        //ACESA.insertaArchivos(ref ListaArcivos[compara]);
                                        if (doc.LeeeArchivo(ListaArcivos[compara].nombre, ListaArcivos[compara].fecha) == 1)
                                        {

                                        }
                                        else
                                        {
                                            
                                            MueveArchivo(ListaArcivos[compara].nombre);
                                            BorraArchivo(ListaArcivos[compara].nombre);
                                            mensaje = "si";
                                            compara++;
                                        }
                                    }
                                    else
                                    {
                                        // actualiza el peso del archivo
                                        ListaArcivos[compara].peso = fi.Length;
                                        mensaje = "si";
                                        compara++;
                                    }
                                }
                                else
                                {
                                    if (mensaje != "si")
                                    {
                                        mensaje = "NO";
                                        compara++;
                                    }
                                    else
                                    {
                                        compara = Lindice;
                                    }
                                }


                                if (mensaje.Equals("NO"))
                                {
                                    if (fi.Name != "")
                                    {
                                        ListaArcivos[Lindice] = new clsArchivo();
                                        ListaArcivos[Lindice].nombre = fi.Name;
                                        ListaArcivos[Lindice].peso = fi.Length;
                                        Lindice++;
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    
                }
            }

        }
        public void BorraArchivo(string nombreArchivo)
        {
            
            for (int d = 0; d < Lindice; d++)
            {
                if(ListaArcivos[d].nombre==nombreArchivo)
                {
                    if (d == Lindice - 1)
                    {
                        //es el ultimo dato 
                        ListaArcivos[d].nombre = "";
                        ListaArcivos[d].peso = 0;
                        Lindice = Lindice - 1;
                        ListaArcivos[Lindice] = null;
                    }
                    else
                    {
                        //es el primero
                        if (d == 0)
                        {
                            int j;

                            for (int w = 0; w < Lindice; w++)
                            {
                                j = w + 1;
                                if (j == Lindice)
                                {
                                    
                                    Lindice = Lindice - 1;
                                    ListaArcivos[Lindice] = null;
                                }
                                else
                                {
                                    ListaArcivos[w].nombre = ListaArcivos[j].nombre;
                                    ListaArcivos[w].peso = ListaArcivos[j].peso;
                                    j++;
                                }
                            }
                        }
                        else
                        {
                            //esta en medio
                            int j;

                            for (int w = d; w < Lindice; w++)
                            {
                                j = w + 1;
                                if (j == Lindice)
                                {
                                    Lindice = Lindice - 1;
                                    ListaArcivos[Lindice] = null;
                                }
                                else
                                {
                                    ListaArcivos[w].nombre = ListaArcivos[j].nombre;
                                    ListaArcivos[w].peso = ListaArcivos[j].peso;
                                    j++;
                                }
                            }
                        }
                    }
                    
                }
            }
        }
        public void MueveArchivo(string nombreArchivo)
        {
            if (File.Exists(@"D:\DirectorioVeritas\Testing\" + nombreArchivo))
            {

                if (!File.Exists(@"D:\DirectorioVeritas\ProcesadosTesting\" + nombreArchivo))
                {
                    File.Move(@"D:\DirectorioVeritas\Testing\" + nombreArchivo, @"D:\DirectorioVeritas\ProcesadosTesting\" + nombreArchivo);
                }
                else
                {
                   // MessageBox.Show("Ya existe el archivo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {


               // MessageBox.Show("Archivo not Fun", "ALERTA DE CHOLO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    Enlugardetimer();
            //}
            //catch (Exception exe)
            //{
            //    MessageBox.Show(exe.Message);
            //}

            //try
            //{
            //    int Repetir = 0;
            //    int clic = 0;
            //    while (Repetir == 0)
            //    {
            //        if (clic == 10)
            //        {
            //            Enlugardetimer();
            //            clic = 0;
            //        }
            //        else
            //        { clic++; }
            //    }
            //}
            //catch (Exception exe)
            //{
            //    clsConst.saveLog(exe.Message, "");
            //}
        }
        public void Enlugardetimer()
        {
            //EstadoServicio = ACESA.VerificaServicio();
            if (ACESA.VerificaServicio() == "DETENIDO")
            {
                // si el proceso esta detenido  limpia el arreglo 
                Lindice = 0;
                ListaArcivos = null;
                ListaArcivos = new clsArchivo[1000];
            }
            else
            {
                if (ACESA.VerificaServicio() == "EJECUCION")
                {
                   
                        //Run your procedure here.
                        //make a evaluation for array[] listFiles xD me encanta cometnar en ingles se ve tan profecional
                        ProcesoArchivo();
                        //Increment counter.
                        counter = counter + 1;
                    
                }
                else
                {
                    //ESTE ES CASO DE ERROR DE PROCESO MANDAR MENSAJE DE ERROR 

                    //MessageBox.Show("ERRRO INESPERADO VERIFICAR CON EL DESARROLADOR" + ACESA.VerificaServicio());
                }
            }
        }


    }
}

