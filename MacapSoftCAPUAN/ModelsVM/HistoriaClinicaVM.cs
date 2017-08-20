using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class HistoriaClinicaVM
    {
        public Paciente paciente { get; set; }
        public IngresoClinica ingresoClinica { get; set; }

        public HistoriaClinicaVM() {
            paciente = new Paciente();
            ingresoClinica = new IngresoClinica();
        }
    }
}