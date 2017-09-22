using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Eps")]
    public class Eps
    {
        [Key, MaxLength(50)]
        public string IdEPS { get; set; }
        public string nombre { get; set; }
    }
}