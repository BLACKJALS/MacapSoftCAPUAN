using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.ModelsVM
{
    public class ListadoHistoriasClinicas
    {
        public string numeroHC { get; set; }
        public string nombrePaciente { get; set; }
        public string apellidoPaciente { get; set; }
        public string idUser { get; set; }
    }
}