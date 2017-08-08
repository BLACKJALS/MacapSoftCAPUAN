using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.categorizacionCAP")]
    public class CategorizacionCAP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id_CategorizacionCAP { get; set; }
        public string nombre { get; set; }
    }

    [Table("dbo.ocupacion")]
    public class Ocupacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id_Ocupacion { get; set; }
        public string nombre { get; set; }
    }

}