using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.motivoCierreHC")]
    public class MotivoCierreHistoriaClinica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idMotivoCierre { get; set; }
        [ForeignKey("MotivoCierre")]
        public int id_MotivoCierre { get; set; }
        [ForeignKey("CierreHC")]
        public long id_Cierre { get; set; }

        public MotivosCierre MotivoCierre { get; set; }
        public CierreHC CierreHC { get; set; }
    }
}