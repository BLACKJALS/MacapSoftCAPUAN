﻿using MacapSoftCAPUAN.BO;
using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MacapSoftCAPUAN.Controllers
{
    [Authorize]
    public class HistoriaClinicaController : Controller
    {
        // GET: HistoriaClinica
        public HistoriaClinicaBO HC;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ingresoPaciente() {
            return View();
        }


        public ActionResult IngresoPacientes(string identificacion)
        {
            
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            RecepcionCaso recepcion = new RecepcionCaso();
            IngresoClinica ingresoCli = new IngresoClinica();
            Remitido remitido = new Remitido();
            var listaPaciente = from item in HC.listarPaciente() where item.numeroDocumento == identificacion select item;
            paciente = listaPaciente.FirstOrDefault();
            recepcion.paciente = paciente;
            if (paciente != null) {
                var pacienteIngreso = from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                ingresoCli = pacienteIngreso.LastOrDefault();
                recepcion.ingresoClinica = ingresoCli;

                var remitidoP = from item in HC.listarRemitido() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                remitido = remitidoP.LastOrDefault();
                recepcion.remitido = remitido;
            }
            List < SelectListItem > listaItemsBarrios = new List<SelectListItem>();
            List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
            List<SelectListItem> listaItemsDocumentoConsultante = new List<SelectListItem>();
            List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
            List<SelectListItem> listaItemsEps = new List<SelectListItem>();
            List<SelectListItem> listaItemsValidation = new List<SelectListItem>();
            List<SelectListItem> listaItemsPaises = new List<SelectListItem>();
            List<SelectListItem> listaItemsEstrato = new List<SelectListItem>();
            List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
            SelectListItem items;
            SelectListItem documento;
            SelectListItem validacion;

            documento = new SelectListItem();
            documento.Text = "Seleccione";
            documento.Value = "";
            listaItemsDocumento.Add(documento);
            listaItemsBarrios.Add(documento);
            listaItemsLocalidades.Add(documento);
            listaItemsEps.Add(documento);
            listaItemsDocumentoConsultante.Add(documento);
            listaItemsPaises.Add(documento);

            var listaBarrio = HC.listarBarrios();
            foreach (var item in listaBarrio)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idBarrio;
                listaItemsBarrios.Add(items);
            }

            var listaEstrato = HC.listarEstrato();
            foreach (var item in listaEstrato)
            {
                items = new SelectListItem();
                items.Text = item.numeroEstrato;
                items.Value = item.idEstrato.ToString();
                listaItemsEstrato.Add(items);
            }

            var listaCiudades = HC.listarCiudades();
            foreach (var item in listaCiudades)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idCiudad;
                listaItemsCuidades.Add(items);
            }

            

            var listaTipoDocumento = HC.listaTiposDocumento();
            foreach (var item in listaTipoDocumento) {
                items = new SelectListItem();
                items.Text = item.tipo;
                items.Value = item.idDocumento.ToString();
                listaItemsDocumento.Add(items);
            }

            var listaTipoDocumentoAdultos = HC.listaTiposDocumento();
            foreach (var item in listaTipoDocumentoAdultos)
            {
                if (item.tipo!= "RC" && item.tipo!= "TI") {
                    items = new SelectListItem();
                    items.Text = item.tipo;
                    items.Value = item.idDocumento.ToString();
                    listaItemsDocumentoConsultante.Add(items);
                }
            }

            var listaPaises = HC.listarPaises();
            foreach (var item in listaPaises)
            {
                    items = new SelectListItem();
                    items.Text = item.nombrePais;
                    items.Value = item.idPais;
                    listaItemsPaises.Add(items);
            }

            validacion = new SelectListItem();
            validacion.Text = "Seleccione";
            validacion.Value = "";
            listaItemsValidation.Add(validacion);
            validacion = new SelectListItem();
            validacion.Text = "SI";
            validacion.Value = "SI";
            listaItemsValidation.Add(validacion);
            validacion = new SelectListItem();
            validacion.Text = "NO";
            validacion.Value = "NO";
            listaItemsValidation.Add(validacion);

            var listaLocalidades = HC.listarLocalidades();
            foreach (var item in listaLocalidades)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idLocalidad;
                listaItemsLocalidades.Add(items);
            }

            var listaEps = HC.listarEps();
            foreach (var item in listaEps)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.IdEPS;
                listaItemsEps.Add(items);
            }

            if (paciente == null)
            {
                var consecutivo = HC.listarConsecutivo().Last();
                ViewBag.itemConsecutivo = consecutivo.numeroConsecutivo;

            }
            else {
                ViewBag.itemConsecutivo = paciente.consecutivo;
            }
            ViewBag.nPc = identificacion;
            ViewBag.ItemLocalidades = listaItemsLocalidades.ToList();
            ViewBag.ItemBarrios = listaItemsBarrios.ToList();
            ViewBag.ItemDocumento = listaItemsDocumento.ToList();
            ViewBag.ItemEps = listaItemsEps.ToList();
            ViewBag.ItemValidacion = listaItemsValidation.ToList();
            ViewBag.ItemDocumentoConsultante = listaItemsDocumentoConsultante.ToList();
            ViewBag.ItemPaises = listaItemsPaises.ToList();
            ViewBag.ItemEstrato = listaItemsEstrato.ToList();
            ViewBag.ItemCiudades = listaItemsCuidades.ToList();
            if (recepcion.paciente != null)
            {
                ViewBag.existente = "Si";
                ViewBag.numeroHC = recepcion.paciente.numeroHistoriaClinica;
                ViewBag.nombre = recepcion.paciente.nombre;
                ViewBag.apellido = recepcion.paciente.apellido;
                ViewBag.direccion = recepcion.paciente.direccion;
                //ViewBag.edad = recepcion.paciente.edad;
                ViewBag.email = recepcion.paciente.email;
                var fechN = (recepcion.paciente.fechaNacimiento).ToString();
                var fechnStr = DateTime.Parse(fechN);
                string format = "yyyy-MM-dd";
                var fecha = fechnStr.ToString(format);
                ViewBag.fechaNacimiento = fecha;
                ViewBag.numeroDocumento = recepcion.paciente.numeroDocumento;
                ViewBag.telefono = recepcion.paciente.telefono;
                if (recepcion.remitido != null) {
                    ViewBag.entidadRemitido = recepcion.remitido.nombreEntidad;
                    ViewBag.profesionalRemitido = recepcion.remitido.nombreRemitente;
                }
                if (recepcion.ingresoClinica != null)
                {
                    ViewBag.motivoConsulta = recepcion.ingresoClinica.motivoConsulta;
                    ViewBag.Observaciones = recepcion.ingresoClinica.observaciones;
                }
                return View(recepcion);
            }
            return View(recepcion);
        }



        public ActionResult RecepcionCaso(string bussinesUnit)
        {
            HC = new HistoriaClinicaBO();
            List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
            List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
            List<SelectListItem> listaItemsEps = new List<SelectListItem>();
            SelectListItem items;
            var listaBarrio = HC.listarBarrios();
            foreach (var item in listaBarrio) {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.nombre;
                listaItemsBarrios.Add(items);
            }

            var listaLocalidades = HC.listarLocalidades();
            foreach (var item in listaLocalidades)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.nombre;
                listaItemsLocalidades.Add(items);
            }

            var listaEps = HC.listarEps();
            foreach (var item in listaEps)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.nombre;
                listaItemsEps.Add(items);
            }
            ViewBag.ItemLocalidades = listaItemsLocalidades.ToList();
            ViewBag.ItemBarrios = listaItemsBarrios.ToList();
            ViewBag.ItemEps = listaItemsEps.ToList();
            return View();
        }


        
        [HttpPost]
        public ActionResult IngresoPacientesCreado(RecepcionCaso model, string Informacion, string Documento)
        {
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            Consultante consultante = new Consultante();
            consultantePaciente consultantePa;
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            Consecutivo consecutivo = new Consecutivo();

            var listaPacientes = HC.listarPaciente();
            if (model.paciente.numeroDocumento != Documento)
            {
                var listaP = (from item in listaPacientes where item.numeroHistoriaClinica == Documento select item).FirstOrDefault();
                model.paciente.fechaNacimiento = listaP.fechaNacimiento;
                model.paciente.numeroHistoriaClinica = model.paciente.numeroDocumento;
                model.consecutivo.numeroConsecutivo = model.consecutivo.numeroConsecutivo + 1;
            }
            else {
                model.paciente.numeroHistoriaClinica = String.IsNullOrEmpty(Documento) ? null : Documento;
            }
            
           
           
            var pacienteExistente = from p in listaPacientes where p.numeroHistoriaClinica == model.paciente.numeroHistoriaClinica select p;
            Paciente pacienteEx = new Paciente();
            pacienteEx = pacienteExistente.FirstOrDefault();
            if (model.paciente.tieneEps == "NO") {
                model.paciente.id_Eps = "No tiene";
            }
            if (pacienteEx != null)
            {
                model.paciente.fechaNacimiento = pacienteEx.fechaNacimiento;
                model.paciente.idUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
                HC.modificarPaciente(model.paciente);
                var listaIngCl = HC.listarIngresoClinica();
                var ingresoCl = (from p in listaIngCl where p.id_paciente == model.paciente.numeroHistoriaClinica select p).LastOrDefault();
                model.ingresoClinica.idIngresoClinica = ingresoCl.idIngresoClinica;
                model.ingresoClinica.id_paciente = model.paciente.numeroHistoriaClinica;
                model.ingresoClinica.IdPaciente = model.ingresoClinica.IdPaciente;
                HC.modificarIngresoCl(model.ingresoClinica);
                if (model.remitido.nombreEntidad != null)
                {
                    remitido = model.remitido;
                    remitido.id_paciente = model.paciente.numeroDocumento;
                    HC.agregarRemitido(remitido);
                }
            }
            else {

            try
            {
                consecutivo = model.consecutivo;
                model.paciente.idUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
                paciente = model.paciente;
                paciente.consecutivo = consecutivo.numeroConsecutivo;
                paciente.id_profesion = 1;
                paciente.id_NivelEscolaridad = 1;
                HC.agregarpaciente(paciente);
                HC.agregarConsecutivo(consecutivo);
                ingresoClinica = model.ingresoClinica;
                model.ingresoClinica.id_paciente = model.paciente.numeroDocumento;
                    
                //ingresoClinica.fechaIngreso = model.ingresoClinica.fechaIngreso;
                //ingresoClinica.id_paciente = model.ingresoClinica.id_paciente;
                //ingresoClinica.id_paciente = model.paciente.numeroDocumento;
                HC.ingresoClinica(ingresoClinica);
                if (model.remitido.nombreEntidad != null) {
                        remitido = model.remitido;
                        remitido.id_paciente = model.paciente.numeroDocumento;
                        HC.agregarRemitido(remitido);
                }

                if (model.consultante.cedula != null)
                {
                    HC.agregarConsultante(model.consultante);

                    consultantePa = new consultantePaciente();
                    consultantePa.IdPaciente = model.paciente;
                    consultantePa.id_Paciente = model.paciente.numeroDocumento;
                    consultantePa.IdConsultante = model.consultante;
                    consultantePa.id_Consultante = model.consultante.id_Consultante;
                    HC.agregarConsultantePaciente(consultantePa);
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
            }
            try
            {
                if (Informacion == "1")
                {
                    ViewBag.Pac = model.paciente.numeroDocumento;
                    return View("PacienteRemitido");
                }
                else {
                    return View("IngresoPacienteCreado");
                }
            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public ActionResult RemitirPaciente(RemisionPaciente modelRemision, string concMremison, DateTime fechaRemision) {
            HC = new HistoriaClinicaBO();
            List<Remision> listaResmisionPaciente = new List<Remision>();
            Remision remisionPaciente;
            MotivosRemisiones motivoRem;
            var lstRP = concMremison.Split(';');
            foreach (var item in lstRP) {
                if (item != "") {
                    remisionPaciente = new Remision();
                    motivoRem = new MotivosRemisiones();
                    motivoRem.idMotivoRemision = int.Parse(item);
                    remisionPaciente.motivoRemision = motivoRem.idMotivoRemision;
                    remisionPaciente.fechaRemitido = fechaRemision; 
                    remisionPaciente.id_paciente = modelRemision.paciente.numeroDocumento;
                    listaResmisionPaciente.Add(remisionPaciente);
                }
            }
            if (modelRemision.paciente.numeroDocumento != "")
            {
                
                modelRemision.remision = listaResmisionPaciente;
                modelRemision.paciente = modelRemision.paciente;
                HC.agregarListaRemision(modelRemision.remision);

            }
            return View("PacienteRemitidoExitoso");
        }

        public ActionResult listarPacientesRemitidos() {
            return View();
        }


        public JsonResult listarPacientesRemitidosCAP() {
            HC = new HistoriaClinicaBO();
            MotivoRemisionVM motivoRemisionVM;
            List<MotivoRemisionVM> listaMotivoRemision = new List<MotivoRemisionVM>();
            Dictionary<string, MotivoRemisionVM> mtv = new Dictionary<string, MotivoRemisionVM>();

            var listaRemisiones = HC.listarRemisiones();
            var listaMotivosRemisiones = HC.listarMotivosRemisiones();
            var listaPacientes = HC.listarPaciente();

            foreach (var item in listaRemisiones)
            {
                motivoRemisionVM = new MotivoRemisionVM();
                foreach (var itemlmr in listaMotivosRemisiones)
                {
                    if (itemlmr.idMotivoRemision == item.motivoRemision) {
                        motivoRemisionVM.id = item.id_paciente;
                        motivoRemisionVM.nombrePaciente = (from pa in listaPacientes where pa.numeroDocumento == item.id_paciente select pa.nombre).FirstOrDefault() + " ";
                        motivoRemisionVM.nombrePaciente += (from pa in listaPacientes where pa.numeroDocumento == item.id_paciente select pa.apellido).FirstOrDefault() + " ";
                        motivoRemisionVM.nombreMotivoRemision = itemlmr.nombre;
                        motivoRemisionVM.fecha = item.fechaRemitido;
                        listaMotivoRemision.Add(motivoRemisionVM);
                    } 
                }
            }
            
            foreach (var itemPaciente in listaMotivoRemision)
            {
                motivoRemisionVM = new MotivoRemisionVM();
                motivoRemisionVM = itemPaciente;
                if (!(mtv.ContainsKey(itemPaciente.id)))
                {
                    mtv.Add(itemPaciente.id, itemPaciente);
                }
                else
                {
                    var a = mtv.FirstOrDefault(x => x.Key == itemPaciente.id);
                    mtv.Remove(itemPaciente.id);
                    itemPaciente.nombreMotivoRemision += " "+ a.Value.nombreMotivoRemision;
                    mtv.Add(itemPaciente.id, itemPaciente);
                }
            }

            return Json(mtv.Values, JsonRequestBehavior.AllowGet);
        }

        public ActionResult buscarPacientes() {
            return View();
        }


        public JsonResult listarPacientesBusquedaCAP()
        {
            HC = new HistoriaClinicaBO();
            var listaPacientes = HC.listarPaciente();
            List<Paciente> list = new List<Paciente>();
            foreach (var item in listaPacientes)
            {
                item.nombre += " " + item.apellido;
                list.Add(item);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult listaHistoriasClinicas() {
            return View();
        }

        public ActionResult ingresoHistoriaClinica() {

            return View();
        }
    }
}