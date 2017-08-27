using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Remisiones")]
    public class Remision
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idRemision { get; set; }
        [ForeignKey("IngresoClinica")]
        public long id_ingresoClinica { get; set; }
        [ForeignKey("Motivo_Remision")]
        public int? id_motivoRemision { get; set; }
        [ForeignKey("Diagnostico_tab")]
        public string id_diagnostico { get; set; }
        [ForeignKey("User")]
        public string id_User { get; set; }
        public string nombreInsitucionRemitida { get; set; }
        public string servicioRemitido { get; set; }
        public string evolucionPaciente { get; set; }
        public string aspectosPositivos { get; set; }
        public string recomendaciones { get; set; }
        public DateTime fechaRemitido { get; set; }
        //public string usuario { get; set; }
        public string nombreProfesional { get; set; }
        public string diagnostico { get; set; }
        public int motivoRemision { get; set; }
        //public bool estadoRemision { get; set; }

        public virtual IngresoClinica IngresoClinica { get; set; }
        public virtual MotivosRemisiones Motivo_Remision { get; set; }
        public virtual Diagnostico Diagnostico_tab { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public string direccion { get; set; }
        //public string telefono { get; set; }

        //public int serviSoli { get; set; }
        //public string accionesEmprendidas { get; set; }
        //public string expectativasServicio { get; set; }
        //public string programacionPsicologo { get; set; }
        //public string anexos { get; set; }
        //public string listaEspera { get; set; }
        //public string remitido { get; set; }
        //public string personalizado { get; set; }
        ////public string psicologoAgendado { get; set; }        
    }


    [Table("dbo.servicioSolicitado")]
    public class ServicioSolicitado {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idServicioSolicitado { get; set; }
        public string nombre { get; set; }
    }


    [Table("dbo.motivosRemisiones")]
    public class MotivosRemisiones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idMotivoRemision { get; set; }
        public string nombre { get; set; }
    }

}