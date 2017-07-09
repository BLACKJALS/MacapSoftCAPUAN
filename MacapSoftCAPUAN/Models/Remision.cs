using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Remisiones")]
    public class Remision
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idRemision { get; set; }
        public string motivoRemision { get; set; }
        public string nombreInsitucionRemitida { get; set; }
        public string anexos { get; set; }
        public DateTime fechaRemitido { get; set; }
        public string diagnostico { get; set; }
        public string psicologoAgendado { get; set; }
        public string telefono { get; set; }
        public string listaEspera { get; set; }
        public string remitido { get; set; }
        public string programacionPsicologo { get; set; }
        public string personalizado { get; set; }
        public virtual Paciente paciente { get; set; }

        public Remision() {
            //paciente = new Paciente();
        }
    }

}