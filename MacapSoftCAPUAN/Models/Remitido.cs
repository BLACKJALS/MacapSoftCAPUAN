using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.EntidadRemitente")]
    public class Remitido
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idRemitente { get; set; }

        [AllowHtml]
        public string nombreEntidad { get; set; }
        public DateTime fechaRemision { get; set; }

        [AllowHtml]
        public string nombreRemitente { get; set; }
        [ForeignKey("ingresoCl")]
        public long id_ingresoCl { get; set; }
        public IngresoClinica ingresoCl { get; set; }
    }
}