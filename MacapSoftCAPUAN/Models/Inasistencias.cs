using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.inasistencias")]
    public class Inasistencias
    {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idInasistencias { get; set; }
        [ForeignKey("Paciente")]
        public string id_paciente { get; set; }

        public string motivo { get; set; }
        public DateTime? fechaInasistencia { get; set; }
        public Paciente Paciente { get; set; }
        public string usuario { get; set; }
    }
}