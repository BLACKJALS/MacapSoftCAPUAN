﻿using MacapSoftCAPUAN.BO;
using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
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
            List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
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

            var consectivo = HC.listarConsecutivo().Last();
            ViewBag.itemConsecutivo = consectivo.numeroConsecutivo;

            ViewBag.ItemLocalidades = listaItemsLocalidades.ToList();
            ViewBag.ItemBarrios = listaItemsBarrios.ToList();
            ViewBag.ItemDocumento = listaItemsDocumento.ToList();
            ViewBag.ItemEps = listaItemsEps.ToList();
            ViewBag.ItemValidacion = listaItemsValidation.ToList();
            ViewBag.ItemDocumentoConsultante = listaItemsDocumentoConsultante.ToList();
            ViewBag.ItemPaises = listaItemsPaises.ToList();
            ViewBag.ItemEstrato = listaItemsEstrato.ToList();
            ViewBag.ItemCiudades = listaItemsCuidades.ToList();
            return View();
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
        public ActionResult IngresoPacientesCreado(RecepcionCaso model, string Informacion)
        {
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            Consultante consultante = new Consultante();
            consultantePaciente consultantePa;
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            Consecutivo consecutivo = new Consecutivo();

            try
            {
                consecutivo = model.consecutivo;
                model.paciente.idUser = System.Web.HttpContext.Current.User.Identity.GetUserId();
                paciente = model.paciente;
                HC.agregarpaciente(paciente);

                HC.agregarConsecutivo(consecutivo);
                if (model.consultante.cedula != null)
                {
                    HC.agregarConsultante(model.consultante);

                    consultantePa = new consultantePaciente();
                    consultantePa.idPaciente = model.paciente;
                    consultantePa.idPaciente.numeroDocumento = model.paciente.numeroDocumento;
                    consultantePa.idConsultante = model.consultante;
                    consultantePa.idConsultante.cedula = model.consultante.cedula;
                    HC.agregarConsultantePaciente(consultantePa);
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
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
                    remisionPaciente.paciente = modelRemision.paciente.numeroDocumento;
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
                        motivoRemisionVM.id = item.paciente;
                        motivoRemisionVM.nombrePaciente = (from pa in listaPacientes where pa.numeroDocumento == item.paciente select pa.nombre).FirstOrDefault() + " ";
                        motivoRemisionVM.nombrePaciente += (from pa in listaPacientes where pa.numeroDocumento == item.paciente select pa.apellido).FirstOrDefault() + " ";
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



        public ActionResult listaHistoriasClinicas() {
            return View();
        }

        public ActionResult ingresoHistoriaClinica() {

            return View();
        }
    }
}