using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace MacapSoftCAPUAN.DALC
{
    public class HistoriaClinicaDALC
    {
        public ApplicationDbContext bd;
        public ApplicationDbContext context;
        public List<Barrios> listarBarrios()
        {
            bd = new ApplicationDbContext();
            List<Barrios> listaBarrios = new List<Barrios>();
            listaBarrios = bd.barriosContext.ToList();
            return listaBarrios;
        }



        public List<Paciente> listarPacientes() {
            bd = new ApplicationDbContext();
            var listaPacientes = bd.pacienteContext.ToList();
            return listaPacientes;
        }



        public List<Estrato> listaEstratos()
        {
            bd = new ApplicationDbContext();
            var listaEstrato = bd.estratoContext.ToList();
            return listaEstrato;
        }



        public List<TiposDocumentos> listaTipoDocumento() {
            bd = new ApplicationDbContext();
            var listaDocumento = bd.tipoDocumentoContext.ToList();
            return listaDocumento;
        }



        public List<Inasistencias> listaInasistencia()
        {
            bd = new ApplicationDbContext();
            var listaInasistencia = bd.inasistenciasContext.ToList();
            return listaInasistencia;
        }



        public List<Localidades> listarLocalidades()
        {
            bd = new ApplicationDbContext();
            List<Localidades> listaLocalidades = new List<Localidades>();
            listaLocalidades = bd.localidadesContext.ToList();
            return listaLocalidades;
        }



        public List<Ciudades> listarCiudades()
        {
            bd = new ApplicationDbContext();
            List<Ciudades> listaCiudades = new List<Ciudades>();
            listaCiudades = bd.ciudadesContext.ToList();
            return listaCiudades;
        }



        public List<Consulta> listarConsultas()
        {
            bd = new ApplicationDbContext();
            List<Consulta> listaConsulta = new List<Consulta>();
            listaConsulta = bd.consultaContext.ToList();
            return listaConsulta;
        }



        public List<ApplicationUser> listarUsuario() {
            context = new ApplicationDbContext();
            var listaUsuarios = context.Users.ToList();
            return listaUsuarios;
        }

        public List<ApplicationUser> listaUsuarioRolesEstudiantePsicologo() {
            context = new ApplicationDbContext();
            var listRoles = context.Roles.ToList();
            var role = (from item in listRoles where item.Name == "Estudiante psicologo" select item).LastOrDefault();
            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();
            return users;
        }


        public List<ApplicationUser> listaUsuarioRolesDocente()
        {
            context = new ApplicationDbContext();
            var listRoles = context.Roles.ToList();
            var role = (from item in listRoles where item.Name == "Docente" select item).LastOrDefault();
            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();
            return users;
        }


        public List<Paises> listarPaises()
        {
            bd = new ApplicationDbContext();
            List<Paises> listaPaises = new List<Paises>();
            listaPaises = bd.paisesContext.ToList();
            return listaPaises;

        }

        //public List<Profesion> listarProfesion()
        //{
        //    bd = new ApplicationDbContext();
        //    List<Profesion> listaProfesion = new List<Profesion>();
        //    listaProfesion = bd.profesionContext.ToList();
        //    return listaProfesion;

        //}

        public List<Eps> listarEps()
        {
            bd = new ApplicationDbContext();
            List<Eps> listaEps = new List<Eps>();
            listaEps = bd.epsConext.ToList();
            return listaEps;
        }



        public List<Consecutivo> listarConsecutivo() {
            bd = new ApplicationDbContext();
            List<Consecutivo> listaConsecutivo = new List<Consecutivo>();
            listaConsecutivo = bd.consecutivoContext.ToList();
            return listaConsecutivo;
        }



        public void agregarRemision(Remision remision)
        {
                bd = new ApplicationDbContext();
                bd.remisionContext.Add(remision);
                bd.SaveChanges();
        }
        


        public void agregarRemitido(Remitido remitido)
        {
            bd = new ApplicationDbContext();
            bd.remitidoContext.Add(remitido);
            bd.SaveChanges();
        }



        public string agregarIngresoClinica(IngresoClinica ingresoClinica)
        {
            try
            {
                bd = new ApplicationDbContext();
                bd.ingresoClinicaContext.Add(ingresoClinica);
                //bd.Entry(ingresoClinica.EntidadRemitente).State = EntityState.Detached;
                bd.SaveChanges();
                return "Éxito";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("Error al guardar los datos de ingreso.", e.Message);
                return argxEx.ToString();
            }
            
        }



        public List<IngresoClinica> listarIngresoClinica()
        {
            bd = new ApplicationDbContext();
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            listaIngresoClinica = bd.ingresoClinicaContext.ToList();
            return listaIngresoClinica;
        }



        public List<Consultante> listarConsultante()
        {
            bd = new ApplicationDbContext();
            List<Consultante> listaConsultante = new List<Consultante>();
            listaConsultante = bd.consultanteContext.ToList();
            return listaConsultante;
        }



        public List<CategorizacionCAP> listarCategorizacion()
        {
            bd = new ApplicationDbContext();
            List<CategorizacionCAP> listaCategorizacionCAP = new List<CategorizacionCAP>();
            listaCategorizacionCAP = bd.categorizacionCAPContext.ToList();
            return listaCategorizacionCAP;
        }



        public string agregarCierre(CierreHC cierre) {
            try
            {
                bd = new ApplicationDbContext();
                bd.cierreHcContext.Add(cierre);
                bd.SaveChanges();
                return "Cierre creado exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el cierre.", e);
                return argxEx.ToString();
            }
        }



        public string agregarPaciente(Paciente paciente)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.pacienteContext.Add(paciente);
                //bd.Entry(paciente.barrio).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                //bd.Entry(paciente.eps).State = EntityState.Unchanged;
                //bd.Entry(paciente.localidad).State = EntityState.Unchanged;
                //bd.Entry(paciente.tipoDocumento).State = EntityState.Unchanged;
                //bd.Entry(paciente.paises).State = EntityState.Unchanged;
                //bd.Entry(paciente.id_estrato).State = EntityState.Unchanged;
                bd.SaveChanges();
                return "paciente creado existosamente";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente es posible que el numero de historica clínica ya exista.",e);
                return argxEx.ToString();
            }
        }



        public string agregarConsultante(Consultante consultante)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.consultanteContext.Add(consultante);
                //bd.Entry(consultante.tipoDocumento).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                bd.SaveChanges();
                return "Consultante creado existosamente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente ya existe un consultante con ese documento registrado.", e);
                return argxEx.ToString();

            }

        }



        //public string agregarConsultantePaciente(consultantePaciente consultantePa)
        //{

        //    try
        //    {
        //        bd = new ApplicationDbContext();
        //        bd.consultantePacienteContext.Add(consultantePa);
        //        //bd.Entry(consultantePa.idConsultante.tipoDocumento).State = EntityState.Detached;
        //        //bd.Entry(consultantePa.idConsultante).State = EntityState.Unchanged;
        //        bd.Entry(consultantePa.IdConsultante).State = EntityState.Unchanged;
        //        bd.Entry(consultantePa.IdPaciente).State = EntityState.Unchanged;
        //        bd.SaveChanges();
        //        return "ConsultantePaciente creado existosamente";
        //    }
        //    catch (Exception e)
        //    {

        //        System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente ya existe la información del consultante paciente con ese documentos de identidad registrado.", e);
        //        throw argxEx;

        //    }

        //}


        public void agregarConsecutivo(Consecutivo consecutivo)
        {
            bd = new ApplicationDbContext();
            bd.consecutivoContext.Add(consecutivo);
            bd.SaveChanges();
        }



        public string agregarRemisionL(List<Remision> remision)
        {
            try
            {
                bd = new ApplicationDbContext();
                foreach (var item in remision)
                {
                    bd.remisionContext.Add(item);
                }
                bd.SaveChanges();
                return "Se ha creado correctamente la remisión.";
            }
            catch (Exception ex)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear la remisión.", ex.Message);
                return argxEx.ToString();
            }
            
        }



        public List<Remision> listaPacientesRemitidos() {
            bd = new ApplicationDbContext();
            var listaUsuarios = bd.pacienteContext;
            var listaRemisiones = bd.remisionContext.ToList();
            return listaRemisiones;
        }



        public List<MotivosRemisiones> listaMotivosRemisiones()
        {
            bd = new ApplicationDbContext();
            var listaMotivosRemisiones = bd.motivosRemisionesContext.ToList();
            return listaMotivosRemisiones;
        }



        public string modificarPaciente(Paciente paciente)
        {

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var result = db.pacienteContext.SingleOrDefault(b => b.numeroHistoriaClinica == paciente.numeroHistoriaClinica);
                    if (result != null)
                    {
                        result.estadoHC = false;
                        db.SaveChanges();
                    }
                }

                //var pacienteModificado = new Paciente { numeroHistoriaClinica = paciente.numeroHistoriaClinica, estadoHC = false };
                //using (var context = new ApplicationDbContext())
                //{
                //    context.pacienteContext.Attach(pacienteModificado);
                //    pacienteModificado.nombre = "A";
                //    //pacienteModificado.estadoHC = false;
                //    //pacienteModificado.estadoHC = pacienteModificado.estadoHC;
                //    context.SaveChanges();
                //}
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                return argxEx.ToString();
            }
            return "Exito";
        }



        public string remitirModificarPaciente(Paciente paciente)
        {

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var result = db.pacienteContext.SingleOrDefault(b => b.numeroHistoriaClinica == paciente.numeroHistoriaClinica);
                    if (result != null)
                    {
                        result.estadoHC = true;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                return argxEx.ToString();
            }
            return "Exito";
        }

        public List<Remitido> listarRemitido()
        {
            bd = new ApplicationDbContext();
            List<Remitido> listaRemision = new List<Remitido>();
            listaRemision = bd.remitidoContext.ToList();
            return listaRemision;
        }



        public List<Sexo> listarSexoPac()
        {
            bd = new ApplicationDbContext();
            List<Sexo> listaSexo = new List<Sexo>();
            listaSexo = bd.sexoContext.ToList();
            return listaSexo;
        }

        public List<NivelEscolaridad> listarlistaNivelEscolaridad()
        {
            bd = new ApplicationDbContext();
            List<NivelEscolaridad> listaNivelEscolaridad = new List<NivelEscolaridad>();
            listaNivelEscolaridad = bd.nivelEscolaridadContext.ToList();
            return listaNivelEscolaridad;
        }


        public List<Ocupacion> listarOcupacion()
        {
            bd = new ApplicationDbContext();
            List<Ocupacion> listaOcupacion = new List<Ocupacion>();
            listaOcupacion = bd.ocupacionCAPContext.ToList();
            return listaOcupacion;
        }


        public List<CierreHC> listarCierres()
        {
            bd = new ApplicationDbContext();
            List<CierreHC> listaCierres = new List<CierreHC>();
            listaCierres = bd.cierreHcContext.ToList();
            return listaCierres;
        }


        public List<IngresoEstrategiasEvaluacion> listarEstrategiasEvaluacion()
        {
            bd = new ApplicationDbContext();
            List<IngresoEstrategiasEvaluacion> listaEstrategiasEvaluacion = new List<IngresoEstrategiasEvaluacion>();
            listaEstrategiasEvaluacion = bd.ingresoEstrategiasEvaluacionContext.ToList();
            return listaEstrategiasEvaluacion;
        }

        public string modificarCierreHC(IngresoClinica ingresoClinica) {
            try
            {
                var cierreHC = new IngresoClinica { idIngresoClinica = ingresoClinica.idIngresoClinica };
                using (var context = new ApplicationDbContext())
                {
                    context.ingresoClinicaContext.Attach(cierreHC);
                    cierreHC.idIngresoClinica = ingresoClinica.idIngresoClinica;
                    cierreHC.estadoHC = true;
                    cierreHC.estadoRemision = true;
                    //cierreHC.idUsuario = ingresoClinica.idUsuario;
                    context.SaveChanges();
                }

                return "Exito";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo modificar el cierre.", e.Message);
                return argxEx.ToString();
            }

        }


        public string modificarRemision(List<IngresoClinica> ingresoPaciente)
        {
            try
            {
                foreach (var item in ingresoPaciente) {
                    var cierreHC = new IngresoClinica { idIngresoClinica = item.idIngresoClinica };
                    using (var context = new ApplicationDbContext())
                    {
                        context.ingresoClinicaContext.Attach(cierreHC);
                        cierreHC.idIngresoClinica = item.idIngresoClinica;
                        cierreHC.estadoRemision = false;
                        cierreHC.id_paciente = item.id_paciente;
                        cierreHC = item;
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                }
                return "Exito";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo modificar la remision.", e.Message);
                return argxEx.ToString();
            }

        }


        public string modificarIngresoCl(IngresoClinica ingresoCl)
        {

            try
            {
                var ingresoNuevo = new IngresoClinica { idIngresoClinica = ingresoCl.idIngresoClinica};
                using (var context = new ApplicationDbContext())
                {
                    context.ingresoClinicaContext.Attach(ingresoNuevo);
                    ingresoNuevo.estadoCivil = ingresoCl.estadoCivil;
                    ingresoNuevo.religion = ingresoCl.religion;
                    ingresoNuevo.categorizacionCAP = ingresoCl.categorizacionCAP;
                    //ingresoNuevo.diagnostico = ingresoCl.diagnostico;
                    ingresoNuevo.problematicaActual = ingresoCl.problematicaActual;
                    ingresoNuevo.historiaPersonal = ingresoCl.historiaPersonal;
                    ingresoNuevo.antecedentes = ingresoCl.antecedentes;
                    ingresoNuevo.historiaFamiliar = ingresoCl.historiaFamiliar;
                    ingresoNuevo.genograma = ingresoCl.genograma;
                    ingresoNuevo.estadoDocumentoGeneral = ingresoCl.estadoDocumentoGeneral;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar la informacion de ingreso nueva.", e.Message);
                return argxEx.ToString();
            }
            return "Exito";
        }



        public string agregarConsulta(Consulta consulta) {
            try
            {
                bd = new ApplicationDbContext();
                bd.consultaContext.Add(consulta);
                bd.SaveChanges();
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo guardar la consulta creada.", e.Message);
                return argxEx.ToString();
            }
            return "Exito";
        }



        public string agregarEstrategiaEva(IngresoEstrategiasEvaluacion ingresoEstrategia)
        {
            try
            {
                bd = new ApplicationDbContext();
                bd.ingresoEstrategiasEvaluacionContext.Add(ingresoEstrategia);
                bd.SaveChanges();
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo guardar la estrategia creada.", e.Message);
                return argxEx.ToString();
            }
            return "Exito";
        }



        public string agregarInasistencia(Inasistencias inasistencia)
        {
            try
            {
                bd = new ApplicationDbContext();
                bd.inasistenciasContext.Add(inasistencia);
                bd.SaveChanges();
                return "Inasistencia creado";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear la inasistencia.", e);
                return argxEx.ToString();
            }
        }


        public string guardarCategorizacionesCAPDiagnosticoConsultas(List<CategorizacionHC> listaCatHC, List<ConsultaDiagnostico> listaConDiag) {
            try
            {
                bd = new ApplicationDbContext();
                foreach (var item in listaCatHC) {
                    bd.categorizcionHcContext.Add(item);
                    bd.SaveChanges();
                }

                foreach (var item in listaConDiag)
                {
                    bd.consultaDiagnosticoContext.Add(item);
                    bd.SaveChanges();
                }
                return "Proceso Exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear la categorizacion y consulta.", e);
                return argxEx.ToString();
            }
        }


        public string guardarDiagnosticoConsultasInformes(List<ConsultaDiagnostico> listaConDiag)
        {
            try
            {
                foreach (var item in listaConDiag)
                {
                    bd.consultaDiagnosticoContext.Add(item);
                    bd.SaveChanges();
                }
                return "Proceso Exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear la categorizacion y consulta.", e);
                return argxEx.ToString();
            }
        }


        public void asignarUsuariosHC(List<PermisosUsuariosPaciente> permisosUsr)
        {
            bd = new ApplicationDbContext();

            foreach (var item in permisosUsr) {
                bd.permisosUsuariosPacienteContext.Add(item);
                bd.SaveChanges();
            }
        }


        public List<PermisosUsuariosPaciente> listarPermisosUsuariosPac()
        {
            bd = new ApplicationDbContext();
            List<PermisosUsuariosPaciente> listaPermisosUsuariosPac = new List<PermisosUsuariosPaciente>();
            listaPermisosUsuariosPac = bd.permisosUsuariosPacienteContext.ToList();
            return listaPermisosUsuariosPac;
        }



        public List<ConsultaDiagnostico> listarConsultaDiagnostico()
        {
            bd = new ApplicationDbContext();
            List<ConsultaDiagnostico> listaConsultaDiagnostico = new List<ConsultaDiagnostico>();
            listaConsultaDiagnostico = bd.consultaDiagnosticoContext.ToList();
            return listaConsultaDiagnostico;
        }


        public List<CategorizacionHC> listarCategorizacionIngresoClinica()
        {
            bd = new ApplicationDbContext();
            List<CategorizacionHC> listaCategorizacionHC = new List<CategorizacionHC>();
            listaCategorizacionHC = bd.categorizcionHcContext.ToList();
            return listaCategorizacionHC;
        }


        //Sentencia que modifica en base de datos el ingreso clínica
        public string modificarCierreHCIngresoCl(IngresoClinica ingresoClinica)
        {
            try
            {
                var cierreHC = new IngresoClinica { idIngresoClinica = ingresoClinica.idIngresoClinica };
                using (var context = new ApplicationDbContext())
                {
                    context.ingresoClinicaContext.Attach(cierreHC);
                    cierreHC.idIngresoClinica = ingresoClinica.idIngresoClinica;
                    cierreHC.estadoHC = true;
                    context.SaveChanges();
                }

                return "Exito";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo modificar el cierre.", e.Message);
                return argxEx.ToString();
            }

        }


        //Sentencia que modifica en base de datos el cierre de una historia clínica
        public string modificarCierre(CierreHC cierre)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var result = db.cierreHcContext.SingleOrDefault(b => b.idCierreHC == cierre.idCierreHC);
                    if (result != null)
                    {
                        result.fechaFinalizaionPsicoterapia = cierre.fechaFinalizaionPsicoterapia;
                        result.fechaInicioPsicoterapia = cierre.fechaInicioPsicoterapia;
                        result.id_UsuarioCierraCaso = cierre.id_UsuarioCierraCaso;

                        if (result.numeroCitasAsignadas != null)
                        {
                            result.numeroCitasAsignadas = cierre.numeroCitasAsignadas;
                        }

                        if (result.numeroSesionesRealizadas != null)
                        {
                            result.numeroSesionesRealizadas = cierre.numeroSesionesRealizadas;
                        }

                        if (cierre.especificacionMotivoCierre != null)
                        {
                            result.especificacionMotivoCierre = cierre.especificacionMotivoCierre;
                        }
                        db.SaveChanges();
                    }
                }


                //var cierreHC = new CierreHC { idCierreHC = cierre.idCierreHC };
                //using (var context = new ApplicationDbContext())
                //{
                //    context.cierreHcContext.Attach(cierreHC);
                //    cierreHC.idCierreHC = cierre.idCierreHC;
                //    cierreHC.fechaFinalizaionPsicoterapia = cierre.fechaFinalizaionPsicoterapia;
                //    cierreHC.fechaInicioPsicoterapia = cierre.fechaInicioPsicoterapia;
                //    cierreHC.id_UsuarioCierraCaso = cierre.id_UsuarioCierraCaso;

                //    if (cierreHC.numeroCitasAsignadas != null)
                //    {
                //        cierreHC.numeroCitasAsignadas = cierre.numeroCitasAsignadas;
                //    }

                //    if (cierreHC.numeroSesionesRealizadas != null)
                //    {
                //        cierreHC.numeroSesionesRealizadas = cierre.numeroSesionesRealizadas;
                //    }
                    
                //    if (cierre.especificacionMotivoCierre != null) {
                //        cierreHC.especificacionMotivoCierre = cierre.especificacionMotivoCierre;
                //    }
                //    context.SaveChanges();
                //}

                return "Exito";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo modificar el cierre.", e.Message);
                return argxEx.ToString();
            }
        }


        //Agregar motivos de cierre de un cierre
        public void agregarMotivosRemision(List<MotivoCierreHistoriaClinica> listaMotivosRemision)
        {
            bd = new ApplicationDbContext();

            foreach (var item in listaMotivosRemision)
            {
                bd.motivoCierreHcContext.Add(item);
                bd.SaveChanges();
            }
        }


        //Eliminar un permiso a un paciente
        public string eliminarUsuarioAsignadoHC(PermisosUsuariosPaciente permisoUsuarioPaciente) {

            try
            {
                var permisoUsuarioPac = new PermisosUsuariosPaciente { idPermisosUsuario = permisoUsuarioPaciente.idPermisosUsuario };
                using (var context = new ApplicationDbContext())
                {
                    context.permisosUsuariosPacienteContext.Attach(permisoUsuarioPac);
                    context.permisosUsuariosPacienteContext.Remove(permisoUsuarioPac);
                    context.SaveChanges();
                }

                return "Exito";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo eliminar el permiso usuario paciente.", e.Message);
                return argxEx.ToString();
            }


            //bd = new ApplicationDbContext();
            //bd.Entry(permisoUsuarioPaciente.AplicationUser).State = EntityState.Detached;
            //bd.Entry(permisoUsuarioPaciente.Paciente).State = EntityState.Detached;
            //bd.permisosUsuariosPacienteContext.Remove(permisoUsuarioPaciente);
            //bd.SaveChanges();
        }

    }
}