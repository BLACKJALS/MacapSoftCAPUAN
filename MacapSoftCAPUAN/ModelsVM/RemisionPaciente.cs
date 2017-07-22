using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class RemisionPaciente
    {
        public Paciente paciente { get; set; }
        public List<Remision> remision { get; set; }


        public RemisionPaciente()
        {
            paciente = new Paciente();
            remision = new List<Remision>();
        }
    }
}