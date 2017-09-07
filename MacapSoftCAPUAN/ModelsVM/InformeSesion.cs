using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class InformeSesion
    {
        public Consulta consulta { get; set; }

        public InformeSesion() {
            consulta = new Consulta();
        }
    }
}