using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Paciente")]
    public class Paciente
    {
        [Key]
        public string numeroHistoriaClinica { get; set; }
        //[ForeignKey("Paises")]
        //public string id_paises { get; set; }
        [ForeignKey("Ciudad")]
        public string id_ciudad { get; set; }
        [ForeignKey("Sexo")]
        public int id_sexo { get; set; }
        [ForeignKey("ConsecutivoID")]
        public int consecutivo { get; set; }

        [AllowHtml]
        public string nombre { get; set; }

        [AllowHtml]
        public string apellido { get; set; }

        public bool estadoHC { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public virtual Ciudades Ciudad { get; set; }
        public virtual Consecutivo ConsecutivoID { get; set; }
        //public virtual Paises Paises { get; set; }//Se agregó el virtual
        //public virtual Barrios Barrio { get; set; } //Se agregó el virtual
        //public virtual Localidades Localidad { get; set; }//Se agregó el virtual
        public virtual Sexo Sexo { get; set; }
        

        public Paciente()
        {
            estadoHC = false;
        }
    }
}