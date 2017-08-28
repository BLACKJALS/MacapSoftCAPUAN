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
        [ForeignKey("IngresoClinica")]
        public long id_ingresoClinica { get; set; }
        [ForeignKey("User")]
        public string usuario { get; set; }

        public string motivo { get; set; }
        public DateTime? fechaInasistencia { get; set; }
        public IngresoClinica IngresoClinica { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public string usuario { get; set; }
    }
}