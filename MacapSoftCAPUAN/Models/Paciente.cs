using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.Paciente")]
    public class Paciente
    {
        [Key]
        public string numeroDocumento { get; set; }
        [ForeignKey("TipoDocumento")]
        public string id_tipoDocumento { get; set; }
        [ForeignKey("Paises")]
        public string id_paises { get; set; }
        [ForeignKey("Barrio")]
        public string id_barrio { get; set; }
        [ForeignKey("Localidad")]
        public string id_localidad { get; set; }
        [ForeignKey("Ciudad")]
        public string id_ciudad { get; set; }
        [ForeignKey("Eps")]
        public string id_Eps { get; set; }
        [ForeignKey("Profesion")]
        public int id_profesion { get; set; }
        [ForeignKey("NivelEscolaridad")]
        public int id_NivelEscolaridad { get; set; }
        [ForeignKey("Estrato")]
        public int id_estrato { get; set; }
        //[ForeignKey("Consecutivo")]
        public int consecutivo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string sexo { get; set; }
        public virtual TiposDocumentos TipoDocumento { get; set; }//Se agregó el virtual
        public DateTime? fechaNacimiento { get; set; }
        public virtual Ciudades Ciudad { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public virtual Paises Paises { get; set; }//Se agregó el virtual
        public virtual Barrios Barrio { get; set; } //Se agregó el virtual
        public virtual Localidades Localidad { get; set; }//Se agregó el virtual
        //public virtual Consecutivo Consecutivo { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public virtual Eps Eps { get; set; }//Se agregó el virtual 
        public virtual Profesion Profesion { get; set; }                //Se agregó el virtual
        public string ocupacion { get; set; }                           
        public virtual NivelEscolaridad NivelEscolaridad { get; set; } //Se agregó el virtual
        public string activo { get; set; }
        public string antecedentes { get; set; }
        //public Inasistencias inasistencias { get; set; }
        public string estadoCivil { get; set; }
        public string religion { get; set; }
        public string idUser { get; set; }
        public string problematicaActual { get; set; }
        public string historiaPersonal { get; set; }
        public string historiaFamiliar { get; set; }
        public string genograma { get; set; }
        public string tieneEps { get; set; }
        public string tieneEpc { get; set; }
        public virtual Estrato Estrato { get; set; }//Se agregó el virtual
        //public virtual ICollection<TiposDocumentos> documentos { get; set; }
        //public virtual ICollection<Barrios> barrios { get; set; }
        //public virtual ICollection<Localidades> localidades { get; set; }
        //public virtual ICollection<Remitido> remitido { get; set; }

        public Paciente()
        {
            //barrio = new Barrios();
            //localidad = new Localidades();
            //eps = new Eps();
            //Paises paises = new Paises();
            //remitido = new Remitido();
            //Remision remision = new Remision();
            //IngresoClinica ingreso = new IngresoClinica();
        }
    }
}