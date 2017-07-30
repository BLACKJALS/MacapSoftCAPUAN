using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.IngresoClinica")]
    public class IngresoClinica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idIngresoClinica { get; set; }
        [ForeignKey("IdPaciente")]
        public string id_paciente { get; set; }
        [ForeignKey("EntidadRemitente")]
        public int? id_entidadRemit { get; set; }
        [ForeignKey("Consulta")]
        public int? id_consulta { get; set; }

        public DateTime fechaIngreso { get; set; }
        public string motivoConsulta { get; set; }
        public string observaciones { get; set; }
        public Paciente IdPaciente { get; set; }
        public string idUsuario { get; set; }
        public string remitido { get; set; }//cambiar la propiedad a foranea
        public Remitido EntidadRemitente { get; set; }
        public Consulta Consulta { get; set; }

        public string especialidadRemitente { get; set; }
        public DateTime fechaRemision { get; set; }
    }
}