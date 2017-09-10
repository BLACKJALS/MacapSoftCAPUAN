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
        public long idCierreHC { get; set; }
        [ForeignKey("IngresoClinica")]
        public long id_ingresoClinica { get; set; }
        //[ForeignKey("Consulta")]
        //public int? id_consulta { get; set; }
        [ForeignKey("MotivoCierre")]
        public int? id_motivoCierre { get; set; }
        [ForeignKey("User")]
        public string id_UsuarioCierraCaso { get; set; }

        public DateTime fechaInicioPsicoterapia { get; set; }
        public DateTime fechaFinalizaionPsicoterapia { get; set; }
        public string numeroCitasAsignadas { get; set; }
        public string numeroSesionesRealizadas { get; set; }
        public string idUsuario { get; set; }
        public string especificacionMotivoCierre { get; set; }
        //public Consulta Consulta { get; set; }
        public IngresoClinica IngresoClinica { get; set; }
        public MotivosCierre MotivoCierre { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public virtual ApplicationUser User { get; set; }
    }

    

    [Table("dbo.motivosCierre")]
    public class MotivosCierre {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idMotivoCierre { get; set; }
        public string Nombre { get; set; }
    }
}