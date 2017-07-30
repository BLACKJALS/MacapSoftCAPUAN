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
        [ForeignKey("Paciente")]
        public string id_paciente { get; set; }
        [ForeignKey("Consulta")]
        public int id_consulta { get; set; }
        [ForeignKey("MotivoCierre")]
        public int id_motivoCierre { get; set; }
        public int MyProperty { get; set; }
        public DateTime fechaInicioPsicoterapia { get; set; }
        public DateTime fechaFinalizaionPsicoterapia { get; set; }
        public string numeroCitasAsignadas { get; set; }
        public string numeroSesionesRealizadas { get; set; }
        public string idUsuario { get; set; }
        public Consulta Consulta { get; set; }
        public Paciente Paciente { get; set; }
        public MotivosCierre MotivoCierre { get; set; }

    }

    [Table("dbo.motivosCierre")]
    public class MotivosCierre {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idMotivoCierre { get; set; }
        public string Nombre { get; set; }
    }
}