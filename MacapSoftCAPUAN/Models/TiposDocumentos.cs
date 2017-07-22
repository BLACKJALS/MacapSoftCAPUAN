﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.tipos_documentos")]
    public class TiposDocumentos
    {
        [Key]
        public string idDocumento { get; set; }
        public string tipo { get; set; }
    }
}