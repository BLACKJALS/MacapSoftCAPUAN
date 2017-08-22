﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using MacapSoftCAPUAN.BO;
using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public DiagnosticoBO diagBo;

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

            var ingresoClVal = new IngresoClinica();
            var cierreHC = new CierreHC();
            

            var listaPaciente = from item in HC.listarPaciente() where item.numeroHistoriaClinica == identificacion select item;
            paciente = listaPaciente.LastOrDefault();

            if (paciente != null) {
                ingresoClVal = (from item in HC.listarIngresoClinica() where paciente.numeroHistoriaClinica == item.id_paciente select item).LastOrDefault();
                //ingresoClVal = listaIngresoClinica.LastOrDefault();
                var listaCierreHC = from item in HC.listarCierres() where ingresoClVal.idIngresoClinica == item.id_ingresoClinica select item;
                cierreHC = listaCierreHC.LastOrDefault();
            }


            if (cierreHC.id_ingresoClinica != 0)
            {
                if (ingresoClVal.estadoHC == true)
                {
                    recepcion.paciente = paciente;
                    if (ingresoClVal != null) {
                        recepcion.ingresoClinica = ingresoClVal;                       
                    }
                    if (paciente != null)
                    {
                        recepcion.ingresoClinica = ingresoClVal;
                        recepcion.remitido = remitido;
                    }
                    List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
                    List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
                    List<SelectListItem> listaItemsDocumentoConsultante = new List<SelectListItem>();
                    List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
                    List<SelectListItem> listaItemsEps = new List<SelectListItem>();
                    List<SelectListItem> listaItemsProfesion = new List<SelectListItem>();
                    List<SelectListItem> listaItemsValidation = new List<SelectListItem>();
                    List<SelectListItem> listaItemsPaises = new List<SelectListItem>();
                    List<SelectListItem> listaItemsEstrato = new List<SelectListItem>();
                    List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
                    List<SelectListItem> listaItemsSexo = new List<SelectListItem>();
                    List<SelectListItem> listaItemsNivelEscolaridad = new List<SelectListItem>();
                    List<SelectListItem> listaItemsOcupacion = new List<SelectListItem>();
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
                    foreach (var item in listaTipoDocumento)
                    {
                        items = new SelectListItem();
                        items.Text = item.tipo;
                        items.Value = item.idDocumento.ToString();
                        listaItemsDocumento.Add(items);
                    }

                    var listaTipoDocumentoAdultos = HC.listaTiposDocumento();
                    foreach (var item in listaTipoDocumentoAdultos)
                    {
                        if (item.tipo != "RC" && item.tipo != "TI")
                        {
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

                    var listaSexo = HC.listarSexo();
                    foreach (var item in listaSexo)
                    {
                        items = new SelectListItem();
                        items.Text = item.sexo;
                        items.Value = item.id_Sexo.ToString();
                        listaItemsSexo.Add(items);
                    }

                    var listaNivelEscolaridad = HC.listarNivelEscolaridad();
                    foreach (var item in listaNivelEscolaridad)
                    {
                        items = new SelectListItem();
                        items.Text = item.nombre;
                        items.Value = item.idNivelEscolaridad.ToString();
                        listaItemsNivelEscolaridad.Add(items);
                    }

                    var listaOcupacion = HC.listarOcupacion();
                    foreach (var item in listaOcupacion)
                    {
                        items = new SelectListItem();
                        items.Text = item.nombre;
                        items.Value = item.id_Ocupacion.ToString();
                        listaItemsOcupacion.Add(items);
                    }


                    if (paciente == null)
                    {
                        var consecutivo = HC.listarConsecutivo().Last();
                        ViewBag.itemConsecutivo = consecutivo.numeroConsecutivo;

                    }
                    else
                    {
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
                    ViewBag.Sexo = listaItemsSexo.ToList();
                    ViewBag.NivelEscolaridad = listaItemsNivelEscolaridad.ToList();
                    ViewBag.Ocupacion = listaItemsOcupacion.ToList();
                    if (recepcion.paciente != null)
                    {
                        ViewBag.existente = "Si";
                        ViewBag.numeroHC = recepcion.paciente.numeroHistoriaClinica;
                        ViewBag.nombre = recepcion.paciente.nombre;
                        ViewBag.apellido = recepcion.paciente.apellido;
                        ViewBag.direccion = recepcion.ingresoClinica.direccion;
                        ViewBag.email = recepcion.ingresoClinica.email;
                        var fechN = (recepcion.paciente.fechaNacimiento).ToString();
                        var fechnStr = DateTime.Parse(fechN);
                        string format = "yyyy-MM-dd";
                        var fecha = fechnStr.ToString(format);
                        ViewBag.fechaNacimiento = fecha;
                        ViewBag.numeroDocumento = recepcion.ingresoClinica.numeroDocumento;
                        ViewBag.telefono = recepcion.ingresoClinica.telefono;
                        if (recepcion.remitido != null)
                        {
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
                else
                {
                    return View("RecepcionCasoDenegada");
                }

            }
            else
            {
                recepcion.paciente = paciente;
                if (paciente != null)
                {
                    recepcion.ingresoClinica = ingresoCli;
                    recepcion.remitido = remitido;
                }
                List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
                List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
                List<SelectListItem> listaItemsDocumentoConsultante = new List<SelectListItem>();
                List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
                List<SelectListItem> listaItemsEps = new List<SelectListItem>();
                List<SelectListItem> listaItemsProfesion = new List<SelectListItem>();
                List<SelectListItem> listaItemsValidation = new List<SelectListItem>();
                List<SelectListItem> listaItemsPaises = new List<SelectListItem>();
                List<SelectListItem> listaItemsEstrato = new List<SelectListItem>();
                List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
                List<SelectListItem> listaItemsSexo = new List<SelectListItem>();
                List<SelectListItem> listaItemsNivelEscolaridad = new List<SelectListItem>();
                List<SelectListItem> listaItemsOcupacion = new List<SelectListItem>();
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
                foreach (var item in listaTipoDocumento)
                {
                    items = new SelectListItem();
                    items.Text = item.tipo;
                    items.Value = item.idDocumento.ToString();
                    listaItemsDocumento.Add(items);
                }

                var listaTipoDocumentoAdultos = HC.listaTiposDocumento();
                foreach (var item in listaTipoDocumentoAdultos)
                {
                    if (item.tipo != "RC" && item.tipo != "TI")
                    {
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

                var listaSexo = HC.listarSexo();
                foreach (var item in listaSexo)
                {
                    items = new SelectListItem();
                    items.Text = item.sexo;
                    items.Value = item.id_Sexo.ToString();
                    listaItemsSexo.Add(items);
                }

                var listaNivelEscolaridad = HC.listarNivelEscolaridad();
                foreach (var item in listaNivelEscolaridad)
                {
                    items = new SelectListItem();
                    items.Text = item.nombre;
                    items.Value = item.idNivelEscolaridad.ToString();
                    listaItemsNivelEscolaridad.Add(items);
                }

                var listaOcupacion = HC.listarOcupacion();
                foreach (var item in listaOcupacion)
                {
                    items = new SelectListItem();
                    items.Text = item.nombre;
                    items.Value = item.id_Ocupacion.ToString();
                    listaItemsOcupacion.Add(items);
                }


                if (paciente == null)
                {
                    var consecutivo = HC.listarConsecutivo().Last();
                    ViewBag.itemConsecutivo = consecutivo.numeroConsecutivo;

                }
                else
                {
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
                ViewBag.Sexo = listaItemsSexo.ToList();
                ViewBag.NivelEscolaridad = listaItemsNivelEscolaridad.ToList();
                ViewBag.Ocupacion = listaItemsOcupacion.ToList();
                if (recepcion.paciente != null)
                {
                    ViewBag.existente = "Si";
                    ViewBag.numeroHC = recepcion.paciente.numeroHistoriaClinica;
                    ViewBag.nombre = recepcion.paciente.nombre;
                    ViewBag.apellido = recepcion.paciente.apellido;
                    ViewBag.direccion = recepcion.ingresoClinica.direccion;
                    ViewBag.email = recepcion.ingresoClinica.email;
                    var fechN = (recepcion.paciente.fechaNacimiento).ToString();
                    var fechnStr = DateTime.Parse(fechN);
                    string format = "yyyy-MM-dd";
                    var fecha = fechnStr.ToString(format);
                    ViewBag.fechaNacimiento = fecha;
                    ViewBag.numeroDocumento = recepcion.ingresoClinica.numeroDocumento;
                    ViewBag.telefono = recepcion.ingresoClinica.telefono;
                    if (recepcion.remitido != null)
                    {
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
        }



        



        [HttpPost]
        public ActionResult IngresoPacientesCreado(RecepcionCaso model, string Informacion, string Documento)
        {
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            Consultante consultante = new Consultante();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            Consecutivo consecutivo = new Consecutivo();
            Paciente pacienteEx = new Paciente();
            var listaPacientes = HC.listarPaciente();
            if (model.paciente.numeroHistoriaClinica != Documento)
            {
                var listaP = (from item in listaPacientes where item.numeroHistoriaClinica == Documento select item).FirstOrDefault();
                if (!(listaP == null))
                {
                    model.paciente.fechaNacimiento = listaP.fechaNacimiento;
                    model.paciente.numeroHistoriaClinica = listaP.numeroHistoriaClinica;
                }
                else{
                    model.paciente.numeroHistoriaClinica = String.IsNullOrEmpty(Documento) ? null : Documento;
                }
            }
            else
            {
                model.paciente.numeroHistoriaClinica = String.IsNullOrEmpty(Documento) ? null : Documento;
            }

            var pacienteExistente = from p in listaPacientes where p.numeroHistoriaClinica == model.paciente.numeroHistoriaClinica select p;
            pacienteEx = pacienteExistente.LastOrDefault();
            if (model.ingresoClinica.tieneEps == "NO")
            {
                model.ingresoClinica.id_Eps = "No tiene";
            }
            if (pacienteEx != null)
            {
                HC.modificarRecepcionCasoModel(model);
            }
            else {

                try{
                    HC.crearRecepcionCasoModel(model);
                }
                catch (Exception ex){
                    ViewBag.error = ex.Message;
                    return View();
                }
            }
            try
            {
                if (Informacion == "1")
                {
                    List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
                    SelectListItem items;

                    var listaTipoDocumento = HC.listaTiposDocumento();
                    foreach (var item in listaTipoDocumento)
                    {
                        items = new SelectListItem();
                        items.Text = item.tipo;
                        items.Value = item.idDocumento.ToString();
                        listaItemsDocumento.Add(items);
                    }

                    var fechN = (model.ingresoClinica.fechaIngreso).ToString();
                    var fechnStr = DateTime.Parse(fechN);
                    string format = "yyyy-MM-dd";
                    var fecha = fechnStr.ToString(format);
                    ViewBag.fechaIniciopsicoterapia = fecha;

                    ViewBag.ItemDocumento = listaItemsDocumento.ToList();
                    ViewBag.Pac = model.paciente.numeroHistoriaClinica;
                    var remPaciente = new RemisionPaciente();
                    remPaciente.paciente = model.paciente;
                    remPaciente.ingresoClinica = model.ingresoClinica;
                    var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
                    ViewBag.usr = usuario;
                    return View("PacienteRemitido", remPaciente);
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
        public ActionResult RemitirPaciente(RemisionPaciente modelRemision, string concMremison) {
            HC = new HistoriaClinicaBO();
            List<Remision> listaResmisionPaciente = new List<Remision>();
            Remision remisionPaciente;
            MotivosRemisiones motivoRem;
            var lstRP = concMremison.Split(';');
            var listaP = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == modelRemision.paciente.numeroHistoriaClinica select item).LastOrDefault();
            var ingresoPaciente = (from item in HC.listarIngresoClinica() where item.id_paciente == listaP.numeroHistoriaClinica select item).LastOrDefault();
            modelRemision.cierre.idUsuario = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //var cierreHC = (from item in HC.listarCierres() where item.id_ingresoClinica == ingresoPaciente.idIngresoClinica select item).LastOrDefault();
            HC.modificarCierre(ingresoPaciente);
            foreach (var item in lstRP) {
                if (item != "") {
                    remisionPaciente = new Remision();
                    motivoRem = new MotivosRemisiones();
                    motivoRem.idMotivoRemision = int.Parse(item);
                    remisionPaciente.motivoRemision = motivoRem.idMotivoRemision;
                    remisionPaciente.fechaRemitido = modelRemision.remisionP.fechaRemitido;
                    remisionPaciente.id_ingresoClinica = ingresoPaciente.idIngresoClinica;
                    remisionPaciente.nombreProfesional = modelRemision.remisionP.nombreProfesional;
                    remisionPaciente.nombreInsitucionRemitida = modelRemision.remisionP.nombreInsitucionRemitida;
                    remisionPaciente.servicioRemitido = modelRemision.remisionP.servicioRemitido;
                    remisionPaciente.evolucionPaciente = modelRemision.remisionP.evolucionPaciente;
                    remisionPaciente.aspectosPositivos = modelRemision.remisionP.aspectosPositivos;
                    remisionPaciente.recomendaciones = modelRemision.remisionP.recomendaciones;
                    listaResmisionPaciente.Add(remisionPaciente);
                }
            }
            if (modelRemision.paciente.numeroHistoriaClinica != "")
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
            List<IngresoClinica> listaIngresoCl = new List<IngresoClinica>();
            var listaRemisiones = (from item in HC.listarRemisiones() select item).ToList();
            var listaMotivosRemisiones = HC.listarMotivosRemisiones();
            var ingresoClinica = (from item in HC.listarIngresoClinica() where item.estadoRemision == true select item).ToList();
            var listaPaciente = HC.listarPaciente();
            var listaRemitidos1 = (from ingreCl in ingresoClinica where ingreCl.estadoRemision == true select ingreCl).ToList();
            var listaAgrupada1 = listaRemitidos1.GroupBy(x => x.id_paciente).ToList();

            foreach (var item in listaAgrupada1) {
                foreach (var item2 in ingresoClinica) {
                    if (item.Key == item2.id_paciente) {
                        if (listaIngresoCl.Exists(x => x.id_paciente == item.Key))
                        {
                            var ingrCl = listaIngresoCl.Find(x => x.id_paciente == item.Key);
                            listaIngresoCl.Remove(ingrCl);
                            listaIngresoCl.Add(item2);
                        }
                        else {
                            listaIngresoCl.Add(item2);
                        }
                    }
                }
            }

            foreach (var item in listaRemisiones)
            {
                motivoRemisionVM = new MotivoRemisionVM();
                foreach (var itemlmr in listaMotivosRemisiones)
                {
                    if (itemlmr.idMotivoRemision == item.motivoRemision) {
                        //motivoRemisionVM.id_historiaClinica = item.id_ingresoClinica;
                        //var listaRemitidos = (from ingreCl in ingresoClinica where ingreCl.estadoRemision == true select ingreCl).ToList();
                        //var listaAgrupada = listaRemitidos.GroupBy(x => x.idIngresoClinica).ToList();
                        var ingreClinicaMotivoRem = (from pa in listaIngresoCl where pa.idIngresoClinica == item.id_ingresoClinica && pa.estadoRemision == true select pa).LastOrDefault();
                        if (ingreClinicaMotivoRem != null) {
                            var paciente = (from itemPac in listaPaciente where itemPac.numeroHistoriaClinica == ingreClinicaMotivoRem.id_paciente select itemPac).LastOrDefault();
                            motivoRemisionVM.id_historiaClinica = paciente.numeroHistoriaClinica;
                            if (ingreClinicaMotivoRem != null)
                            {
                                motivoRemisionVM.nombrePaciente = (from pa in listaPaciente where pa.numeroHistoriaClinica == ingreClinicaMotivoRem.id_paciente select pa.nombre).FirstOrDefault() + " ";
                                motivoRemisionVM.nombrePaciente += (from pa in listaPaciente where pa.numeroHistoriaClinica == ingreClinicaMotivoRem.id_paciente select pa.apellido).FirstOrDefault() + " ";
                                motivoRemisionVM.nombreMotivoRemision = itemlmr.nombre;
                                motivoRemisionVM.lugarRemitido = item.nombreInsitucionRemitida;
                                motivoRemisionVM.fecha = item.fechaRemitido;
                                listaMotivoRemision.Add(motivoRemisionVM);
                            }
                        }
                    } 
                }
            }

            foreach (var itemPaciente in listaMotivoRemision)
            {
                motivoRemisionVM = new MotivoRemisionVM();
                motivoRemisionVM = itemPaciente;
                if (!(mtv.ContainsKey(itemPaciente.id_historiaClinica)))
                {
                   mtv.Add(itemPaciente.id_historiaClinica, itemPaciente);
                }
                else
                {
                    var a = mtv.FirstOrDefault(x => x.Key == itemPaciente.id_historiaClinica);
                    mtv.Remove(itemPaciente.id_historiaClinica);
                    itemPaciente.nombreMotivoRemision += " " + a.Value.nombreMotivoRemision;
                    mtv.Add(itemPaciente.id_historiaClinica, itemPaciente);
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




        public JsonResult listarHistoriasClinicasSegunUsuario()
        {
            HC = new HistoriaClinicaBO();
            List<ListadoHistoriasClinicas> listPc = new List<ListadoHistoriasClinicas>();
            if (User.IsInRole("Administrador"))
            {
                listPc = HC.listarHCAdmin();
            }

            if (User.IsInRole("Docente"))
            {
                listPc = HC.listarHCDocente();
            }

            if (User.IsInRole("Estudiante psicologo"))
            {
                listPc = HC.listarHCEstudiante();
            }

            return Json(listPc, JsonRequestBehavior.AllowGet);
        }




        public ActionResult listaHistoriasClinicasInactivas()
        {
            return View();
        }




        public JsonResult listarHistoriasClinicasInactivasSegunUsuario()
        {

            HC = new HistoriaClinicaBO();
            List<ListadoHistoriasClinicas> listPc = new List<ListadoHistoriasClinicas>();
            if (User.IsInRole("Administrador"))
            {
                listPc = HC.listarHCInactivasAdmin();
            }

            if (User.IsInRole("Docente"))
            {
                listPc = HC.listarHCInactivasDocente();
            }

            if (User.IsInRole("Estudiante psicologo"))
            {
                listPc = HC.listarHCInactivasEstudiante();
            }
            return Json(listPc, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult AsignarUsuarioPost(string usr)
        {
            return View("AsignarUsuarioPost");
        }




        [HttpPost]
        public ActionResult ElementosConsultarPost()
        {

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                string GridHtml = "Prueba"; 
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(new Paragraph("Hola"));
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
            
        }


        public ActionResult historiaClinica(string numeroHC) {
            HistoriaClinicaVM historiaCl = new HistoriaClinicaVM();
            HC = new HistoriaClinicaBO();

            var listaIngresoCl = HC.listarIngresoClinica();
            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == numeroHC select item).LastOrDefault();
            var ingreso = (from item in listaIngresoCl where item.id_paciente == paciente.numeroHistoriaClinica && item.estadoHC == false select item).LastOrDefault();
            
            historiaCl.ingresoClinica = ingreso;
            historiaCl.paciente = paciente;
            ViewBag.idPaciente = paciente.numeroHistoriaClinica;
            ViewBag.estadoDocumentoGen = ingreso.estadoDocumentoGeneral;
            return View(historiaCl);
        }

        

        public ActionResult remitirPaciente(string id) {

            RemisionPaciente remisionPac = new RemisionPaciente();
            List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
            SelectListItem items;
            HC = new HistoriaClinicaBO();

            var listaTipoDocumento = HC.listaTiposDocumento();

            foreach (var item in listaTipoDocumento)
            {
                items = new SelectListItem();
                items.Text = item.tipo;
                items.Value = item.idDocumento.ToString();
                listaItemsDocumento.Add(items);
            }

            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == id select item).FirstOrDefault();
            var ingresos = (from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item).LastOrDefault();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();

            var fechN = (ingresos.fechaIngreso).ToString();
            var fechnStr = DateTime.Parse(fechN);
            string format = "yyyy-MM-dd";
            var fecha = fechnStr.ToString(format);

            ViewBag.usr = usuario;
            ViewBag.fechaIniciopsicoterapia = fecha;
            remisionPac.paciente = paciente;
            remisionPac.ingresoClinica = ingresos;
            ViewBag.ItemDocumento = listaItemsDocumento.ToList();
            return View("PacienteRemitido", remisionPac);
        }



        public ActionResult documentoGeneral(string id)
        {
            HC = new HistoriaClinicaBO();
            diagBo = new DiagnosticoBO();
            DocumentoGeneralVM documentoGeneralVM = new DocumentoGeneralVM();
            List<SelectListItem> listaItemsSexo = new List<SelectListItem>();
            List<SelectListItem> listaItemsPaises = new List<SelectListItem>();
            List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
            List<SelectListItem> listaItemsOcupacion = new List<SelectListItem>();
            List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
            List<SelectListItem> listaItemsEps = new List<SelectListItem>();
            List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
            List<SelectListItem> listaItemsNivelEscolaridad = new List<SelectListItem>();
            List<SelectListItem> listaItemsValidation = new List<SelectListItem>();
            List<SelectListItem> listaItemsDiagnostico = new List<SelectListItem>();
            List<SelectListItem> listaItemsCategorizacion = new List<SelectListItem>();
            SelectListItem validacion;
            SelectListItem items;

            var ingreso = (from item in HC.listarIngresoClinica() where item.id_paciente == id select item).LastOrDefault();
            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == ingreso.id_paciente select item).LastOrDefault();
            var remitido = (from item in HC.listarRemitido() where item.id_paciente == paciente.numeroHistoriaClinica select item).LastOrDefault();
            var consultante = (from item in HC.listarConsultante() where item.numeroDocumentoPaciente == paciente.numeroHistoriaClinica select item).LastOrDefault();
            var estrategiaIngreso = (from item in HC.listarIngresoEstrategiasEvaluacion() where item.id_ingreso == ingreso.idIngresoClinica select item).FirstOrDefault();
            var consulta = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingreso.idIngresoClinica select item).FirstOrDefault();

            documentoGeneralVM.ingresoClinica = ingreso;
            documentoGeneralVM.paciente = paciente;

            var fechIngresoCl = (ingreso.fechaIngreso).ToString();
            var fechInStr = DateTime.Parse(fechIngresoCl);
            string format1 = "yyyy-MM-dd";
            var fechaIngreso = fechInStr.ToString(format1);

            var fechN = (paciente.fechaNacimiento).ToString();
            var fechnStr = DateTime.Parse(fechN);
            string format = "yyyy-MM-dd";
            var fecha = fechnStr.ToString(format);



            var listaBarrio = HC.listarBarrios();
            foreach (var item in listaBarrio)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idBarrio;
                listaItemsBarrios.Add(items);
            }


            var listaCiudades = HC.listarCiudades();
            foreach (var item in listaCiudades)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idCiudad;
                listaItemsCuidades.Add(items);
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


            var listaSexo = HC.listarSexo();
            foreach (var item in listaSexo)
            {
                items = new SelectListItem();
                items.Text = item.sexo;
                items.Value = item.id_Sexo.ToString();
                listaItemsSexo.Add(items);
            }


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


            var listaOcupacion = HC.listarOcupacion();
            foreach (var item in listaOcupacion)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.id_Ocupacion.ToString();
                listaItemsOcupacion.Add(items);
            }

            var listaNivelEscolaridad = HC.listarNivelEscolaridad();
            foreach (var item in listaNivelEscolaridad)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idNivelEscolaridad.ToString();
                listaItemsNivelEscolaridad.Add(items);
            }


            var listaCategorizacion = HC.listarCategorizacion();
            foreach (var item in listaCategorizacion)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.id_CategorizacionCAP.ToString();
                listaItemsCategorizacion.Add(items);
            }


            var listaDiagnostico = diagBo.listarDiagnostico();
            foreach (var item in listaDiagnostico)
            {
                items = new SelectListItem();
                items.Text = item.Codigo;
                items.Value = item.Codigo;
                listaItemsDiagnostico.Add(items);
            }


            ViewBag.ItemLocalidades = listaItemsLocalidades.ToList();
            ViewBag.ItemBarrios = listaItemsBarrios.ToList();
            ViewBag.ItemEps = listaItemsEps.ToList();
            ViewBag.ItemPaises = listaItemsPaises.ToList();
            ViewBag.ItemCiudades = listaItemsCuidades.ToList();
            ViewBag.Sexo = listaItemsSexo.ToList();
            ViewBag.NivelEscolaridad = listaItemsNivelEscolaridad.ToList();
            ViewBag.Ocupacion = listaItemsOcupacion.ToList();
            ViewBag.ItemValidacion = listaItemsValidation.ToList();
            ViewBag.ItemCategorizacion = listaItemsCategorizacion.ToList();
            ViewBag.ItemDiagnostico = listaItemsDiagnostico.ToList();

            ViewBag.fechaNacimiento = fecha;
            ViewBag.fechaIngreso = fechaIngreso;


            if (remitido != null)
            {
                documentoGeneralVM.remitido = remitido;
                var fechR = (remitido.fechaRemision).ToString();
                var fechRStr = DateTime.Parse(fechR);
                string format2 = "yyyy-MM-dd";
                var fechaRemision = fechnStr.ToString(format2);
                ViewBag.fechaRemision = fechaRemision;
            }

            if (consultante != null)
            {
                documentoGeneralVM.consultante = consultante;
            }


            if (estrategiaIngreso != null)
            {
                documentoGeneralVM.estrategiaEva = estrategiaIngreso;
            }


            if (consulta != null)
            {
                documentoGeneralVM.consulta = consulta;
            }

            if (ingreso.estadoDocumentoGeneral == true) {
                ViewBag.estadoDocumento = "1";
            }

            return View(documentoGeneralVM);
        }



        public ActionResult guardarDocumentoGeneral(DocumentoGeneralVM documentoGeneral, string NumeroHCP, string Diag, string Cat)
        {
            HC = new HistoriaClinicaBO();
            
            var ingreso = (from item in HC.listarIngresoClinica() where item.id_paciente == NumeroHCP where item.estadoHC == false select item).LastOrDefault();
            ingreso.estadoCivil = documentoGeneral.ingresoClinica.estadoCivil;
            ingreso.diagnostico = Diag;
            ingreso.categorizacionCAP = Cat;
            ingreso.estadoDocumentoGeneral = true;
            ingreso.religion = documentoGeneral.ingresoClinica.religion;
            ingreso.problematicaActual = documentoGeneral.ingresoClinica.problematicaActual;
            ingreso.historiaPersonal = documentoGeneral.ingresoClinica.historiaPersonal;
            ingreso.antecedentes = documentoGeneral.ingresoClinica.antecedentes;
            ingreso.historiaFamiliar = documentoGeneral.ingresoClinica.historiaFamiliar;
            ingreso.genograma = documentoGeneral.ingresoClinica.genograma;
            HC.modificarIngresoCl(ingreso);

            documentoGeneral.consulta.id_ingresoClinica = ingreso.idIngresoClinica;
            HC.agregarConsulta(documentoGeneral.consulta);

            documentoGeneral.estrategiaEva.id_ingreso = ingreso.idIngresoClinica;
            HC.agregarEstrategiaIngreso(documentoGeneral.estrategiaEva);

            return View("DocumentoGeneralCreado");
        }


        public ActionResult cierreCasoHC()
        {
            return View();
        }


        public ActionResult InformeSesion() {
            return View();
        }

        //public ActionResult ingresoHistoriaClinica() {

        //    return View();
        //}
    }
}