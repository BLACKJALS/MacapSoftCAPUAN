using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class MotivoRemisionVM
    {
        public string id_historiaClinica { get; set; }
        public string numeroDocumento { get; set; }
        public string nombreMotivoRemision { get; set; }
        public string nombrePaciente { get; set; }
        public string lugarRemitido { get; set; }
        public DateTime fecha { get; set; }
    }
}