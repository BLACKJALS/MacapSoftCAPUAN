using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Consultante")]
    public class Consultante
    {
        [Key, MaxLength(20)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public long id_Consultante { get; set; }
        public string cedula { get; set; }
        [ForeignKey("TipoDocumento")]
        public string id_tipoDocumento { get; set; }
        [ForeignKey("Sexo")]
        public int? id_sexo { get; set; }

        [AllowHtml]
        public string nombre { get; set; }

        [AllowHtml]
        public string apellido { get; set; }

        public TiposDocumentos TipoDocumento { get; set; }

        public virtual Sexo Sexo { get; set; }

        [AllowHtml]
        public string numeroDocumentoPaciente { get; set; }

        [AllowHtml]
        public string telefono { get; set; }

        [AllowHtml]
        public string parentezco { get; set; }
        //public virtual ICollection<TiposDocumentos> documentos { get; set; }

        public Consultante()
        {
            //this.documentos = new HashSet<TiposDocumentos>();
        }
    }


    [Table("dbo.parentezco")]
    public class parentezco {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idParentezco { get; set; }
        public string nombre { get; set; }
    }
}