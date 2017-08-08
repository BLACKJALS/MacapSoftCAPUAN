using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.nivelEscolaridad")]
    public class NivelEscolaridad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idNivelEscolaridad { get; set; }
        public string nombre { get; set; }
    }

    //[Table("dbo.profesion")]
    //public class Profesion
    //{
    //    [Key]
    //    public int idProfesion { get; set; }
    //    public string nombre { get; set; }
    //}

    [Table("dbo.estrato")]
    public class Estrato
    {
        [Key]
        public int idEstrato { get; set; }
        public string numeroEstrato { get; set; }
    }
}