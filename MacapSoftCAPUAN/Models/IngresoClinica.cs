using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.IngresoClinica")]
    public class IngresoClinica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idIngresoClinica { get; set; }
        [ForeignKey("IdPaciente")]
        public string id_paciente { get; set; }
        [ForeignKey("EntidadRemitente")]
        public int? id_entidadRemit { get; set; }
        [ForeignKey("Estrato")]
        public int id_estrato { get; set; }
        [ForeignKey("TipoDocumento")]
        public string id_tipoDocumento { get; set; }
        [ForeignKey("Barrio")]
        public string id_barrio { get; set; }
        [ForeignKey("Localidad")]
        public string id_localidad { get; set; }
        [ForeignKey("NivelEscolaridad")]
        public int id_NivelEscolaridad { get; set; }
        [ForeignKey("Ocupacion")]
        public long id_ocupacion { get; set; }
        [ForeignKey("Eps")]
        public string id_Eps { get; set; }

        public int edad { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string profesion { get; set; }
        public string activo { get; set; }
        public string antecedentes { get; set; }
        public string estadoCivil { get; set; }
        public string religion { get; set; }
        public string idUser { get; set; }
        public string problematicaActual { get; set; }
        public string historiaPersonal { get; set; }
        public string historiaFamiliar { get; set; }
        public string genograma { get; set; }
        public string tieneEps { get; set; }
        public string tieneEpc { get; set; }
        public string numeroDocumento { get; set; }
        public bool estadoHC { get; set; }

        public virtual Eps Eps { get; set; }//Se agregó el virtual 
        public virtual Ocupacion Ocupacion { get; set; }
        public virtual NivelEscolaridad NivelEscolaridad { get; set; } //Se agregó el virtual
        public virtual TiposDocumentos TipoDocumento { get; set; }//Se agregó el virtual
        public virtual Barrios Barrio { get; set; } //Se agregó el virtual
        public virtual Localidades Localidad { get; set; }//Se agregó el virtual
        public virtual Estrato Estrato { get; set; }//Se agregó el virtual
        public virtual Paciente IdPaciente { get; set; }
        public virtual Remitido EntidadRemitente { get; set; }

        public DateTime fechaIngreso { get; set; }
        public string motivoConsulta { get; set; }
        public string observaciones { get; set; }
        public string idUsuario { get; set; }
        public string remitido { get; set; }//cambiar la propiedad a foranea

        //public Consulta Consulta { get; set; }
        //[ForeignKey("Consulta")]
        //public int? id_consulta { get; set; }
        public string especialidadRemitente { get; set; }
        public DateTime fechaRemision { get; set; }

        public IngresoClinica() {
            estadoHC = false;
        }

    }
}