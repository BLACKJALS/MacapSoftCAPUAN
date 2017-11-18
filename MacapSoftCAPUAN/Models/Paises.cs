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
        [Key, MaxLength(20)]
        public string idPais { get; set; }
        public string nombrePais { get; set; }
        //public virtual ICollection<Ciudades> ciudades { get; set; }
        //public Paises() {
        //    this.ciudades = new HashSet<Ciudades>();
        //}
    }

    [Table("dbo.Departamentos")]
    public class Departamentos {
        [Key, MaxLength(50)]
        public string idDepartamento { get; set; }
        [ForeignKey("Pais")]
        public string id_pais { get; set; }

        public string nombre { get; set; }
        public Paises Pais { get; set; }
    }


    [Table("dbo.Ciudades")]
    public class Ciudades
    {
        [Key, MaxLength(20)]
        public string idCiudad { get; set; }
        [ForeignKey("Departamento")]
        public string id_Departamento { get; set; }

        public string nombre { get; set; }
        public Departamentos Departamento { get; set; }
    }



    [Table("dbo.Localidades")]
    public class Localidades
    {
        [Key, MaxLength(20)]
        public string idLocalidad { get; set; }
        //[ForeignKey("Ciudad")]
        //public string id_ciudad { get; set; }

        public string nombre { get; set; }
        //public Ciudades Ciudad { get; set; }
    }

    [Table("dbo.Barrios")]
    public class Barrios{
        [Key, MaxLength(20)]
        public string idBarrio { get; set; }
        [ForeignKey("Localidades")]
        public string id_localidad { get; set; }

        public string nombre { get; set; }
        public Localidades Localidades { get; set; }
    }

    

}