using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Consultante")]
    public class Consultante
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long id_Consultante { get; set; }
        public string cedula { get; set; }
        [ForeignKey("TipoDocumento")]
        public string id_tipoDocumento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public TiposDocumentos TipoDocumento { get; set; }
        public string numeroDocumentoPaciente { get; set; }
        public string telefono { get; set; }
        public string parentezco { get; set; }
        //public virtual ICollection<TiposDocumentos> documentos { get; set; }

        public Consultante()
        {
            //this.documentos = new HashSet<TiposDocumentos>();
        }
    }

    //[Table("dbo.consultantePaciente")]
    //public class consultantePaciente
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int idConsultantePaciente { get; set; }
    //    [ForeignKey("IdConsultante")]
    //    public long id_Consultante { get; set; }
    //    [ForeignKey("IdPaciente")]
    //    public string id_Paciente { get; set; }

    //    public Consultante IdConsultante { get; set; }
    //    public Paciente IdPaciente { get; set; }
    //}

    [Table("dbo.parentezco")]
    public class parentezco {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idParentezco { get; set; }
        public string nombre { get; set; }
    }
}