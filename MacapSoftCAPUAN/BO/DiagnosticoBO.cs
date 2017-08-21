using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.BO
{
    public class DiagnosticoBO
    {
        DiagnosticosDALC dia;

        public List<Diagnostico> listarDiagnostico() {
            dia = new DiagnosticosDALC();
            return dia.ListarDiagnosticos();
        }

    }
}