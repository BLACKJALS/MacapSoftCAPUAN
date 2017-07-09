using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Remitente")]
    public class Remitido
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idRemitente { get; set; }
        public string nombreEntidad { get; set; }
        public DateTime fechaRemision { get; set; }
        public string nombreRemitente { get; set; }
    }
}