using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.ConsultaDiagnostico")]
    public class ConsultaDiagnostico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idConsultaDiagnostico { get; set; }
        [ForeignKey("Consulta")]
        public int id_consulta { get; set; }
        [ForeignKey("Diagnostico")]
        public string id_diagnostico { get; set; }

        public virtual Consulta Consulta { get; set; }
        public virtual Diagnostico Diagnostico { get; set; }
    }
}