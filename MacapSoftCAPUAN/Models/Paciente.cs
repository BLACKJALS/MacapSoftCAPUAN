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
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string sexo { get; set; }
        public TiposDocumentos tipoDocumento { get; set; }//Virtual
        public DateTime? fechaNacimiento { get; set; }
        public string lugarNacimiento { get; set; }
        public int edad { get; set; }
        public string direccion { get; set; }
        public Barrios barrio { get; set; } //Virtual
        public Localidades localidad { get; set; } //Virtual
        public Estrato estrato { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public Eps eps { get; set; }//
        public Profesion profesion { get; set; }
        public string ocupacion { get; set; }
        public NivelEscolaridad nivelEscolaridad { get; set; }
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