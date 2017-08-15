using iTextSharp.text;
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
            

            var listaPaciente = from item in HC.listarPaciente() where item.numeroDocumento == identificacion select item;
            paciente = listaPaciente.LastOrDefault();

            if (paciente != null) {
                var listaIngresoClinica = from item in HC.listarIngresoClinica() where paciente.id_Paciente == item.id_paciente select item;
                ingresoClVal = listaIngresoClinica.LastOrDefault();

                var listaCierreHC = from item in HC.listarCierres() where ingresoClVal.idIngresoClinica == item.id_ingresoClinica select item;
                cierreHC = listaCierreHC.LastOrDefault();
            }
            

            if (cierreHC.id_ingresoClinica != 0)
            {
                if (cierreHC.estadoHC == true) {
                    recepcion.paciente = paciente;
                    if (paciente != null)
                    {
                        //var pacienteIngreso = from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                        //ingresoCli = pacienteIngreso.LastOrDefault();
                        recepcion.ingresoClinica = ingresoCli;

                        //var remitidoP = from item in HC.listarRemitido() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                        //remitido = remitidoP.LastOrDefault();
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
            else {
                recepcion.paciente = paciente;
                if (paciente != null)
                {
                    //var pacienteIngreso = from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                    //ingresoCli = pacienteIngreso.LastOrDefault();
                    recepcion.ingresoClinica = ingresoCli;

                    //var remitidoP = from item in HC.listarRemitido() where item.id_paciente == paciente.numeroHistoriaClinica select item;
                    //remitido = remitidoP.LastOrDefault();
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
            //consultantePaciente consultantePa;
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            Consecutivo consecutivo = new Consecutivo();

            var listaPacientes = HC.listarPaciente();
            if (model.paciente.numeroDocumento != Documento)
            {
                var listaP = (from item in listaPacientes where item.numeroHistoriaClinica == Documento select item).FirstOrDefault();
                model.paciente.fechaNacimiento = listaP.fechaNacimiento;
                model.paciente.numeroHistoriaClinica = listaP.numeroHistoriaClinica;
                //model.consecutivo.numeroConsecutivo = model.consecutivo.numeroConsecutivo + 1;
            }
            else {
                model.paciente.numeroHistoriaClinica = String.IsNullOrEmpty(Documento) ? null : Documento;
            }
            
            var pacienteExistente = from p in listaPacientes where p.numeroHistoriaClinica == model.paciente.numeroHistoriaClinica select p;
            Paciente pacienteEx = new Paciente();
            pacienteEx = pacienteExistente.LastOrDefault();
            if (model.paciente.tieneEps == "NO") {
                model.paciente.id_Eps = "No tiene";
            }
            if (pacienteEx != null)
            {
                HC.modificarRecepcionCasoModel(model, pacienteEx);
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
                    ViewBag.ItemDocumento = listaItemsDocumento.ToList();
                    ViewBag.Pac = model.paciente.numeroDocumento;
                    var remPaciente = new RemisionPaciente();
                    remPaciente.paciente = model.paciente;
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
            var listaP = (from item in HC.listarPaciente() where item.numeroDocumento == modelRemision.paciente.numeroDocumento select item).LastOrDefault();
            var ingresoPaciente = (from item in HC.listarIngresoClinica() where item.id_paciente == listaP.id_Paciente select item).LastOrDefault();
            var cierreHC = (from item in HC.listarCierres() where item.id_ingresoClinica == ingresoPaciente.idIngresoClinica select item).LastOrDefault();
            HC.modificarCierre(cierreHC);
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
                    //remisionPaciente.id_paciente = modelRemision.paciente.numeroDocumento;
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
            Dictionary<int, MotivoRemisionVM> mtv = new Dictionary<int, MotivoRemisionVM>();

            var listaRemisiones = HC.listarRemisiones();
            var listaMotivosRemisiones = HC.listarMotivosRemisiones();
            var ingresoClinica = HC.listarIngresoClinica();
            var listaPaciente = HC.listarPaciente();
            foreach (var item in listaRemisiones)
            {
                motivoRemisionVM = new MotivoRemisionVM();
                foreach (var itemlmr in listaMotivosRemisiones)
                {
                    if (itemlmr.idMotivoRemision == item.motivoRemision) {
                        motivoRemisionVM.id = item.id_ingresoClinica;
                        var ingreClinicaMotivoRem = (from pa in ingresoClinica where pa.idIngresoClinica == item.id_ingresoClinica select pa).FirstOrDefault();
                        if (ingreClinicaMotivoRem != null) {
                            motivoRemisionVM.nombrePaciente = (from pa in listaPaciente where pa.id_Paciente == ingreClinicaMotivoRem.id_paciente select pa.nombre).FirstOrDefault() + " ";
                            motivoRemisionVM.nombrePaciente += (from pa in listaPaciente where pa.id_Paciente == ingreClinicaMotivoRem.id_paciente select pa.apellido).FirstOrDefault() + " ";
                            motivoRemisionVM.nombreMotivoRemision = itemlmr.nombre;
                            motivoRemisionVM.lugarRemitido = item.nombreInsitucionRemitida;
                            motivoRemisionVM.fecha = item.fechaRemitido;
                            listaMotivoRemision.Add(motivoRemisionVM);
                        }
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
                    itemPaciente.nombreMotivoRemision += " " + a.Value.nombreMotivoRemision;
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


        public JsonResult listarHistoriasClinicasSegunUsuario()
        {
            HC = new HistoriaClinicaBO();
            List<Paciente> listPc = new List<Paciente>();
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
            //List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            //List<Paciente> listaPaciente = new List<Paciente>();
            //HC = new HistoriaClinicaBO();
            //var ingresoClinica = HC.listarIngresoClinica();
            //var cierresHC = (from item in HC.listarCierres() where item.estadoHC == false select item).ToList();
            //var user = System.Web.HttpContext.Current.User.Identity.GetUserId();

            //var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();
            //var pacienteUser = (from item in HC.listarPaciente() where item.idUser == user select item).ToList();


            //foreach (var itemIngr in ingresoClinica)
            //{
            //    foreach (var item in cierresHC)
            //    {
            //        if (item.id_ingresoClinica == itemIngr.idIngresoClinica) {
            //            listaIngresoClinica.Add(itemIngr);
            //            break;
            //        }
            //    }
            //}

            //foreach (var itemPAC in HC.listarPaciente()) {
            //    foreach (var itemIngre in listaIngresoClinica) {
            //        if (itemPAC.id_Paciente == itemIngre.id_paciente) {
            //            if(itemPAC.idUser == user)
            //            {
            //                itemPAC.idUser = usuario;
            //                listaPaciente.Add(itemPAC);
            //                break;
            //            }
            //        }
            //    }
            //}

        }

        public ActionResult listaHistoriasClinicasInactivas()
        {
            return View();
        }

        public JsonResult listarHistoriasClinicasInactivasSegunUsuario()
        {

            HC = new HistoriaClinicaBO();
            List<Paciente> listPc = new List<Paciente>();
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
                listPc = HC.listarHCEstudiante();
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


        public ActionResult historiaClinica() {
            return View();
        }

        public ActionResult documentoGeneral() {
            return View();
        }

        public ActionResult remitirPaciente() {
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
            ViewBag.ItemDocumento = listaItemsDocumento.ToList();
            return View("PacienteRemitido");
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