using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.BO
{
    public class HistoriaClinicaBO
    {
        private HistoriaClinicaDALC hcDALC;
        HistoriaClinicaBO HC;


        public List<Barrios> listarBarrios() {
            hcDALC = new HistoriaClinicaDALC();
            var listaBarrios = hcDALC.listarBarrios();
            return listaBarrios; 
        }



        public List<Localidades> listarLocalidades()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaLicalidades = hcDALC.listarLocalidades();
            return listaLicalidades;
        }



        public List<Ciudades> listarCiudades()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaCiudades = hcDALC.listarCiudades();
            return listaCiudades;
        }



        public List<Paises> listarPaises()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaPaises = hcDALC.listarPaises();
            return listaPaises;
        }

        public List<Eps> listarEps()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaEps = hcDALC.listarEps();
            return listaEps;
        }

        public List<MotivosCierre> listarMotivosCierre()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaMotivosCierre = hcDALC.listarMotivosCierres();
            return listaMotivosCierre;
        }


        public List<MotivoCierreHistoriaClinica> listarMotivoCierreHistoriaClinicaBO()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaMotivosCierre = hcDALC.listarMotivoCierreHistoriaClinica();
            return listaMotivosCierre;
        }

        

        public string agregarpaciente(Paciente paciente)
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarPaciente(paciente);
        }



        public string agregarConsultante(Consultante consultante) {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarConsultante(consultante);
        }
        
        //public string agregarConsultantePaciente(consultantePaciente consultantePa)
        //{
        //    hcDALC = new HistoriaClinicaDALC();
        //    return hcDALC.agregarConsultantePaciente(consultantePa);
        //}

        public void agregarRemision(Remision remision)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemision(remision);
        }

        public string agregarRemitido(Remitido remitido)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.agregarRemitido(remitido);
                return "Creado existoso";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo agregar la remision.", e.Message);
                throw argxEx;
            }
        }

        public List<Paciente> listarPaciente() {
            hcDALC = new HistoriaClinicaDALC();
            var pacientes = hcDALC.listarPacientes().ToList();
            return pacientes;
        }

        public List<TiposDocumentos> listaTiposDocumento()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaDocumento = hcDALC.listaTipoDocumento();
            return listaDocumento;
        }

        public List<Consecutivo> listarConsecutivo() {
            hcDALC = new HistoriaClinicaDALC();
            var listaConsecutivo = hcDALC.listarConsecutivo();
            return listaConsecutivo;
        }


        public List<Estrato> listarEstrato() {
            hcDALC = new HistoriaClinicaDALC();
            var listaEstrato = hcDALC.listaEstratos();
            return listaEstrato;
        }


        public List<CategorizacionCAP> listarCategorizacion()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaCategorizacion = hcDALC.listarCategorizacion();
            return listaCategorizacion;
        }

        public void agregarConsecutivo(Consecutivo consecutivo) {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarConsecutivo(consecutivo);
        }

        public void agregarListaRemision(List<Remision> remision)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemisionL(remision);
        }

        public List<Remision> listarRemisiones() {
            hcDALC = new HistoriaClinicaDALC();
            var listaPr = hcDALC.listaPacientesRemitidos();
            return listaPr;
        }

        public List<MotivosRemisiones> listarMotivosRemisiones()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaMotivosR = hcDALC.listaMotivosRemisiones();
            return listaMotivosR;
        }

        public string modificarPaciente(Paciente paciente)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.modificarPaciente(paciente);
                return "Se ha actualizado correctamente el paciente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                throw argxEx;
            }
            
        }


        public string remitirModificarPaciente(Paciente paciente)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.remitirModificarPaciente(paciente);
                return "Se ha actualizado correctamente el paciente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                throw argxEx;
            }
        }

        

        public string ingresoClinica(IngresoClinica ingresoClinica) {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.agregarIngresoClinica(ingresoClinica);
                return "Se ha creado el ingreso clinica correctamente del paciente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo guardar la información del ingreso del paciente.", e.Message);
                throw argxEx;
            }

        }
        

        public List<IngresoClinica> listarIngresoClinica()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaIngrCl = hcDALC.listarIngresoClinica();
            return listaIngrCl;
        }

        public List<Remitido> listarRemitido() {
            hcDALC = new HistoriaClinicaDALC();
            var listaRem = hcDALC.listarRemitido();
            return listaRem;
        }

        public string modificarIngresoCl(IngresoClinica ingrCl)
        {

            try
            {
                hcDALC = new HistoriaClinicaDALC();
                return hcDALC.modificarIngresoCl(ingrCl);
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                throw argxEx;
            }

        }

        public List<Consultante> listarConsultante() {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarConsultante();
        }

        public List<Sexo> listarSexo()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaSexo = hcDALC.listarSexoPac();
            return listaSexo;
        }


        public List<NivelEscolaridad> listarNivelEscolaridad()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaNivelEscolaridad = hcDALC.listarlistaNivelEscolaridad();
            return listaNivelEscolaridad;
        }

        public List<Ocupacion> listarOcupacion()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaOcupacion = hcDALC.listarOcupacion();
            return listaOcupacion;
        }

        public string agregarCierreHC(CierreHC cierre)
        {

            try
            {
                hcDALC = new HistoriaClinicaDALC();
                return hcDALC.agregarCierre(cierre);
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                return argxEx.ToString();
            }

        }


        public List<CierreHC> listarCierres()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarCierres();
        }

        //Obtiene la lista de usuarios de la aplicación
        public List<ApplicationUser> listarUsuario()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarUsuario();
        }

        //Obtiene la lista de usuarios docente de la aplicación
        public List<ApplicationUser> listarUsuarioDocente()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listaUsuarioRolesDocente();
        }

        //Obtiene la lista de usuarios estudiante de la aplicación
        public List<ApplicationUser> listarUsuarioEstudiante()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listaUsuarioRolesEstudiantePsicologo();
        }

        public List<IngresoEstrategiasEvaluacion> listarIngresoEstrategiasEvaluacion()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarEstrategiasEvaluacion();
        }



        public List<Consulta> listarConsultas()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarConsultas();
        }
        



        public string modificarRecepcionCasoModel(RecepcionCaso recepcionC) {//, Paciente pacienteEx) {
            try
            {
                HC = new HistoriaClinicaBO();
                Remitido remitido = new Remitido();
                IngresoClinica ingresoClinica = new IngresoClinica();
                Paciente paciente = new Paciente();
                var recepCaso = recepcionC;
                //recepCaso.paciente.fechaNacimiento = pacienteEx.fechaNacimiento;
                //recepCaso.paciente.consecutivo = recepcionC.consecutivo.numeroConsecutivo;
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                var usuario = (from item in HC.listarUsuario() where item.Id == user select item).FirstOrDefault();
                recepCaso.ingresoClinica.idUser = usuario.Id;//System.Web.HttpContext.Current.User.Identity.GetUserId();
                //HC.modificarPaciente(recepCaso.paciente);
                //var pacienteIngr = (from item in HC.listarPaciente() where item.numeroDocumento == recepcionC.paciente.numeroDocumento select item).LastOrDefault();
                recepcionC.ingresoClinica.id_paciente = recepcionC.paciente.numeroHistoriaClinica;
                ingresoClinica = recepcionC.ingresoClinica;
                recepcionC.paciente.estadoHC = false;
                paciente = recepcionC.paciente;
                HC.modificarPaciente(paciente);
                
                if (recepcionC.consultante.cedula != null)
                {
                    var existe = (from item in hcDALC.listarConsultante() where recepcionC.consultante.cedula == item.cedula select item).LastOrDefault();
                    if (existe == null)
                    {
                        //recepcionC.consultante.numeroDocumentoPaciente = pacienteIngr.numeroDocumento;
                        HC.agregarConsultante(recepcionC.consultante);
                        ingresoClinica.id_Consultante = recepcionC.consultante.cedula;
                    }
                }
                HC.ingresoClinica(ingresoClinica);
                var listaHCIngreso = HC.listarIngresoClinica().LastOrDefault();
                recepcionC.cierre.id_ingresoClinica = listaHCIngreso.idIngresoClinica;
                HC.agregarCierreHC(recepcionC.cierre);
                if (recepcionC.remitido.nombreEntidad != null)
                {
                    recepcionC.remitido.id_ingresoCl = recepcionC.ingresoClinica.idIngresoClinica;
                    remitido = recepcionC.remitido;
                    HC.agregarRemitido(remitido);
                }
                return "proceso exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                return argxEx.ToString();
            }

        }



        public string crearRecepcionCasoModel(RecepcionCaso recepcionC)
        {
            try
            {
                HC = new HistoriaClinicaBO();
                Remitido remitido = new Remitido();
                Consecutivo consecutivo = new Consecutivo();
                IngresoClinica ingresoClinica = new IngresoClinica();
                Paciente paciente = new Paciente();
                consecutivo = recepcionC.consecutivo;
                paciente = recepcionC.paciente;
                paciente.estadoHC = false; 
                paciente.consecutivo = consecutivo.numeroConsecutivo;

                if (recepcionC.ingresoClinica.tieneEpc == null) {
                    recepcionC.ingresoClinica.tieneEpc = "NO";
                }

                if (recepcionC.ingresoClinica.tieneEps == null)
                {
                    recepcionC.ingresoClinica.tieneEps = "NO";
                }

                var pacienteExst = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == recepcionC.paciente.numeroHistoriaClinica select item).LastOrDefault();
                if (pacienteExst == null) {
                    HC.agregarConsecutivo(consecutivo);
                    paciente.consecutivo = consecutivo.idConsecutivo;
                    HC.agregarpaciente(paciente);
                    var usuarioId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var usuarioExitente = (from item in HC.listarUsuario() where item.Id == usuarioId select item).FirstOrDefault();
                    recepcionC.ingresoClinica.idUser = usuarioExitente.Id; //System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var pacienteIngr = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == recepcionC.paciente.numeroHistoriaClinica select item).LastOrDefault();
                    recepcionC.ingresoClinica.id_paciente = recepcionC.paciente.numeroHistoriaClinica;
                    ingresoClinica = recepcionC.ingresoClinica;
                    if (recepcionC.consultante.cedula != null)
                    {
                        Consultante existe = null;
                        var listaConsultante = hcDALC.listarConsultante();
                        if (listaConsultante != null) {
                             existe = (from item in hcDALC.listarConsultante() where recepcionC.consultante.cedula == item.cedula select item).LastOrDefault();
                        }
                        if (existe == null)
                        {
                            recepcionC.ingresoClinica.id_Consultante = recepcionC.consultante.cedula;
                            //recepcionC.consultante.numeroDocumentoPaciente = pacienteIngr.numeroHistoriaClinica;
                            HC.agregarConsultante(recepcionC.consultante);
                        }
                        recepcionC.ingresoClinica.id_Consultante = recepcionC.consultante.cedula;
                    }
                    HC.ingresoClinica(ingresoClinica);
                    var listaHCIngreso = HC.listarIngresoClinica().LastOrDefault();
                    recepcionC.cierre.id_ingresoClinica = listaHCIngreso.idIngresoClinica;
                    recepcionC.cierre.idUsuario = usuarioExitente.Id; //System.Web.HttpContext.Current.User.Identity.GetUserId();
                    HC.agregarCierreHC(recepcionC.cierre);
                    if (recepcionC.remitido.nombreEntidad != null)
                    {
                        recepcionC.remitido.id_ingresoCl = recepcionC.ingresoClinica.idIngresoClinica; 
                        remitido = recepcionC.remitido;
                        HC.agregarRemitido(remitido);
                    }

                    
                    return "proceso exitoso";
                }
                return "proceso exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                return argxEx.ToString();
            }

        }



        public string modificarCierre(IngresoClinica ingresoClinica) {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.modificarCierreHC(ingresoClinica);
        }


        public List<PermisosUsuariosPaciente> permisosUsuariosPac(){
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarPermisosUsuariosPac();
        }
            


        public List<ListadoHistoriasClinicas> listarHCAdmin() {
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            HC = new HistoriaClinicaBO();
            var ingresoClinica = HC.listarIngresoClinica();
            var pacienteActivo = (from item in HC.listarPaciente() where item.estadoHC == false select item).ToList();
            if(pacienteActivo != null)
            {
                var cierresHC = (from item in ingresoClinica where item.estadoHC == false select item).ToList();
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();

                foreach (var itemPAC in pacienteActivo)
                {
                    foreach (var itemIngre in cierresHC)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            var usuario = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                            if (usuario != null)
                            {
                                ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                itemIngre.idUser = usuario;
                                ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                ModeloHistoriasCl.idUser = itemIngre.idUser;
                                listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                            }
                        }
                    }
                }
            }
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }




        public List<ListadoHistoriasClinicas> listarHCInactivasAdmin()
        {
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<IngresoClinica> listaIngresoClinica1 = new List<IngresoClinica>();
            Dictionary<string, IngresoClinica> listaIngresoClinicaSinRepeticion = new Dictionary<string, IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            HC = new HistoriaClinicaBO();
            var ingresoClinica = HC.listarIngresoClinica();
            var cierresHC = (from item in ingresoClinica where item.estadoHC == true select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var pacienteInactivo = (from item in HC.listarPaciente() where item.estadoHC == true select item).ToList();

            foreach (var item in cierresHC) {
                if (!(listaIngresoClinicaSinRepeticion.ContainsKey(item.id_paciente))) {
                    listaIngresoClinicaSinRepeticion.Add(item.id_paciente, item);
                }
            }

            foreach (var item1 in listaIngresoClinicaSinRepeticion) {
                listaIngresoClinica1.Add(item1.Value);
            }

            if (pacienteInactivo != null)
            {
                foreach (var itemPAC in pacienteInactivo)
                {
                    foreach (var itemIngre in listaIngresoClinica1)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            var usuario = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                            if (usuario != null)
                            {
                                //if (listadoModeloHistoriasCl.Contains(itemPAC.numeroHistoriaClinica)) {
                                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                    itemIngre.idUser = usuario;
                                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                    ModeloHistoriasCl.idUser = itemIngre.idUser;
                                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                                //}
                            }
                        }
                    }
                }
            }
            
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }



        public List<ListadoHistoriasClinicas> listarHCDocente()
        {
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            HC = new HistoriaClinicaBO();

            var ingresoClinica = HC.listarIngresoClinica();
            var listaPermisosUsuarios = HC.permisosUsuariosPac();
            var cierresHC = (from item in ingresoClinica where item.estadoHC == false select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();
            var pacienteActivo = (from item in HC.listarPaciente() where item.estadoHC == false select item).ToList();
            if (pacienteActivo != null)
            {
                foreach (var itemPAC in pacienteActivo)
                {
                    foreach (var itemIngre in listaPermisosUsuarios)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.id_aplicationUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.id_aplicationUser select item.Email).FirstOrDefault();
                                if (usr != null)
                                {
                                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                    itemIngre.id_aplicationUser = usuario;
                                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                    ModeloHistoriasCl.idUser = itemIngre.id_aplicationUser;
                                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                                }
                            }
                        }
                    }
                }
                //foreach (var itemPAC in pacienteActivo)
                //{
                //    foreach (var itemIngre in cierresHC)
                //    {
                //        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                //        {
                //            if (itemIngre.idUser == user)
                //            {
                //                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                //                if (usr != null)
                //                {
                //                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                //                    itemIngre.idUser = usuario;
                //                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                //                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                //                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                //                    ModeloHistoriasCl.idUser = itemIngre.idUser;
                //                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                //                }
                //            }
                //        }
                //    }
                //}
            }               
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }



        public List<ListadoHistoriasClinicas> listarHCInactivasDocente()
        {
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            Dictionary<string, IngresoClinica> listaIngresoClinicaSinRepeticion = new Dictionary<string, IngresoClinica>();
            List<IngresoClinica> listaIngresoClinica1 = new List<IngresoClinica>();
            HC = new HistoriaClinicaBO();

            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var listaPermisosUsuarios = HC.permisosUsuariosPac().Where(x => x.id_aplicationUser == user).GroupBy(x => x.id_paciente).ToList();
            var ingresoClinica = HC.listarIngresoClinica();


            foreach (var item in listaPermisosUsuarios) {
                foreach (var item2 in ingresoClinica) {
                    if(long.Parse(item.Key) == item2.idIngresoClinica)
                    {
                        if (item2.estadoHC == true)
                        {
                            listaIngresoClinica.Add(item2);
                        }
                    }
                }
            }


            var cierresHC = (from item in listaIngresoClinica where item.estadoHC == true select item).ToList();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in listaIngresoClinica where item.idUser == user select item).ToList();
            var pacienteInactivo = (from item in HC.listarPaciente() where item.estadoHC == true select item).ToList();

            
            foreach (var item in cierresHC)
            {
                if (!(listaIngresoClinicaSinRepeticion.ContainsKey(item.id_paciente)))
                {
                    listaIngresoClinicaSinRepeticion.Add(item.id_paciente, item);
                }
            }

            foreach (var item1 in listaIngresoClinicaSinRepeticion)
            {
                listaIngresoClinica1.Add(item1.Value);
            }

            if (pacienteInactivo != null)
            {
                foreach (var itemPAC in pacienteInactivo)
                {
                    foreach (var itemIngre in listaIngresoClinica1)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            //if (itemIngre.idUser == user)
                            //{
                                var usr = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();//itemIngre.idUser select item.Email).FirstOrDefault();
                                if (usr != null)
                                {
                                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                    itemIngre.idUser = usr;
                                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                    ModeloHistoriasCl.idUser = itemIngre.idUser;
                                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                                }
                            //}
                        }
                    }
                }
            }
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }



        public List<ListadoHistoriasClinicas> listarHCEstudiante()
        {
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            HC = new HistoriaClinicaBO();

            var listaPermisosUsuarios = HC.permisosUsuariosPac();
            var ingresoClinica = HC.listarIngresoClinica();
            var cierresHC = (from item in ingresoClinica where item.estadoHC == false select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();
            var pacienteActivo = (from item in HC.listarPaciente() where item.estadoHC == false select item).ToList();
            if (pacienteActivo != null)
            {
                foreach (var itemPAC in pacienteActivo)
                {
                    foreach (var itemIngre in listaPermisosUsuarios)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.id_aplicationUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.id_aplicationUser select item.Email).FirstOrDefault();
                                if (usr != null)
                                {
                                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                    itemIngre.id_aplicationUser = usuario;
                                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                    ModeloHistoriasCl.idUser = itemIngre.id_aplicationUser;
                                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                                }
                            }
                        }
                    }
                }
                //foreach (var itemPAC in pacienteActivo)
                //{
                //    foreach (var itemIngre in cierresHC)
                //    {
                //        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                //        {
                //            if (itemIngre.idUser == user)
                //            {
                //                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                //                if (usr != null)
                //                {
                //                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                //                    itemIngre.idUser = usuario;
                //                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                //                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                //                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                //                    ModeloHistoriasCl.idUser = itemIngre.idUser;
                //                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                //                }
                //            }
                //        }
                //    }
                //}
            }
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }



        public List<ListadoHistoriasClinicas> listarHCInactivasEstudiante()
        {
            List<ListadoHistoriasClinicas> listadoModeloHistoriasCl = new List<ListadoHistoriasClinicas>();
            ListadoHistoriasClinicas ModeloHistoriasCl;
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            List<Paciente> listaPaciente = new List<Paciente>();
            Dictionary<string, IngresoClinica> listaIngresoClinicaSinRepeticion = new Dictionary<string, IngresoClinica>();
            List<IngresoClinica> listaIngresoClinica1 = new List<IngresoClinica>();

            HC = new HistoriaClinicaBO();
            var ingresoClinica = HC.listarIngresoClinica();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var listaPermisosUsuarios = HC.permisosUsuariosPac().Where(x => x.id_aplicationUser == user).GroupBy(x => x.id_paciente).ToList();
            //var ingresoClinica = HC.listarIngresoClinica();
            foreach (var item in listaPermisosUsuarios)
            {
                foreach (var item2 in ingresoClinica)
                {
                    if (long.Parse(item.Key) == item2.idIngresoClinica)
                    {
                        if (item2.estadoHC == true)
                        {
                            listaIngresoClinica.Add(item2);
                        }
                    }
                }
            }
            var cierresHC = (from item in listaIngresoClinica where item.estadoHC == true select item).ToList();
            
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in listaIngresoClinica where item.idUser == user select item).ToList();

            var pacienteInactivo = (from item in HC.listarPaciente() where item.estadoHC == true select item).ToList();

            foreach (var item in cierresHC)
            {
                if (!(listaIngresoClinicaSinRepeticion.ContainsKey(item.id_paciente)))
                {
                    listaIngresoClinicaSinRepeticion.Add(item.id_paciente, item);
                }
            }

            foreach (var item1 in listaIngresoClinicaSinRepeticion)
            {
                listaIngresoClinica1.Add(item1.Value);
            }

            if (pacienteInactivo != null)
            {
                foreach (var itemPAC in pacienteInactivo)
                {
                    foreach (var itemIngre in listaIngresoClinica1)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            //if (itemIngre.idUser == user)
                            //{
                                var usr = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault(); //itemIngre.idUser select item.Email).FirstOrDefault();
                            if (usr != null)
                                {
                                    ModeloHistoriasCl = new ListadoHistoriasClinicas();
                                    itemIngre.idUser = usr;
                                    ModeloHistoriasCl.numeroHC = itemPAC.numeroHistoriaClinica;
                                    ModeloHistoriasCl.nombrePaciente = itemPAC.nombre;
                                    ModeloHistoriasCl.apellidoPaciente = itemPAC.apellido;
                                    ModeloHistoriasCl.idUser = itemIngre.idUser;
                                    listadoModeloHistoriasCl.Add(ModeloHistoriasCl);
                                }
                            //}
                        }
                    }
                }
            }
            return listadoModeloHistoriasCl.OrderBy(x => x.numeroHC).ToList();
        }



        public string modificarRemision(List<IngresoClinica> ingresoPaciente) {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.modificarRemision(ingresoPaciente);
        }



        public string agregarConsulta(Consulta consulta) {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarConsulta(consulta);
        }



        public string agregarEstrategiaIngreso(IngresoEstrategiasEvaluacion estrategiaIngreso)
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarEstrategiaEva(estrategiaIngreso);
        }


        //Método encargado de agregar en bd inasitencias, de acuerdo a la HC.
        public string agregarInasistencia(Inasistencias inasistencia)
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarInasistencia(inasistencia);
        }


        //Método para obtener el listado de inasistencias de una hc asociada.
        public List<Inasistencias> listarInasistencias()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listaInasistencia();
        }


        //Método encargado para guardar en bd los diagnósticos y la categorización del CAP, según seleccione el usuario.
        public string crearDiagnosticosYcategorizacion(long idHistoriaClinica, string diagnostico, string categorizacion)
        {
            hcDALC = new HistoriaClinicaDALC();

            var listaDiagnostico = diagnostico.Split(',');
            var categorizaciones = categorizacion.Split(',');
            var listaCat = hcDALC.listarCategorizacion();
            var listaConsultas = hcDALC.listarConsultas();
            var ultimaConsulta = (from item in listaConsultas where item.id_ingresoClinica == idHistoriaClinica select item).LastOrDefault();

            List<CategorizacionHC> lstCategorizacion = new List<CategorizacionHC>();
            List<ConsultaDiagnostico> lstConsultaDiagnostico = new List<ConsultaDiagnostico>();
            CategorizacionHC categorizacionCAP;
            ConsultaDiagnostico consultaDiagnostico;

            foreach (var itemCategorizaciones in categorizaciones) {
                categorizacionCAP = new CategorizacionHC();
                foreach (var itemCategorizacion in listaCat) {
                    if (itemCategorizacion.id_CategorizacionCAP == long.Parse(itemCategorizaciones)) {
                        categorizacionCAP.id_IngresoClinica = idHistoriaClinica;
                        categorizacionCAP.id_Categorizacion = itemCategorizacion.id_CategorizacionCAP;
                        lstCategorizacion.Add(categorizacionCAP);
                    }
                }
            }

            foreach (var itemCategorizaciones in listaDiagnostico)
            {
                consultaDiagnostico = new ConsultaDiagnostico();
                consultaDiagnostico.id_diagnostico = itemCategorizaciones;
                consultaDiagnostico.id_consulta = ultimaConsulta.idConsulta;
                lstConsultaDiagnostico.Add(consultaDiagnostico);
            }
            return hcDALC.guardarCategorizacionesCAPDiagnosticoConsultas(lstCategorizacion, lstConsultaDiagnostico);
        }



        public string crearDiagnosticosInformeSesion(long idHistoriaClinica, string diagnostico)
        {
            hcDALC = new HistoriaClinicaDALC();

            var listaDiagnostico = diagnostico.Split(',');
            var listaConsultas = hcDALC.listarConsultas();
            var listaConsultasDiagnosticos = hcDALC.listarConsultaDiagnostico();
            var ultimaConsulta = (from item in listaConsultas where item.id_ingresoClinica == idHistoriaClinica select item).LastOrDefault();
            var listaConsultasIngreso = (from item in hcDALC.listarConsultas() where item.id_ingresoClinica == ultimaConsulta.id_ingresoClinica select item).ToList();

            List<ConsultaDiagnostico> lstConsultaDiagnostico = new List<ConsultaDiagnostico>();
            ConsultaDiagnostico consultaDiagnostico;
            List<ConsultaDiagnostico> consultaDiagnostico2 = new List<ConsultaDiagnostico>();
            foreach (var item in listaConsultasIngreso) {
                foreach (var item2 in listaConsultasDiagnosticos) {
                    if (item.idConsulta == item2.id_consulta) {
                        consultaDiagnostico2.Add(item2);
                    }
                }
            }

            foreach (var itemDiagnostico in listaDiagnostico)
            {
                var listaDiagnosticoExistente = (from item in consultaDiagnostico2 where item.id_diagnostico == itemDiagnostico select item).ToList();
                if (listaDiagnosticoExistente.Count == 0) {
                    consultaDiagnostico = new ConsultaDiagnostico();
                    consultaDiagnostico.id_diagnostico = itemDiagnostico;
                    consultaDiagnostico.id_consulta = ultimaConsulta.idConsulta;
                    lstConsultaDiagnostico.Add(consultaDiagnostico);
                }
            }
            return hcDALC.guardarDiagnosticoConsultasInformes(lstConsultaDiagnostico);
        }



        public string agregarEstrategiaIngreso(List<PermisosUsuariosPaciente> permisosUsr)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.asignarUsuariosHC(permisosUsr);
            return "Proceso Exitoso";
        }



        public List<ConsultaDiagnostico> listarConsultaDiagnosticos()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarConsultaDiagnostico();
        }


        //Se modifica el ingreso clínica ya que se cierra la HC
        public string modificarCierreHCIngresoClinica(IngresoClinica ingresoCl)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.modificarCierreHCIngresoCl(ingresoCl);
                return "Proceso Exitoso";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                return argxEx.ToString();
            }
        }


        //Método encargado de modificar un cierre
        public string modificarCierre(CierreHC cierreHistoriaClinica)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.modificarCierre(cierreHistoriaClinica);
            return "Proceso Exitoso";
        }


        //Método encargado de agregar los motivos de remision de acuerdo al cierre de una HC
        public string agregarMotivosRemision(List<MotivoCierreHistoriaClinica> listaMotivosRemisison)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarMotivosRemision(listaMotivosRemisison);
            return "Proceso Exitoso";
        }



        //Se elimina un usuario asignado a la historia clínica
        public string eliminarUsuarioAsignado(PermisosUsuariosPaciente permisosUsuarioPaciente) {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.eliminarUsuarioAsignadoHC(permisosUsuarioPaciente);
            return "Proceso Exitoso";
        }


        public List<CategorizacionHC> listarCetegorizacionesHC()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarCategorizacionIngresoClinica();
        }

        

    }
}