using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioVeritas
{
    class clsArchivo
    {

        public string lineaNegocio = "testing";
        public string nombre { set; get; }
        public long peso { set; get; }
        public string estatus { set; get; }
        public string Error { set; get; }
        public string observaciones { set; get; }
        public string tipoArchivo { set; get; }
        public string folio { set; get; }
        public string Ecorrejido { set; get; }
        public DateTime fecha { set; get; }
    }
}
