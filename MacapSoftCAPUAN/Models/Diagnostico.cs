using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Diagnosticos")]
    public class Diagnostico
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long ID { get; set; }
        [Key]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Destacado { get; set; }
    }
}