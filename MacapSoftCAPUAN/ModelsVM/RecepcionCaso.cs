using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class RecepcionCaso
    {
        public Paciente paciente { get; set; }
        public Consultante consultante { get; set; }
        public Remitido remitido { get; set; }
        public Remision remision { get; set; }
        public consultantePaciente consultantePaciente { get; set; }
        public IngresoClinica ingresoClinica { get; set; }
        public Consecutivo consecutivo { get; set; }
        //public Eps epsObj { get; set; }


        public RecepcionCaso() {
            paciente = new Paciente();
            consultantePaciente = new consultantePaciente();
            remitido = new Remitido();
            remision = new Remision();
            consultante = new Consultante();
            ingresoClinica = new IngresoClinica();
            consecutivo = new Consecutivo();
        }
    }
}