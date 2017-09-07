using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class DocumentoGeneralVM
    {
        public IngresoClinica ingresoClinica { get; set; }
        public Paciente paciente { get; set; }
        public Remision remision { get; set; }
        public Remitido remitido { get; set; }
        public Consultante consultante { get; set; }
        public IngresoEstrategiasEvaluacion estrategiaEva { get; set; }
        public Consulta consulta { get; set; }
        public List<IngresoEstrategiasEvaluacion> estrategiasIngreso { get; set; }
        public string diagnosticos { get; set; }
        public string categorizacionCAP { get; set; }

        public DocumentoGeneralVM() {
            ingresoClinica = new IngresoClinica();
            paciente = new Paciente();
            remision = new Remision();
            remitido = new Remitido();
            consultante = new Consultante();
            estrategiaEva = new IngresoEstrategiasEvaluacion();
            consulta = new Consulta();
            consulta.numeroSesion = 1;
            estrategiasIngreso = new List<IngresoEstrategiasEvaluacion>();
        }

    }
}