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
        public virtual ICollection<Ciudades> ciudades { get; set; }
        public Paises() {
            this.ciudades = new HashSet<Ciudades>();
        }
    }

    [Table("dbo.Ciudades")]
    public class Ciudades
    {
        [Key]
        public string idCiudad { get; set; }
        public string nombre { get; set; }
        //public long idPais { get; set; }
        public virtual Paises paises { get; set; }
        public virtual ICollection<Localidades> localidades { get; set; }

        public Ciudades()
        {
            this.localidades = new HashSet<Localidades>();
        }
    }

    [Table("dbo.Localidades")]
    public class Localidades
    {
        [Key]
        public string idLocalidad { get; set; }
        public string nombre { get; set; }
        //public long idCiudad { get; set; }
        public virtual Ciudades ciudades { get; set; }
        public virtual ICollection<Barrios> barrios { get; set; }
        public Localidades()
        {
            this.barrios = new HashSet<Barrios>();
        }
    }

    [Table("dbo.Barrios")]
    public class Barrios{
        [Key]
        public string idBarrio { get; set; }
        public string nombre { get; set; }
        //public long idLocalidad { get; set; }
        public virtual Localidades localidades { get; set; }
    }

    

}