using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.CategorizacionHC")]
    public class CategorizacionHC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id_CategorizacionHC { get; set; }
        [ForeignKey("Categorizacion")]
        public long id_Categorizacion { get; set; }
        [ForeignKey("IngresoClinica")]
        public long id_IngresoClinica { get; set; }

        public CategorizacionCAP Categorizacion { get; set; }
        public IngresoClinica IngresoClinica { get; set; }
    }
}