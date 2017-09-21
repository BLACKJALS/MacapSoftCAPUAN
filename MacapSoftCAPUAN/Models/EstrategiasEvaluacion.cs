using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacapSoftCAPUAN.Models
{
    //[Table("dbo.estrategiasEvaluacion")]
    //public class EstrategiasEvaluacion
    //{
    //    [Key]
    //    [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
    //    public int idEstrategiaEvaluacion { get; set; }
    //    public string nombre { get; set; }
    //}


    [Table("dbo.ingresoEstrategias")]
    public class IngresoEstrategiasEvaluacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idEstrategiaEvaluacion { get; set; }
        [ForeignKey("IngresoClinica")]
        public long? id_ingreso { get; set; }

        [AllowHtml]
        public string pruebasPsico { get; set; }

        [AllowHtml]
        public string cuestionarios { get; set; }

        [AllowHtml]
        public string pruebasProyectivas { get; set; }

        [AllowHtml]
        public string examenMental { get; set; }

        [AllowHtml]
        public string entrevistas { get; set; }
        //[ForeignKey("EstrategiaEv")]
        //public int? id_estrategiEv { get; set; }
        //public string nombre { get; set; }

        public IngresoClinica IngresoClinica { get; set; }
        //public EstrategiasEvaluacion EstrategiaEv { get; set; }
    }


    [Table("dbo.consultas")]
    public class Consulta {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idConsulta { get; set; }
        //[ForeignKey("EstrategiasE")]
        //public int? id_estrategiasE { get; set; }
        [ForeignKey("IngresoClinica")]
        public long? id_ingresoClinica { get; set; }
        //[ForeignKey("Consultante")]
        //public string id_consultante { get; set; }
        [ForeignKey("User")]
        public string id_User { get; set; }

        public DateTime? fecha { get; set; }
        [AllowHtml]
        public string objetivoSesion { get; set; }

        [AllowHtml]
        public string ejerciciosEvento { get; set; }

        [AllowHtml]
        public string desarrolloTemasTratados { get; set; }

        [AllowHtml]
        public string tareasProximaSesion { get; set; }

        [AllowHtml]
        public string reciboPago { get; set; }

        [AllowHtml]
        public string resultadoAutoevaluacion { get; set; }

        [AllowHtml]
        public string hipotesisPsicologica { get; set; }

        [AllowHtml]
        public string objetivosTerapeuticos { get; set; }

        [AllowHtml]
        public string estrategiasTecnicasTerapeuticas { get; set; }

        [AllowHtml]
        public string logrosAlcanzadosSegunObjetivosTerapeuticos { get; set; }

        [AllowHtml]
        public string logrosAlcanzadosSegunConsultante { get; set; }

        [AllowHtml]
        public string resumen { get; set; }

        [AllowHtml]
        public string observacionesRecomendaciones { get; set; }

        public int numeroSesion { get; set; }
        //public EstrategiasEvaluacion EstrategiasE { get; set; }
        public IngresoClinica IngresoClinica { get; set; }
        public virtual ApplicationUser User { get; set; }
        //public string usuario { get; set; }
        //public Consultante Consultante { get; set; }
    }
}