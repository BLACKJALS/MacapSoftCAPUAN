using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.Models
{
    [Table("dbo.permisosUsuariosPacientes")]
    public class PermisosUsuariosPaciente
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public long idPermisosUsuario { get; set; }
        [ForeignKey("Paciente")]
        public string id_paciente { get; set; }
        [ForeignKey("AplicationUser")]
        public string id_aplicationUser { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual ApplicationUser  AplicationUser { get; set; }
    }
}