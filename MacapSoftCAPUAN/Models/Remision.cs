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
        public string nombreInsitucionRemitida { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string nombreProfesional { get; set; }
        public ServicioSolicitado serviSoli { get; set; }
        public string accionesEmprendidas { get; set; }
        public string expectativasServicio { get; set; }
        public DateTime fechaRemitido { get; set; }
        public string usuario { get; set; }
        public Diagnostico diagnostico { get; set; }
        public MotivosRemisiones motivoRemision { get; set; }
        public Paciente paciente { get; set; }

        public string programacionPsicologo { get; set; }
        public string anexos { get; set; }
        public string listaEspera { get; set; }
        public string remitido { get; set; }
        public string personalizado { get; set; }
        //public string psicologoAgendado { get; set; }        
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