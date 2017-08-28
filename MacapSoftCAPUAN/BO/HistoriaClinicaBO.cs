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

        public List<ApplicationUser> listarUsuario()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listarUsuario();
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
                HC.modificarPaciente(recepcionC.paciente);
                HC.ingresoClinica(ingresoClinica);
                var listaHCIngreso = HC.listarIngresoClinica().LastOrDefault();
                recepcionC.cierre.id_ingresoClinica = listaHCIngreso.idIngresoClinica;
                HC.agregarCierreHC(recepcionC.cierre);
                if (recepcionC.remitido.nombreEntidad != null)
                {
                    recepcionC.remitido.id_paciente = recepcionC.paciente.numeroHistoriaClinica;
                    remitido = recepcionC.remitido;
                    HC.agregarRemitido(remitido);
                }

                if (recepcionC.consultante.cedula != null)
                {
                    var existe = (from item in hcDALC.listarConsultante() where recepcionC.consultante.cedula == item.cedula select item).LastOrDefault();
                    if (existe == null)
                    {
                        //recepcionC.consultante.numeroDocumentoPaciente = pacienteIngr.numeroDocumento;
                        HC.agregarConsultante(recepcionC.consultante);
                    }
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
                var pacienteExst = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == recepcionC.paciente.numeroHistoriaClinica select item).LastOrDefault();
                if (pacienteExst == null) {
                    HC.agregarpaciente(paciente);
                    HC.agregarConsecutivo(consecutivo);
                    var usuarioId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var usuarioExitente = (from item in HC.listarUsuario() where item.Id == usuarioId select item).FirstOrDefault();
                    recepcionC.ingresoClinica.idUser = usuarioExitente.Id; //System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var pacienteIngr = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == recepcionC.paciente.numeroHistoriaClinica select item).LastOrDefault();
                    recepcionC.ingresoClinica.id_paciente = recepcionC.paciente.numeroHistoriaClinica;
                    ingresoClinica = recepcionC.ingresoClinica;
                    HC.ingresoClinica(ingresoClinica);
                    var listaHCIngreso = HC.listarIngresoClinica().LastOrDefault();
                    recepcionC.cierre.id_ingresoClinica = listaHCIngreso.idIngresoClinica;
                    recepcionC.cierre.idUsuario = usuarioExitente.Id; //System.Web.HttpContext.Current.User.Identity.GetUserId();
                    HC.agregarCierreHC(recepcionC.cierre);
                    if (recepcionC.remitido.nombreEntidad != null)
                    {
                        recepcionC.remitido.id_paciente = pacienteIngr.numeroHistoriaClinica; 
                        remitido = recepcionC.remitido;
                        HC.agregarRemitido(remitido);
                    }

                    if (recepcionC.consultante.cedula != null)
                    {
                        var existe = (from item in hcDALC.listarConsultante() where recepcionC.consultante.cedula == item.cedula select item).LastOrDefault();
                        if (existe == null)
                        {
                            recepcionC.consultante.numeroDocumentoPaciente = pacienteIngr.numeroHistoriaClinica;
                            HC.agregarConsultante(recepcionC.consultante);
                        }
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

                foreach (var itemPAC in HC.listarPaciente())
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
                foreach (var itemPAC in HC.listarPaciente())
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
            var cierresHC = (from item in ingresoClinica where item.estadoHC == false select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();
            var pacienteActivo = (from item in HC.listarPaciente() where item.estadoHC == false select item).ToList();
            if (pacienteActivo != null)
            {
                foreach (var itemPAC in HC.listarPaciente())
                {
                    foreach (var itemIngre in cierresHC)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.idUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                                if (usr != null)
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
            var ingresoClinica = HC.listarIngresoClinica();
            var cierresHC = (from item in ingresoClinica where item.estadoHC == true select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();

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
                foreach (var itemPAC in HC.listarPaciente())
                {
                    foreach (var itemIngre in listaIngresoClinica1)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.idUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
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
                            }
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

            var ingresoClinica = HC.listarIngresoClinica();
            var cierresHC = (from item in ingresoClinica where item.estadoHC == false select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();
            var pacienteActivo = (from item in HC.listarPaciente() where item.estadoHC == false select item).ToList();
            if (pacienteActivo != null)
            {
                foreach (var itemPAC in HC.listarPaciente())
                {
                    foreach (var itemIngre in cierresHC)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.idUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
                                if (usr != null)
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
            var cierresHC = (from item in ingresoClinica where item.estadoHC == true select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            var pacienteUser = (from item in ingresoClinica where item.idUser == user select item).ToList();

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
                foreach (var itemPAC in HC.listarPaciente())
                {
                    foreach (var itemIngre in listaIngresoClinica1)
                    {
                        if (itemPAC.numeroHistoriaClinica == itemIngre.id_paciente)
                        {
                            if (itemIngre.idUser == user)
                            {
                                var usr = (from item in HC.listarUsuario() where item.Id == itemIngre.idUser select item.Email).FirstOrDefault();
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
                            }
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



        public string agregarInasistencia(Inasistencias inasistencia)
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarInasistencia(inasistencia);
        }


        public List<Inasistencias> listarInasistencias()
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.listaInasistencia();
        }
    }
}