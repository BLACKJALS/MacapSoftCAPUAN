using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class CierreCasoVM
    {
        public Paciente paciente { get; set; }
        public IngresoClinica ingresoClinica { get; set; }
        public Remision remision { get; set; }
        public CierreHC cierreHC { get; set; }
        public Consulta consulta { get; set; }
        public MotivosCierre motivosCierre { get; set; }

        public CierreCasoVM() {
            paciente = new Paciente();
            ingresoClinica = new IngresoClinica();
            remision = new Remision();
            cierreHC = new CierreHC();
            consulta = new Consulta();
            motivosCierre = new MotivosCierre();
        }
    }
}