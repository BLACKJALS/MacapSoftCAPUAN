using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{

    [Table("dbo.cierresHC")]
    public class CierreHC
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCierreHC { get; set; }
        public DateTime fechaInicioPsicoterapia { get; set; }
        public DateTime fechaFinalizaionPsicoterapia { get; set; }
        public string numeroCitasAsignadas { get; set; }
        public string numeroSesionesRealizadas { get; set; }
        public string idUsuario { get; set; }
        public Consulta Consulta { get; set; }
        public Paciente paciente { get; set; }
        public MotivosCierre motivoCierre { get; set; }

    }

    [Table("dbo.motivosCierre")]
    public class MotivosCierre {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idMotivoCierre { get; set; }
        public string Nombre { get; set; }
    }
}