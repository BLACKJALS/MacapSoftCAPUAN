using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Paises")]
    public class Paises
    {
        [Key]
        public string idPais { get; set; }
        public string nombrePais { get; set; }
        //public virtual ICollection<Ciudades> ciudades { get; set; }
        //public Paises() {
        //    this.ciudades = new HashSet<Ciudades>();
        //}
    }

    [Table("dbo.Ciudades")]
    public class Ciudades
    {
        [Key]
        public string idCiudad { get; set; }
        [ForeignKey("Pais")]
        public string id_pais { get; set; }

        public string nombre { get; set; }
        public Paises Pais { get; set; }
    }



    [Table("dbo.Localidades")]
    public class Localidades
    {
        [Key]
        public string idLocalidad { get; set; }
        //[ForeignKey("Ciudad")]
        //public string id_ciudad { get; set; }

        public string nombre { get; set; }
        //public Ciudades Ciudad { get; set; }
    }

    [Table("dbo.Barrios")]
    public class Barrios{
        [Key]
        public string idBarrio { get; set; }
        [ForeignKey("Localidades")]
        public string id_localidad { get; set; }

        public string nombre { get; set; }
        public Localidades Localidades { get; set; }
    }

    

}