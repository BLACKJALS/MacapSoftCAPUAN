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
        public string cedula { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public virtual TiposDocumentos tipoDocumento { get; set; }
        public string telefono { get; set; }
        public string parentezco { get; set; }
        //public virtual ICollection<TiposDocumentos> documentos { get; set; }

        public Consultante()
        {
            //this.documentos = new HashSet<TiposDocumentos>();
        }
    }

    [Table("dbo.consultantePaciente")]
    public class consultantePaciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idConsultantePaciente { get; set; }
        public virtual Consultante idConsultante { get; set; }
        public virtual Paciente idPaciente { get; set; }
    }
}