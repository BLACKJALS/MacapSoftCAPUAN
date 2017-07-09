using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN
{
    public class Mapeo
    {
        public DiagnosticoVM MapeoDiagnostico(Diagnostico diagnostico)
        {
            DiagnosticoVM diagVM = new DiagnosticoVM();
            diagVM.Codigo = diagnostico.Codigo;
            diagVM.Nombre = diagnostico.Nombre;
            diagVM.Destacado = diagnostico.Destacado;
            return diagVM;
        }
    }
}