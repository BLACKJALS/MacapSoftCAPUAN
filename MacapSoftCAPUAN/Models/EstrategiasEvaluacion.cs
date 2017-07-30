using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.estrategiasEvaluacion")]
    public class EstrategiasEvaluacion
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idEstrategiaEvaluacion { get; set; }
        public string nombre { get; set; }
    }


    [Table("dbo.consultas")]
    public class Consulta {

        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int idConsulta { get; set; }
        [ForeignKey("EstrategiasE")]
        public int id_estrategiasE { get; set; }
        [ForeignKey("Paciente")]
        public string id_paciente { get; set; }
        [ForeignKey("Consultante")]
        public string id_consultante { get; set; }

        public DateTime? fecha { get; set; }
        public string objetivoSesion { get; set; }
        public string ejerciciosEvento { get; set; }
        public string desarrolloTemasTratados { get; set; }
        public string tareasProximaSesion { get; set; }
        public string reciboPago { get; set; }
        public string resultadoAutoevaluacion { get; set; }
        public string hipotesisPsicologica { get; set; }
        public string objetivosTerapeuticos { get; set; }
        public string estrategiasTecnicasTerapeuticas { get; set; }
        public string logrosAlcanzadosSegunObjetivosTerapeuticos { get; set; }
        public string logrosAlcanzadosSegunConsultante { get; set; }
        public string resumen { get; set; }
        public string observacionesRecomendaciones { get; set; }
        public EstrategiasEvaluacion EstrategiasE { get; set; }
        public Paciente Paciente { get; set; }
        public string usuario { get; set; }
        public Consultante Consultante { get; set; }
    }
}