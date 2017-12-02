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
using System.Web.UI;

namespace MacapSoftCAPUAN.Controllers
{
    [Authorize]
    public class HistoriaClinicaController : Controller
    {
        // GET: HistoriaClinica
        public HistoriaClinicaBO HC;
        public DiagnosticoBO diagBo;

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult Index()
        {
            return View();
        }



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ingresoPaciente() {
            return View();
        }



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult IngresoPacientes(string identificacion)
        {

            HC = new HistoriaClinicaBO();
            Paises pais;
            Localidades localidad;
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
                if (paciente.estadoHC == true)
                {
                    pais = new Paises();
                    var paises = HC.listarPaises();
                    var ciudades = HC.listarCiudades();
                    var departamentos = HC.listarDepartamentos();
                    var ciudad = (from item in ciudades where item.idCiudad == paciente.id_ciudad select item).FirstOrDefault();
                    if (ciudad != null) {
                        var departamento = (from item in departamentos where item.idDepartamento == ciudad.id_Departamento select item).FirstOrDefault();
                        pais = (from item in paises where item.idPais == departamento.id_pais select item).FirstOrDefault();
                    }
                    recepcion.paciente = paciente;

                    if (ingresoClVal != null) {
                        recepcion.ingresoClinica = ingresoClVal;                       
                    }

                    if (paciente != null)
                    {
                        recepcion.ingresoClinica = ingresoClVal;
                        recepcion.remitido = remitido;
                    }

                    localidad = new Localidades();
                    var localidades = HC.listarLocalidades();
                    var barrios = HC.listarBarrios();
                    var barrio = (from item in barrios where item.idBarrio == ingresoClVal.id_barrio select item).FirstOrDefault();
                    if (barrio != null)
                    {
                        localidad = (from item in localidades where item.idLocalidad == barrio.id_localidad select item).FirstOrDefault();
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
                        if (item.tipo != "RC" && item.tipo != "TI" && item.tipo != "NUIP")
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
                        var numeroConsecutivo = (from item in HC.listarConsecutivo() where item.idConsecutivo == paciente.consecutivo select item.numeroConsecutivo).LastOrDefault();
                        if (numeroConsecutivo != null ) {
                            ViewBag.itemConsecutivo = numeroConsecutivo;
                        }
                        
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
                        var paisSelec = (from item in HC.listarPaises() where item.nombrePais == pais.nombrePais select item.idPais).LastOrDefault();
                        ViewBag.Pais = paisSelec;//pais.nombrePais;
                        var localidadSelec = (from item in localidades where item.idLocalidad == localidad.idLocalidad select item.idLocalidad).LastOrDefault();
                        var departamento = (from item in HC.listarDepartamentos() where item.idDepartamento == recepcion.paciente.id_departamento select item).FirstOrDefault();
                        ViewBag.Localidad = localidadSelec;
                        ViewBag.DepartamentoValor = departamento.idDepartamento;
                        ViewBag.DepartamentoTexto = departamento.nombre;
                        ViewBag.CiudadValor = ciudad.idCiudad;
                        ViewBag.CiudadTexto = ciudad.nombre;
                        ViewBag.barrioValor = barrio.idBarrio;
                        ViewBag.barrioTexto = barrio.nombre;
                        ViewBag.numeroHC = recepcion.paciente.numeroHistoriaClinica;
                        ViewBag.nombre = recepcion.paciente.nombre;
                        ViewBag.apellido = recepcion.paciente.apellido;
                        ViewBag.direccion = recepcion.ingresoClinica.direccion;
                        ViewBag.email = recepcion.ingresoClinica.email;
                        ViewBag.profesion = recepcion.ingresoClinica.profesion;
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
                            if (!(String.IsNullOrEmpty(recepcion.ingresoClinica.motivoConsulta)))
                            {
                                var motConsulta = recepcion.ingresoClinica.motivoConsulta.Replace('\n', ' ');
                                var motConsultaClean = motConsulta.Replace('\r', ' ');
                                ViewBag.motivoConsulta = motConsultaClean;
                            }

                            if (!(String.IsNullOrEmpty(recepcion.ingresoClinica.observaciones)))
                            {
                                var observaciones = recepcion.ingresoClinica.observaciones.Replace('\n', ' ');
                                var observacionesClean = observaciones.Replace('\r', ' ');
                                ViewBag.Observaciones = observacionesClean;
                            }
                            //ViewBag.motivoConsulta = recepcion.ingresoClinica.motivoConsulta;
                            //ViewBag.Observaciones = recepcion.ingresoClinica.observaciones;
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
                    if (item.tipo != "RC" && item.tipo != "TI" && item.tipo != "NUIP")
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
                    //if (recepcion.ingresoClinica != null)
                    //{
                    //    ViewBag.motivoConsulta = recepcion.ingresoClinica.motivoConsulta;
                    //    ViewBag.Observaciones = recepcion.ingresoClinica.observaciones;
                    //}
                    return View(recepcion);
                }
                return View(recepcion);
            }
        }



        //Método en el cual se guarda el ingreso "Recepción de caso de un paciente", sea nuevo o antiguo.
        [HttpPost]
        public ActionResult IngresoPacientesCreado(RecepcionCaso model, string Informacion, string Documento, string EstadoBarrio)
        {
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            List<PermisosUsuariosPaciente> listaPermisoUsuario = new List<PermisosUsuariosPaciente>();
            PermisosUsuariosPaciente permisoUsuario = new PermisosUsuariosPaciente();
            Consultante consultante = new Consultante();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            Consecutivo consecutivo = new Consecutivo();
            Paciente pacienteEx = new Paciente();
            var listaPacientes = HC.listarPaciente();
            string validacionVista = "";
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
                if (EstadoBarrio == "1")
                {
                    Barrios barrio = new Barrios();
                    barrio.idBarrio = model.ingresoClinica.id_barrio;
                    barrio.id_localidad = "Otros";
                    barrio.nombre = model.ingresoClinica.id_barrio;
                    HC.guardarBarrio(barrio);
                }
                HC.modificarRecepcionCasoModel(model);
                permisoUsuario.id_aplicationUser = model.ingresoClinica.idUser;
                permisoUsuario.id_paciente = model.paciente.numeroHistoriaClinica;
                listaPermisoUsuario.Add(permisoUsuario);
                var listaUsuarioExistente = (from item in HC.permisosUsuariosPac() where item.id_aplicationUser == permisoUsuario.id_aplicationUser && item.id_paciente == permisoUsuario.id_paciente select item).ToList();
                if (listaUsuarioExistente.Count == 0) {
                    HC.agregarEstrategiaIngreso(listaPermisoUsuario);
                }
            }
            else {

                try{
                    if (EstadoBarrio == "1")
                    {
                        var barrioExi = (from item in HC.listarBarrios() where item.idBarrio == model.ingresoClinica.id_barrio select item).LastOrDefault();
                        if (!(barrioExi != null))
                        {
                            Barrios barrio = new Barrios();
                            barrio.idBarrio = model.ingresoClinica.id_barrio;
                            barrio.id_localidad = "Otros";
                            barrio.nombre = model.ingresoClinica.id_barrio;
                            HC.guardarBarrio(barrio);
                            HC.crearRecepcionCasoModel(model);
                            permisoUsuario.id_aplicationUser = model.ingresoClinica.idUser;
                            permisoUsuario.id_paciente = model.paciente.numeroHistoriaClinica;
                            listaPermisoUsuario.Add(permisoUsuario);
                            HC.agregarEstrategiaIngreso(listaPermisoUsuario);
                        }
                        else {
                            validacionVista = "No se pudo crear ya existe el barrio";
                        }
                    }
                    else {
                        HC.crearRecepcionCasoModel(model);
                        permisoUsuario.id_aplicationUser = model.ingresoClinica.idUser;
                        permisoUsuario.id_paciente = model.paciente.numeroHistoriaClinica;
                        listaPermisoUsuario.Add(permisoUsuario);
                        HC.agregarEstrategiaIngreso(listaPermisoUsuario);
                    }
                    
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
                    if (validacionVista == "No se pudo crear ya existe el barrio") {
                        return View("IngresoPacientesCreado");
                    }
                    else
                    {
                        return View("IngresoPacienteCreado");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }

        }



        //Método en el cual se guarda la remisión del paciente.
        [HttpPost]
        public ActionResult RemitirPaciente(RemisionPaciente modelRemision, string concMremison) {
            HC = new HistoriaClinicaBO();
            List<Remision> listaResmisionPaciente = new List<Remision>();
            Remision remisionPaciente;
            MotivosRemisiones motivoRem;
            var lstRP = concMremison.Split(';');
            var listaP = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == modelRemision.paciente.numeroHistoriaClinica select item).LastOrDefault();
            var ingresoPaciente = (from item in HC.listarIngresoClinica() where item.id_paciente == listaP.numeroHistoriaClinica select item).LastOrDefault();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item).FirstOrDefault();
            var cierreHC = (from item in HC.listarCierres() where item.id_ingresoClinica == ingresoPaciente.idIngresoClinica select item).LastOrDefault();
            var numeroConsultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingresoPaciente.idIngresoClinica select item).Count();
            var numeroInasistencias = (from item in HC.listarInasistencias() where item.id_ingresoClinica == ingresoPaciente.idIngresoClinica select item).Count();
            modelRemision.cierre.idUsuario = usuario.Id;//System.Web.HttpContext.Current.User.Identity.GetUserId();
            modelRemision.paciente.estadoHC = true;
            
            HC.remitirModificarPaciente(modelRemision.paciente);
            //HC.modificarPaciente(modelRemision.paciente);
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

            if (cierreHC != null) {
                cierreHC.fechaFinalizaionPsicoterapia = modelRemision.cierre.fechaFinalizaionPsicoterapia;
                cierreHC.fechaInicioPsicoterapia = modelRemision.cierre.fechaInicioPsicoterapia;
                cierreHC.id_UsuarioCierraCaso = modelRemision.cierre.idUsuario;
                cierreHC.idUsuario = modelRemision.cierre.idUsuario;
                if (numeroConsultas != 0) {
                    cierreHC.numeroSesionesRealizadas = numeroConsultas.ToString();
                }
                HC.modificarCierre(cierreHC);
            }

            return View("PacienteRemitidoExitoso");
        }


        [Authorize(Roles = "Administrador")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
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
                    itemPaciente.nombreMotivoRemision += ", " + a.Value.nombreMotivoRemision;
                    mtv.Add(itemPaciente.id_historiaClinica, itemPaciente);
                }
            }
            
            return Json(mtv.Values, JsonRequestBehavior.AllowGet);
        }



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
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



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult listaHistoriasClinicas() {
            HC = new HistoriaClinicaBO();
            List<SelectListItem> listaItemsDocentes = new List<SelectListItem>();
            List<SelectListItem> listaItemsEstudiantes = new List<SelectListItem>();
            SelectListItem items;
            SelectListItem documento;

            documento = new SelectListItem();
            documento.Text = "Seleccione";
            documento.Value = "";
            listaItemsDocentes.Add(documento);
            listaItemsEstudiantes.Add(documento);

            var listaUsuariosDocente = (from item in HC.listarUsuarioDocente() where item.Activo == true select item).ToList();
            var listaUsuariosEstudiante = (from item in HC.listarUsuarioEstudiante() where item.Activo == true select item).ToList();

            if (listaUsuariosDocente != null) {
                foreach (var item in listaUsuariosDocente)
                {
                    items = new SelectListItem();
                    items.Text = item.UserName;
                    items.Value = item.Id;
                    listaItemsDocentes.Add(items);
                }
                ViewBag.docente = listaItemsDocentes.ToList();
            }

            if (listaUsuariosEstudiante != null)
            {
                foreach (var item in listaUsuariosEstudiante)
                {
                    items = new SelectListItem();
                    items.Text = item.UserName;
                    items.Value = item.Id;
                    listaItemsEstudiantes.Add(items);
                }
                ViewBag.estudiante = listaItemsEstudiantes.ToList();
            }
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



        [Authorize(Roles = "Administrador")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult listaHistoriasClinicasInactivas()
        {
            return View();
        }



        [Authorize(Roles = "Administrador")]
        public JsonResult listarHistoriasClinicasInactivasSegunUsuario()
        {

            HC = new HistoriaClinicaBO();
            List<ListadoHistoriasClinicas> listPc = new List<ListadoHistoriasClinicas>();
            if (User.IsInRole("Administrador"))
            {
                listPc = HC.listarHCInactivasAdmin();
            }

            //if (User.IsInRole("Docente"))
            //{
            //    listPc = HC.listarHCInactivasDocente();
            //}
            //
            //if (User.IsInRole("Estudiante psicologo"))
            //{
            //    listPc = HC.listarHCInactivasEstudiante();
            //}
            return Json(listPc, JsonRequestBehavior.AllowGet);
        }



        //Metodo que permite asignar un usuario (estudiante, docente) a una HC.
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public JsonResult AsignarUsuarioPost(string id, string docente, string estudiante)
        {
            HC = new HistoriaClinicaBO();
            string mensajeError = "";
            var listadoUsuariosPermisosDocente = (from item in HC.permisosUsuariosPac() where item.id_paciente == id && item.id_aplicationUser == docente select item).ToList();
            var listadoUsuariosPermisosEstudiante = (from item in HC.permisosUsuariosPac() where item.id_paciente == id && item.id_aplicationUser == estudiante select item).ToList();
            List<PermisosUsuariosPaciente> lstPermisos = new List<PermisosUsuariosPaciente>();
            PermisosUsuariosPaciente doc = new PermisosUsuariosPaciente();
            PermisosUsuariosPaciente pac = new PermisosUsuariosPaciente();

            doc.id_aplicationUser = docente;
            doc.id_paciente = id;

            pac.id_aplicationUser = estudiante;
            pac.id_paciente = id;

            
            
            if (listadoUsuariosPermisosDocente.Count > 0)
            {
                mensajeError = "No se puede asignar el usuario porque ya tiene este usuario docente asignado.";
            }
            else {
                lstPermisos.Add(doc);
            }

            if (listadoUsuariosPermisosEstudiante.Count > 0)
            {
                mensajeError += " No se puede asignar el usuario porque ya tiene este usuario estudiante asignado.";
            }
            else {
                lstPermisos.Add(pac);
            }

            if (lstPermisos.Count>=1) {
                HC.agregarEstrategiaIngreso(lstPermisos);
                mensajeError = "Se asignó correctamente los usuarios";
            }
            
            return Json(mensajeError, JsonRequestBehavior.AllowGet);
        }


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult asignacionUsuarioExitoso() {
            return View();
        }



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ElementosConsultarPost(string gifs, string cnp)
        {
            List<Consulta> listaConsultasIngreso = new List<Consulta>();
            List<Inasistencias> listaInasistencias = new List<Inasistencias>();
            List<CategorizacionHC> listaCategorizacionHC;
            List<Consulta> listaConsulta;
            List<Consulta> listaConsultaGeneralPorPaciente = new List<Consulta>();
            List<CategorizacionHC> listaCategorizacion;
            Dictionary<string, string> diccionarioConsultasDiagnostico;
            Dictionary<string, string> diccionarioCategorizacionCAP;
            Dictionary<string, string> diccionarioRemisiones;
            Dictionary<string, string> diccionarioCierres;
            Remitido remitido;

            string diagnosticoConsultas = "";
            string categorizacionesHC = "";

            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.LETTER);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            HC = new HistoriaClinicaBO();
            diagBo = new DiagnosticoBO();

            var estadosConsulta = gifs.Split(';');
            string fechaRemisionRemitente = "";
            string sexoConsultante = "";
            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == cnp select item).FirstOrDefault();
            var ingresoClinica = (from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica  select item).ToList();
            var consultas = HC.listarConsultas();
            var listaEntidadesRemitentes = HC.listarRemisiones();
            var listaRemitidos = HC.listarRemitido();
            var inasistencias = HC.listarInasistencias();
            var consultasDiagnósticos = HC.listarConsultaDiagnosticos();
            var categorizacionHC = HC.listarCetegorizacionesHC();
            var categorizacionesNombre = HC.listarCategorizacion();


            var sexo = (from item in HC.listarSexo() where item.id_Sexo == paciente.id_sexo select item.sexo).FirstOrDefault();
            var ciudad = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item.nombre).FirstOrDefault();
            var ciudadObject = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item).FirstOrDefault();
            var departamento = (from item in HC.listarDepartamentos() where item.idDepartamento == ciudadObject.id_Departamento select item).FirstOrDefault();
            //var pais = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item.id_pais).FirstOrDefault();
            var nombrePais = (from item in HC.listarPaises() where item.idPais == departamento.id_pais select item.nombrePais).FirstOrDefault();

            document.Open();
            Paragraph title = new Paragraph();
            Paragraph informacionParrafo = new Paragraph();
            Paragraph subtitulos = new Paragraph();
            title.Alignment = Element.ALIGN_CENTER;
            title.Font = FontFactory.GetFont("Arial", 13);
            informacionParrafo.Font = FontFactory.GetFont("Arial", 9);
            subtitulos.Font = FontFactory.GetFont("Arial", 11);

            int ingr = 0;

            title = new Paragraph();
            title.Alignment = Element.ALIGN_CENTER;
            title.Font = FontFactory.GetFont("Arial", 13);
            title.Add("\nHistoria clínica" + " - " + paciente.nombre + " " + paciente.apellido + "\n\n");
            document.Add(title);

            foreach (var ingrClinica in ingresoClinica)
            {

                listaConsulta = new List<Consulta>();
                remitido = new Remitido();
                diccionarioConsultasDiagnostico = new Dictionary<string, string>();
                diccionarioCategorizacionCAP = new Dictionary<string, string>();
                listaCategorizacionHC = new List<CategorizacionHC>();
                listaCategorizacion = new List<CategorizacionHC>();
                diagnosticoConsultas = "";
                categorizacionesHC = "";

                var nivelEscolaridad = (from item in HC.listarNivelEscolaridad() where item.idNivelEscolaridad == ingrClinica.id_NivelEscolaridad select item.nombre).FirstOrDefault();
                var ocupacion = (from item in HC.listarOcupacion() where item.id_Ocupacion == ingrClinica.id_ocupacion select item.nombre).FirstOrDefault();
                var barrio = (from item in HC.listarBarrios() where item.idBarrio == ingrClinica.id_barrio select item.nombre).FirstOrDefault();
                var barrioNombre = (from item in HC.listarBarrios() where item.idBarrio == ingrClinica.id_barrio select item).FirstOrDefault();
                var localidad = (from item in HC.listarLocalidades() where item.idLocalidad == barrioNombre.id_localidad select item.nombre).FirstOrDefault();
                var eps = (from item in HC.listarEps() where item.IdEPS == ingrClinica.id_Eps select item.nombre).FirstOrDefault();
                var entidadRemitente = (from item in HC.listarRemitido() where item.id_ingresoCl == ingrClinica.idIngresoClinica select item).LastOrDefault();
                var consultante = (from item in HC.listarConsultante() where item.cedula == ingrClinica.id_Consultante select item).LastOrDefault();
                var estrategiasIngreso = (from item in HC.listarIngresoEstrategiasEvaluacion() where item.id_ingreso == ingrClinica.idIngresoClinica select item).LastOrDefault();
                var remisiones = (from item in HC.listarRemisiones() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).ToList();
                var nombreUsuario = (from item in HC.listarUsuario() where item.Id == ingrClinica.idUser select item.nombreUsuario).FirstOrDefault();
                var numeroConsultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).Count();
                var numeroInasistencias = (from item in HC.listarInasistencias() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).Count();


                var fechIngreso = (ingrClinica.fechaIngreso).ToString();
                var fechnStIngreso = DateTime.Parse(fechIngreso);
                string format = "yyyy-MM-dd";
                var fecha = fechnStIngreso.ToString(format);

                var fechNacimiento = (paciente.fechaNacimiento).ToString();
                var fechnStNacimiento = DateTime.Parse(fechNacimiento);
                string formatNafechnStNacimientocimiento = "yyyy-MM-dd";
                var fechaNacimiento = fechnStNacimiento.ToString(formatNafechnStNacimientocimiento);

                
                var validarSexoConsultante = (consultante != null)? (from item in HC.listarSexo() where item.id_Sexo == consultante.id_sexo select item.sexo).FirstOrDefault() : "No tiene sexo";

                foreach (var itemInasistencias in inasistencias)
                {
                    if (ingrClinica.idIngresoClinica == itemInasistencias.id_ingresoClinica)
                    {
                        listaInasistencias.Add(itemInasistencias);
                    }
                }

                foreach (var item in listaRemitidos)
                {
                    if (item.id_ingresoCl == ingrClinica.idIngresoClinica)
                    {
                        remitido = item;
                    }
                }

                if (remitido != null)
                {
                    var fechRemitente = (remitido.fechaRemision).ToString();
                    var fechnRemitente = DateTime.Parse(fechRemitente);
                    string formatNafechnRemitente = "yyyy-MM-dd";
                    fechaRemisionRemitente = fechnRemitente.ToString(formatNafechnRemitente);
                }

                //--------------------------Creación de la recepción de caso o documento general



                

                if (gifs.Contains("1") || gifs.Contains("4"))
                {
                    if (ingr > 0)
                    {
                        document.NewPage();
                    }

                
                //document.Add(new Paragraph(300f,DateTime.Now.ToString()));
                
                    PdfPTable table = new PdfPTable(3);
                    PdfPCell cell = new PdfPCell(new Phrase("Información general de la historia clínica", title.Font));
                    Paragraph texto = new Paragraph();

                    texto.Font = FontFactory.GetFont("Arial", 9);
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("Fecha de recepción: " + fecha, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Número de historia clínica: " + paciente.numeroHistoriaClinica, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Usuario que generó el documento: " + nombreUsuario, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    foreach (var item in consultas)
                    {
                        if (item.id_ingresoClinica == ingrClinica.idIngresoClinica)
                        {
                            //listaConsulta.Add(item);
                            listaConsultaGeneralPorPaciente.Add(item);
                        }
                    }

                    var consultaDocumentoGeneral = (from item in consultas where item.id_ingresoClinica == ingrClinica.idIngresoClinica && item.numeroSesion == 1 select item).ToList();

                    if (consultaDocumentoGeneral.Count > 0)
                    {
                        foreach (var item in consultaDocumentoGeneral)
                        {
                            listaConsulta.Add(item);
                        }
                    }



                    foreach (var item in listaConsulta)
                    {
                        foreach (var item1 in consultasDiagnósticos)
                        {
                            if (item.idConsulta == item1.id_consulta)
                            {
                                //diagnosticoConsultas += diagnosticoConsultas + item1.id_diagnostico+",";
                                if (!(diccionarioConsultasDiagnostico.ContainsKey(item1.id_diagnostico)))
                                {
                                    diccionarioConsultasDiagnostico.Add(item1.id_diagnostico, item1.id_diagnostico);
                                }
                            }
                        }
                    }

                    if (diccionarioConsultasDiagnostico != null)
                    {
                        foreach (var item in diccionarioConsultasDiagnostico)
                        {
                            var nombreDiagnostico = (from item1 in diagBo.listarDiagnostico() where item1.Codigo == item.Key select item1.Nombre).FirstOrDefault();
                            diagnosticoConsultas += item.Value + "-" + nombreDiagnostico + ", ";
                        }

                    }



                    cell = new PdfPCell(new Phrase("Diagnósticos", texto.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var validacionDiagnosticosConsultas = (diagnosticoConsultas != "") ? diagnosticoConsultas : "No se ingresó diagnosticos";
                    cell = new PdfPCell(new Paragraph(validacionDiagnosticosConsultas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    foreach (var item in categorizacionHC)
                    {
                        if (item.id_IngresoClinica == ingrClinica.idIngresoClinica)
                        {
                            listaCategorizacion.Add(item);
                        }
                    }

                    foreach (var item in categorizacionesNombre)
                    {
                        foreach (var item1 in listaCategorizacion)
                        {
                            if (item.id_CategorizacionCAP == item1.id_Categorizacion)
                            {
                                if (!(diccionarioCategorizacionCAP.ContainsKey(item.nombre)))
                                {
                                    diccionarioCategorizacionCAP.Add(item.nombre, item.nombre);
                                    //break;
                                }

                            }
                        }
                    }


                    if (diccionarioCategorizacionCAP != null)
                    {
                        foreach (var item in diccionarioCategorizacionCAP)
                        {
                            categorizacionesHC += item.Value + ", ";
                        }

                    }

                    var validacionCategorizacionConsultas = (categorizacionesHC != "") ? categorizacionesHC : "No se ingresó categorizaciones.";
                    cell = new PdfPCell(new Phrase("Categorización CAP", texto.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(validacionCategorizacionConsultas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Datos personales", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Nombre: " + paciente.nombre + " " + paciente.apellido);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Sexo: " + sexo);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Fecha de nacimiento: " + fechaNacimiento);
                    table.AddCell(texto);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Número de documento: " + ingrClinica.numeroDocumento);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Tipo de documento: " + ingrClinica.id_tipoDocumento);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Lugar de nacimiento: país:" + nombrePais + "- ciudad: " + ciudad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Edad: " + ingrClinica.edad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Estado civil: " + ingrClinica.estadoCivil);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Religion: " + ingrClinica.religion);
                    table.AddCell(texto);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Escolaridad: " + nivelEscolaridad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Profesión: " + ingrClinica.profesion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Ocupación: " + ocupacion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Dirección: " + ingrClinica.direccion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Barrio: " + barrio);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Localidad: " + localidad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Estrato: " + ingrClinica.id_estrato);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Teléfono: " + ingrClinica.telefono);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Email: " + ingrClinica.email);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("EPS: " + ingrClinica.tieneEps);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("EPC: " + ingrClinica.tieneEpc);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Institución: " + eps);
                    table.AddCell(texto);

                    cell = new PdfPCell(new Phrase("Datos de remitido", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var entidadRemitenteNombre = (entidadRemitente != null) ? entidadRemitente.nombreEntidad : "No tiene";
                    cell = new PdfPCell(new Phrase("Institución que remite: " + entidadRemitenteNombre, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    var entidadRemitenteNombreRemitente = (entidadRemitente != null) ? entidadRemitente.nombreRemitente : "No tiene";
                    cell = new PdfPCell(new Phrase("Profesional que remite: " + entidadRemitenteNombreRemitente, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fecha de remisión: " + fechaRemisionRemitente, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Datos consultante", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultanteNombre = (consultante != null) ? consultante.nombre : "No se ingreso nombre";
                    var consultanteApellido = (consultante != null) ? consultante.apellido : " ni apellido";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Nombre: " + consultanteNombre + " " + consultanteApellido);
                    table.AddCell(texto);

                    //var sexoConsultanteValidacion = (sexoConsultante != "") ? sexoConsultante : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Sexo: " + validarSexoConsultante);
                    table.AddCell(texto);

                    var consultanteParentezco = (consultante != null) ? consultante.parentezco : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Parentesco: " + consultanteParentezco);
                    table.AddCell(texto);

                    var consultanteTelefono = (consultante != null) ? consultante.telefono : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Teléfono: " + consultanteTelefono);
                    table.AddCell(texto);

                    var consultanteTipoDocumento = (consultante != null) ? consultante.id_tipoDocumento : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Tipo de documento: " + consultanteTipoDocumento);
                    table.AddCell(texto);

                    var consultanteCedula = (consultante != null) ? consultante.cedula : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Número de documento: " + consultanteCedula);
                    table.AddCell(texto);

                    cell = new PdfPCell(new Phrase("Motivo Consulta", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.motivoConsulta, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Problemática", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.problematicaActual, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Historia personal", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.historiaPersonal, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Antecedentes", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.antecedentes, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Estrategias evaluación", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Pruebas psicométricas");
                    table.AddCell(texto);

                    var estrategiasIngPruebasPsico = (estrategiasIngreso != null) ? estrategiasIngreso.pruebasPsico : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngPruebasPsico, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Cuestionarios");
                    table.AddCell(texto);

                    var estrategiasIngCuestionarios = (estrategiasIngreso != null) ? estrategiasIngreso.cuestionarios : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngCuestionarios, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Pruebas proyectivas");
                    table.AddCell(texto);

                    var estrategiasIngPruebasProyectivas = (estrategiasIngreso != null) ? estrategiasIngreso.pruebasProyectivas : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngPruebasProyectivas, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Exámen mental");
                    table.AddCell(texto);

                    var estrategiasIngExamenMental = (estrategiasIngreso != null) ? estrategiasIngreso.examenMental : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngExamenMental, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Entrevistas");
                    table.AddCell(texto);

                    var estrategiasIngEntrevistas = (estrategiasIngreso != null) ? estrategiasIngreso.entrevistas : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngEntrevistas, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    var consultasResultadoEv = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().resultadoAutoevaluacion : " ";
                    cell = new PdfPCell(new Phrase("Resultados evaluación", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(consultasResultadoEv, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Historia familiar", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.historiaFamiliar, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Hipótesis psicológica", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasHipotesisPsicologica = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().hipotesisPsicologica : " ";
                    cell = new PdfPCell(new Paragraph(consultasHipotesisPsicologica, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Objetivos terapéuticos", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasObjetivosTerapeuticos = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().objetivosTerapeuticos : " ";
                    cell = new PdfPCell(new Paragraph(consultasObjetivosTerapeuticos, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Estrategias y técnicas terapéuticas", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasEstrategiasTecnicasTerapeuticas = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().estrategiasTecnicasTerapeuticas : " ";
                    cell = new PdfPCell(new Paragraph(consultasEstrategiasTecnicasTerapeuticas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Logros alcanzados según consultante", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasLogrosAlcSegConsultante = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().logrosAlcanzadosSegunConsultante : " ";
                    cell = new PdfPCell(new Paragraph(consultasLogrosAlcSegConsultante, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Resúmen", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasResumen = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().resumen : " ";
                    cell = new PdfPCell(new Paragraph(consultasResumen, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Observaciones y recomendaciones", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasObservacionesRecomendaciones = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().observacionesRecomendaciones : " ";
                    cell = new PdfPCell(new Paragraph(consultasObservacionesRecomendaciones, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    document.Add(table);
                    ingr++;
                }
            }
            //--------------------------Creación de las consultas
            //for (int i = 0; i <= numeroConsultas; i++) {
            PdfPCell cell1 = new PdfPCell();
            int i = 0;
            if (gifs.Contains("2"))
            {
                if (!(gifs.Contains("1") || gifs.Contains("4"))) {
                    foreach (var item in consultas)
                    {
                        foreach (var ingrClinica in ingresoClinica)
                        {
                            if (item.id_ingresoClinica == ingrClinica.idIngresoClinica)
                            {
                                //listaConsulta.Add(item);
                                listaConsultaGeneralPorPaciente.Add(item);
                            }
                        }
                    }
                }


                foreach (var item in listaConsultaGeneralPorPaciente)
                {
                    string diagnosticosConsulta = "";
                    var nombreDiag = "";
                    var nombreUsuarioConsulta = (from item1 in HC.listarUsuario() where item1.Id == item.id_User select item1.nombreUsuario).FirstOrDefault();
                    var nombreUsrConsulta = nombreUsuarioConsulta != null ? nombreUsuarioConsulta : "";

                    if ((gifs.Contains("1") || gifs.Contains("4")))
                    {
                        document.NewPage();
                    }
                    else if (i > 0)
                    {
                        document.NewPage();
                    }

                    Paragraph textoConsulta;
                    PdfPTable tableConsultas = new PdfPTable(1);
                    cell1 = new PdfPCell(new Phrase("CONSULTA", title.Font));
                    //cell.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Sesión número" + " " + item.numeroSesion, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Usuario quien antendió la consulta" + ": " + nombreUsrConsulta, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Diagnósticos", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var consultaDiagnostico = (from item1 in HC.listarConsultaDiagnosticos() where item1.id_consulta == item.idConsulta select item1.id_diagnostico).ToList();
                    if (consultaDiagnostico != null)
                    {
                        foreach (var diagnostico in consultaDiagnostico)
                        {
                            nombreDiag = (from diagnos in diagBo.listarDiagnostico() where diagnos.Codigo == diagnostico select diagnos.Nombre).FirstOrDefault();
                            diagnosticosConsulta += diagnostico + "-" + nombreDiag + "*";
                        }
                    }

                    var diagnosticosSesion = (diagnosticosConsulta != "") ? diagnosticosConsulta : "No se ingresaron diagnósticos.";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(diagnosticosSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Objetivos de la sesión", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var objetivoConsultaSesion = (item.objetivoSesion != null) ? item.objetivoSesion : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(objetivoConsultaSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Ejercicios y eventos significativos", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var ejerciciosEventoSesion = (item.ejerciciosEvento != null) ? item.ejerciciosEvento : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(ejerciciosEventoSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Desarrollo y temas tratados", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var desarrolloTemasTratadosSesion = (item.desarrolloTemasTratados != null) ? item.desarrolloTemasTratados : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(desarrolloTemasTratadosSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Próxima sesión", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var tareasProximaSesionSesion = (item.tareasProximaSesion != null) ? item.tareasProximaSesion : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(tareasProximaSesionSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Observaciones", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var observacionesRecomendacionesSesion = (item.observacionesRecomendaciones != null) ? item.observacionesRecomendaciones : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(observacionesRecomendacionesSesion);
                    tableConsultas.AddCell(textoConsulta);

                    document.Add(tableConsultas);
                    i += 1;
                }
            }
            //}
            //--------------------------Creación de las inasistencias
            int j = 0;
            if (gifs.Contains("3"))
            {
                if ((gifs.Contains("1") || gifs.Contains("2") || gifs.Contains("4")))
                {
                    document.NewPage();
                }
                if (j > 1)
                {
                    document.NewPage();
                }
                //document.NewPage();
                foreach (var item in listaInasistencias)
                {
                    var nombreUsuarioInasistencia = (from item1 in HC.listarUsuario() where item1.Id == item.usuario select item1.nombreUsuario).FirstOrDefault();
                    var nombreUsrInasistencia = nombreUsuarioInasistencia != null ? nombreUsuarioInasistencia : "";

                    var fechInasistencia = (item.fechaInasistencia).ToString();
                    var fechnStInasistencia = DateTime.Parse(fechInasistencia);
                    string formatInasistencia = "yyyy-MM-dd";
                    var fechaInasistencia = fechnStInasistencia.ToString(formatInasistencia);

                    //document.NewPage();
                    Paragraph textoInasistencia = new Paragraph();
                    PdfPTable tableInasistencias = new PdfPTable(2);
                    cell1 = new PdfPCell(new Phrase("INASISTENCIA", title.Font));
                    cell1.Colspan = 2;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Fecha de la inasistencia" + " " + fechaInasistencia, subtitulos.Font));
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Usuario quién generó la inasistencia" + ": " + nombreUsrInasistencia, subtitulos.Font));
                    cell1.Colspan = 2;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Paragraph(item.motivo, subtitulos.Font));
                    cell1.Colspan = 3;
                    tableInasistencias.AddCell(cell1);
                    document.Add(tableInasistencias);
                    j += 1;
                }
            }
            //--------------------------Creación de las remisiones 
            int k = 0;
            if (gifs.Contains("5"))
            {
                foreach (var ingrClinica in ingresoClinica)
                {
                    diccionarioRemisiones = new Dictionary<string, string>();
                    if ((gifs.Contains("1") || gifs.Contains("4") || gifs.Contains("2") || gifs.Contains("3")))
                    {
                        document.NewPage();
                    }
                    if (k > 0)
                    {
                        document.NewPage();
                    }
                    var remisiones = (from item in HC.listarRemisiones() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).ToList();
                    if (remisiones.Count > 0)
                    {
                        var fechaRemitido = "";
                        PdfPCell cell2 = new PdfPCell();
                        var concatRemisiones = "";
                        if (remisiones.Count > 0)
                        {
                            var fechRemitido = (remisiones.FirstOrDefault().fechaRemitido).ToString();
                            var fechNRemitido = DateTime.Parse(fechRemitido);
                            string formatRemitido = "yyyy-MM-dd";
                            fechaRemitido = fechNRemitido.ToString(formatRemitido);
                        }
                        else
                        {
                            fechaRemitido = "No tiene fecha";
                        }

                        Paragraph textoRemision = new Paragraph();
                        PdfPTable tableRemision = new PdfPTable(2);
                        cell2 = new PdfPCell(new Phrase("REMISIONES", title.Font));
                        cell2.Colspan = 2;
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Fecha de la remisión" + " " + fechaRemitido, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        foreach (var item in remisiones)
                        {
                            foreach (var item1 in HC.listarMotivosRemisiones())
                            {
                                if (item1.idMotivoRemision == item.motivoRemision)
                                {
                                    if (!(diccionarioRemisiones.ContainsKey(item1.nombre)))
                                    {
                                        diccionarioRemisiones.Add(item1.nombre, item1.nombre);
                                    }
                                }
                            }
                        }

                        foreach (var item in diccionarioRemisiones)
                        {
                            concatRemisiones += item.Value + ",";
                        }

                        cell2 = new PdfPCell(new Phrase("Factores de riesgo", title.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        cell2.Colspan = 3;
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Paragraph(concatRemisiones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var institucionRemitida = remisiones.Count > 0 ? remisiones.FirstOrDefault().nombreInsitucionRemitida : "";
                        cell2 = new PdfPCell(new Paragraph("Institución remitido: " + institucionRemitida, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var servicioRemitido = remisiones.Count > 0 ? remisiones.FirstOrDefault().servicioRemitido : "";
                        cell2 = new PdfPCell(new Paragraph("Servicio remitido: " + servicioRemitido, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var evolucionPaciente = remisiones.Count > 0 ? remisiones.FirstOrDefault().evolucionPaciente : "";
                        cell2 = new PdfPCell(new Paragraph("Evolución del paciente: " + evolucionPaciente, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var aspectosPositivos = remisiones.Count > 0 ? remisiones.FirstOrDefault().aspectosPositivos : "";
                        cell2 = new PdfPCell(new Paragraph("Aspectos positivos: " + aspectosPositivos, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var recomendaciones = remisiones.Count > 0 ? remisiones.FirstOrDefault().recomendaciones : "";
                        cell2 = new PdfPCell(new Paragraph("Recomendaciones: " + recomendaciones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        document.Add(tableRemision);
                        k += 1;
                    }
                }
            }

            //--------------------------Creación de los cierres
            int l = 0;
            if (gifs.Contains("6"))
            {
                foreach (var ingrClinica in ingresoClinica)
                {
                    diccionarioCierres = new Dictionary<string, string>();
                    if ((gifs.Contains("1") || gifs.Contains("4") || gifs.Contains("2") || gifs.Contains("3") || gifs.Contains("5")))
                    {
                        document.NewPage();
                    }
                    if (l > 0)
                    {
                        document.NewPage();
                    }
                    var cierres = (from item in HC.listarCierres() where item.id_ingresoClinica == ingrClinica.idIngresoClinica && item.especificacionMotivoCierre != null select item).ToList();
                    if (cierres.Count > 0)
                    {
                        List<MotivoCierreHistoriaClinica> motivosCierres = new List<MotivoCierreHistoriaClinica>();
                        var fechaIniPsico = "";
                        var fechaFinaliPsico = "";
                        PdfPCell cell2 = new PdfPCell();
                        var concatCierres = "";
                        if (cierres.Count > 0)
                        {
                            var fechIniPsico = (cierres.FirstOrDefault().fechaInicioPsicoterapia).ToString();
                            var fechNIniPsico = DateTime.Parse(fechIniPsico);
                            string formatIniPsico = "yyyy-MM-dd";
                            fechaIniPsico = fechNIniPsico.ToString(formatIniPsico);

                            var fechFinaliPsico = (cierres.FirstOrDefault().fechaFinalizaionPsicoterapia).ToString();
                            var fechNFinaliPsico = DateTime.Parse(fechFinaliPsico);
                            string formatFinaliPsico = "yyyy-MM-dd";
                            fechaFinaliPsico = fechNFinaliPsico.ToString(formatFinaliPsico);

                        }
                        else
                        {
                            fechaIniPsico = "No tiene fecha";
                            fechaFinaliPsico = "No tiene fecha";
                        }

                        Paragraph textoRemision = new Paragraph();
                        PdfPTable tableRemision = new PdfPTable(2);
                        cell2 = new PdfPCell(new Phrase("INFORME Y CIERRE DE HISTORIA CLÍNICA", title.Font));
                        cell2.Colspan = 2;
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Fecha del cierre" + " " + fechaFinaliPsico, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);



                        foreach (var item in HC.listarMotivoCierreHistoriaClinicaBO())
                        {
                            foreach (var item1 in cierres)
                            {
                                if (item.id_Cierre == item1.idCierreHC)
                                {
                                    motivosCierres.Add(item);

                                }
                            }
                        }


                        foreach (var item in HC.listarMotivosCierre())
                        {
                            foreach (var item1 in motivosCierres)
                            {
                                if (item1.id_MotivoCierre == item.idMotivoCierre)
                                {
                                    if (!(diccionarioCierres.ContainsKey(item.Nombre)))
                                    {
                                        diccionarioCierres.Add(item.Nombre, item.Nombre);
                                        //break;
                                    }

                                }
                            }
                        }


                        if (diccionarioCierres != null)
                        {
                            foreach (var item in diccionarioCierres)
                            {
                                concatCierres += item.Value + ",";
                            }

                        }

                        cell2 = new PdfPCell(new Paragraph("Motivos de cierre: " + concatCierres, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var fechaInicio = cierres.Count > 0 ? cierres.FirstOrDefault().fechaInicioPsicoterapia.ToString() : "";
                        cell2 = new PdfPCell(new Paragraph("Fecha de inicio de la psicoterápia: " + fechaIniPsico, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var fechaFin = cierres.Count > 0 ? cierres.FirstOrDefault().fechaFinalizaionPsicoterapia.ToString() : "";
                        cell2 = new PdfPCell(new Paragraph("Fecha de finalización de la psicoterápia: " + fechaFinaliPsico, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var numeroCitas = cierres.FirstOrDefault().numeroCitasAsignadas != null ? cierres.FirstOrDefault().numeroCitasAsignadas : "No tuvo citas asignadas.";
                        cell2 = new PdfPCell(new Paragraph("Número de citas asignadas: " + numeroCitas, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var numeroSesiones = cierres.FirstOrDefault().numeroSesionesRealizadas !=null ? cierres.FirstOrDefault().numeroSesionesRealizadas : "No tuvo citas asignadas.";
                        cell2 = new PdfPCell(new Paragraph("Número de sesiones realizadas: " + numeroSesiones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var especificacionMotivoCierreHC = cierres.Count > 0 ? cierres.FirstOrDefault().especificacionMotivoCierre : "";
                        cell2 = new PdfPCell(new Paragraph("Especificación motivo del cierre: " + especificacionMotivoCierreHC, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var instrumentoEva = cierres.Count > 0 ? cierres.FirstOrDefault().instrumentosEvaluacion : "";
                        cell2 = new PdfPCell(new Paragraph("Instrumentos evaluación: " + instrumentoEva, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var resultadosObtenidos = cierres.Count > 0 ? cierres.FirstOrDefault().resultadoObtenidoEvaluacion : "";
                        cell2 = new PdfPCell(new Paragraph("Resultados obtenidos: " + resultadosObtenidos, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        document.Add(tableRemision);
                        l += 1;
                    }
                }
            }

            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", "HistoriaClínicaNúmero" + paciente.numeroHistoriaClinica + ".pdf");
        }
        


        public ActionResult historiaClinica(string numeroHC) {
            HistoriaClinicaVM historiaCl = new HistoriaClinicaVM();
            HC = new HistoriaClinicaBO();
            try
            {
                var listarInasistencia = HC.listarInasistencias();
                var listaIngresoCl = HC.listarIngresoClinica();
                var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == numeroHC && item.estadoHC == false select item).LastOrDefault();
                var ingreso = (from item in listaIngresoCl where item.id_paciente == paciente.numeroHistoriaClinica && item.estadoHC == false select item).LastOrDefault();
                var eps = (from item in HC.listarEps() where item.IdEPS == ingreso.id_Eps select item.nombre).LastOrDefault();

                var validacionEps = (eps != null) ? eps : "No se ingreso una eps";

                if (listarInasistencia != null) {
                    var conteoInasistencia = (from item in listarInasistencia where item.id_ingresoClinica == ingreso.idIngresoClinica select item).Count();
                    if (conteoInasistencia != 0) {
                        ViewBag.conteo = conteoInasistencia;
                    }
                }
                
                historiaCl.ingresoClinica = ingreso;
                historiaCl.paciente = paciente;
                ViewBag.idPaciente = paciente.numeroHistoriaClinica;
                ViewBag.estadoDocumentoGen = ingreso.estadoDocumentoGeneral;
                ViewBag.eps = validacionEps;
                return View(historiaCl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("ErrorValidacionHistoriaClinica");
            }
            
        }


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
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


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult cierreCasoHC(string id, int inasistencia)
        {
            HC = new HistoriaClinicaBO();
            CierreCasoVM cierreCasoVM = new CierreCasoVM();
            List<SelectListItem> listaItemsOcupacion = new List<SelectListItem>();
            SelectListItem items;

            var listaOcupacion = HC.listarOcupacion();

            foreach (var item in listaOcupacion)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.id_Ocupacion.ToString();
                listaItemsOcupacion.Add(items);
            }

            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == id select item).FirstOrDefault();
            var ingresoClinica = (from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item).LastOrDefault();
            var numeroConsultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).Count();
            var numeroInasistencias = (from item in HC.listarInasistencias() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).Count();
            var consultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Email).FirstOrDefault();

            var fechNa = (paciente.fechaNacimiento).ToString();
            var fechanaStr = DateTime.Parse(fechNa);
            string formatFNa = "yyyy-MM-dd";
            var fechaNac = fechanaStr.ToString(formatFNa);


            cierreCasoVM.paciente = paciente;
            cierreCasoVM.ingresoClinica = ingresoClinica;

            var fechN = (ingresoClinica.fechaIngreso).ToString();
            var fechnStr = DateTime.Parse(fechN);
            string format = "yyyy-MM-dd";
            var fecha = fechnStr.ToString(format);

            ViewBag.ItemsOcupacion = listaItemsOcupacion.ToList();
            ViewBag.fechaIniciopsicoterapia = fecha;
            ViewBag.fechaNacimiento = fechaNac;
            ViewBag.ItemNumHC = paciente.numeroHistoriaClinica;
            ViewBag.UsuarioCierre = usuario;

            if (ingresoClinica.id_NivelEscolaridad != 0) {
                var escolaridad = (from item in HC.listarNivelEscolaridad() where item.idNivelEscolaridad == ingresoClinica.id_NivelEscolaridad select item.nombre).FirstOrDefault();
                ViewBag.Escolaridad = escolaridad;
            }

            if (numeroConsultas > 0)
            {
                ViewBag.ItemNumeroSesion = numeroConsultas;
            }


            if (numeroInasistencias > 0)
            {
                ViewBag.ItemNumeroInasistencias = numeroInasistencias;
            }


            if (inasistencia == 1) {
                ViewBag.estadoInasistencias = "1";
            }

            return View(cierreCasoVM);
        }


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult cierreCaso(CierreCasoVM cierreCaso, string concMotCierre, string NumeroHCP) {
            //MotivosCierre motivosCierre;
            MotivoCierreHistoriaClinica motivoCierreHC;
            List<MotivoCierreHistoriaClinica> listMotivoCierreHC =  new List<MotivoCierreHistoriaClinica>();
            HC = new HistoriaClinicaBO();

            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == NumeroHCP select item).FirstOrDefault();
            var ingresoClinica = (from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica && item.estadoHC == false select item).LastOrDefault();
            var cierresHC = (from item in HC.listarCierres() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).FirstOrDefault();
            var consultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).ToList();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Email == cierreCaso.cierreHC.id_UsuarioCierraCaso select item.Id).FirstOrDefault();
            var lstRP = concMotCierre.Split(';');

            foreach (var item in lstRP)
            {
                if (item != "")
                {
                    //motivosCierre = new MotivosCierre();
                    motivoCierreHC = new MotivoCierreHistoriaClinica();
                    motivoCierreHC.id_MotivoCierre = int.Parse(item);
                    motivoCierreHC.id_Cierre = cierresHC.idCierreHC;
                    listMotivoCierreHC.Add(motivoCierreHC);
                }
            }
            
            paciente.estadoHC = true;
            cierresHC.id_UsuarioCierraCaso = usuario;
            cierresHC.fechaInicioPsicoterapia = cierreCaso.cierreHC.fechaInicioPsicoterapia;
            cierresHC.fechaFinalizaionPsicoterapia = cierreCaso.cierreHC.fechaFinalizaionPsicoterapia;
            cierresHC.especificacionMotivoCierre = cierreCaso.cierreHC.especificacionMotivoCierre;
            cierresHC.numeroSesionesRealizadas = cierreCaso.cierreHC.numeroSesionesRealizadas;
            cierresHC.numeroCitasAsignadas = cierreCaso.cierreHC.numeroCitasAsignadas;
            cierresHC.instrumentosEvaluacion = cierreCaso.cierreHC.instrumentosEvaluacion;
            cierresHC.resultadoObtenidoEvaluacion = cierreCaso.cierreHC.resultadoObtenidoEvaluacion;

            HC.remitirModificarPaciente(paciente);
            HC.modificarCierreHCIngresoClinica(ingresoClinica);
            HC.modificarCierre(cierresHC);
            HC.agregarMotivosRemision(listMotivoCierreHC);
            return View();
        }


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
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
            var remitido = (from item in HC.listarRemitido() where item.id_ingresoCl == ingreso.idIngresoClinica select item).LastOrDefault();
            var consultante = (from item in HC.listarConsultante() where item.cedula == ingreso.id_Consultante select item).LastOrDefault();
            var estrategiaIngreso = (from item in HC.listarIngresoEstrategiasEvaluacion() where item.id_ingreso == ingreso.idIngresoClinica select item).LastOrDefault();
            var consulta = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingreso.idIngresoClinica && item.numeroSesion == 1 select item).LastOrDefault();

            Paises pais = new Paises();
            var paises = HC.listarPaises();
            var ciudades = HC.listarCiudades();
            var departamentos = HC.listarDepartamentos();
            var ciudad = (from item in ciudades where item.idCiudad == paciente.id_ciudad select item).FirstOrDefault();
            if (ciudad != null)
            {
                var departamento = (from item in departamentos where item.idDepartamento == ciudad.id_Departamento select item).FirstOrDefault();
                pais = (from item in paises where item.idPais == departamento.id_pais select item).FirstOrDefault();
                //pais = (from item in paises where item.idPais == ciudad.id_pais select item).FirstOrDefault();
            }

            Localidades localidad = new Localidades();
            var localidades = HC.listarLocalidades();
            var barrios = HC.listarBarrios();
            var barrio = (from item in barrios where item.idBarrio == ingreso.id_barrio select item).FirstOrDefault();
            if (barrio != null)
            {
                localidad = (from item in localidades where item.idLocalidad == barrio.id_localidad select item).FirstOrDefault();
            }


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

            var paisSelec = (from item in HC.listarPaises() where item.nombrePais == pais.nombrePais select item.idPais).LastOrDefault();
            ViewBag.Pais = paisSelec;//pais.nombrePais;
            var localidadSelec = (from item in localidades where item.idLocalidad == localidad.idLocalidad select item.idLocalidad).LastOrDefault();
            ViewBag.Localidad = localidadSelec;
            
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
                var fechaRemision = fechRStr.ToString(format2);
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

            if (ingreso.religion != null) {
                ViewBag.estadoDiagnósticos = "1";
            }

            if (ingreso.estadoDocumentoGeneral == true) {
                ViewBag.estadoDocumento = "1";
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.motivoConsulta))) {
                var motConsulta = documentoGeneralVM.ingresoClinica.motivoConsulta.Replace('\n', ' ');
                var motConsultaClean = motConsulta.Replace('\r',' ');
                documentoGeneralVM.ingresoClinica.motivoConsulta = motConsultaClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.problematicaActual)))
            {
                var probActual = documentoGeneralVM.ingresoClinica.problematicaActual.Replace('\n', ' ');
                var probActualClean = probActual.Replace('\r', ' ');
                documentoGeneralVM.ingresoClinica.problematicaActual = probActualClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.historiaPersonal)))
            {
                var histPersonal = documentoGeneralVM.ingresoClinica.historiaPersonal.Replace('\n', ' ');
                var histPersonalClean = histPersonal.Replace('\r', ' ');
                documentoGeneralVM.ingresoClinica.historiaPersonal = histPersonalClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.antecedentes)))
            {
                var antc = documentoGeneralVM.ingresoClinica.antecedentes.Replace('\n', ' ');
                var antcClean = antc.Replace('\r', ' ');
                documentoGeneralVM.ingresoClinica.antecedentes = antcClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.resultadoAutoevaluacion)))
            {
                var resAutoEva = documentoGeneralVM.consulta.resultadoAutoevaluacion.Replace('\n', ' ');
                var resAutoEvaClean = resAutoEva.Replace('\r', ' ');
                documentoGeneralVM.consulta.resultadoAutoevaluacion = resAutoEvaClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.historiaFamiliar)))
            {
                var histFam = documentoGeneralVM.ingresoClinica.historiaFamiliar.Replace('\n', ' ');
                var histFamClean = histFam.Replace('\r', ' ');
                documentoGeneralVM.ingresoClinica.historiaFamiliar = histFamClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.ingresoClinica.genograma)))
            {
                var gen = documentoGeneralVM.ingresoClinica.genograma.Replace('\n', ' ');
                var genClean = gen.Replace('\r', ' ');
                documentoGeneralVM.ingresoClinica.genograma = genClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.hipotesisPsicologica)))
            {
                var hipPsico = documentoGeneralVM.consulta.hipotesisPsicologica.Replace('\n', ' ');
                var hipPsicoClean = hipPsico.Replace('\r', ' ');
                documentoGeneralVM.consulta.hipotesisPsicologica = hipPsicoClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.objetivosTerapeuticos)))
            {
                var objTera = documentoGeneralVM.consulta.objetivosTerapeuticos.Replace('\n', ' ');
                var objTeraClean = objTera.Replace('\r', ' ');
                documentoGeneralVM.consulta.objetivosTerapeuticos = objTeraClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.estrategiasTecnicasTerapeuticas)))
            {
                var estrTec = documentoGeneralVM.consulta.estrategiasTecnicasTerapeuticas.Replace('\n', ' ');
                var estrTecClean = estrTec.Replace('\r', ' ');
                documentoGeneralVM.consulta.estrategiasTecnicasTerapeuticas = estrTecClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.logrosAlcanzadosSegunObjetivosTerapeuticos)))
            {
                var logrObjeTera = documentoGeneralVM.consulta.logrosAlcanzadosSegunObjetivosTerapeuticos.Replace('\n', ' ');
                var logrObjeTeraClean = logrObjeTera.Replace('\r', ' ');
                documentoGeneralVM.consulta.logrosAlcanzadosSegunObjetivosTerapeuticos = logrObjeTeraClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.logrosAlcanzadosSegunConsultante)))
            {
                var lgrConsultante = documentoGeneralVM.consulta.logrosAlcanzadosSegunConsultante.Replace('\n', ' ');
                var lgrConsultanteClean = lgrConsultante.Replace('\r', ' ');
                documentoGeneralVM.consulta.logrosAlcanzadosSegunConsultante = lgrConsultanteClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.resumen)))
            {
                var resum = documentoGeneralVM.consulta.resumen.Replace('\n', ' ');
                var resumClean = resum.Replace('\r', ' ');
                documentoGeneralVM.consulta.resumen = resumClean;
            }

            if (!(String.IsNullOrEmpty(documentoGeneralVM.consulta.observacionesRecomendaciones)))
            {
                var obsRecomendaciones = documentoGeneralVM.consulta.observacionesRecomendaciones.Replace('\n', ' ');
                var obsRecomendacionesClean = obsRecomendaciones.Replace('\r', ' ');
                documentoGeneralVM.consulta.observacionesRecomendaciones = obsRecomendacionesClean;
            }

            return View(documentoGeneralVM);
        }



        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult guardarDocumentoGeneral(DocumentoGeneralVM documentoGeneral, string NumeroHCP, string Diag, string Cat, string estadoDocumento)
        {
            HC = new HistoriaClinicaBO();
            
            var ingreso = (from item in HC.listarIngresoClinica() where item.id_paciente == NumeroHCP where item.estadoHC == false select item).LastOrDefault();
            //ingreso.estadoCivil = documentoGeneral.ingresoClinica.estadoCivil;
            //ingreso.diagnostico = Diag;
            //ingreso.categorizacionCAP = Cat;
            documentoGeneral.diagnosticos = Diag;
            documentoGeneral.categorizacionCAP = Cat;

            if (estadoDocumento == "1")
            {
                ingreso.estadoDocumentoGeneral = true;
            }
            else {
                ingreso.estadoDocumentoGeneral = false;
            }

            ingreso.motivoConsulta = documentoGeneral.ingresoClinica.motivoConsulta;
            ingreso.religion = documentoGeneral.ingresoClinica.religion;
            ingreso.estadoCivil = documentoGeneral.ingresoClinica.estadoCivil;
            ingreso.problematicaActual = documentoGeneral.ingresoClinica.problematicaActual;
            ingreso.historiaPersonal = documentoGeneral.ingresoClinica.historiaPersonal;
            ingreso.antecedentes = documentoGeneral.ingresoClinica.antecedentes;
            ingreso.historiaFamiliar = documentoGeneral.ingresoClinica.historiaFamiliar;
            ingreso.genograma = documentoGeneral.ingresoClinica.genograma;
            HC.modificarIngresoCl(ingreso);

            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Id).FirstOrDefault();
            documentoGeneral.consulta.id_ingresoClinica = ingreso.idIngresoClinica;
            documentoGeneral.consulta.id_User = usuario;
            documentoGeneral.ingresoClinica.idIngresoClinica = ingreso.idIngresoClinica;
            var documentoConsultaGeneralExistente = (from item in HC.listarConsultas() where item.id_ingresoClinica == documentoGeneral.ingresoClinica.idIngresoClinica && item.numeroSesion == 1 select item).ToList().LastOrDefault();

            if (documentoConsultaGeneralExistente != null)
            {
                documentoGeneral.consulta.idConsulta = documentoConsultaGeneralExistente.idConsulta;
                HC.modificarConsultaDocumentoGeneral(documentoGeneral.consulta);
            }
            else {
                HC.agregarConsulta(documentoGeneral.consulta);
            } 

            documentoGeneral.estrategiaEva.id_ingreso = ingreso.idIngresoClinica;

            if (!(String.IsNullOrEmpty(Diag)) && !(String.IsNullOrEmpty(Cat)))
            {
                HC.crearDiagnosticosYcategorizacion(ingreso.idIngresoClinica, documentoGeneral.diagnosticos, documentoGeneral.categorizacionCAP);
            }

            var ingresoEstrategiasEv = (from item in HC.listarIngresoEstrategiasEvaluacion() where item.id_ingreso == documentoGeneral.ingresoClinica.idIngresoClinica select item).ToList().LastOrDefault();

            if (ingresoEstrategiasEv != null)
            {
                HC.eliminarIngresoEstrategiaEvDocumentoGeneral(ingresoEstrategiasEv);
            }
            HC.agregarEstrategiaIngreso(documentoGeneral.estrategiaEva);

            return View("DocumentoGeneralCreado");
        }





        //Metodo con el cual se realzia la validación de parámetros y direccionamiento a la vista de informe de sesión.
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult InformeSesion(string id) {
            List<SelectListItem> listaItemsDiagnostico = new List<SelectListItem>();
            Consulta consulta = new Consulta();
            List<string> diagnosticos = new List<string>();
            SelectListItem items;

            HC = new HistoriaClinicaBO();
            diagBo = new DiagnosticoBO();
            var ingreso = (from item in HC.listarIngresoClinica() where item.id_paciente == id select item).LastOrDefault();
            var numeroConsultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingreso.idIngresoClinica select item).Count();
            var Consultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingreso.idIngresoClinica select item).ToList();
            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == ingreso.id_paciente select item).LastOrDefault();
            var informesSesión = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingreso.idIngresoClinica select item).ToList();
            var consultasDiagnosticos = HC.listarConsultaDiagnosticos();

            if (consultasDiagnosticos != null) {
                foreach (var item in Consultas) {
                    var numeroConsulta = "Número de consulta "+item.numeroSesion+": ";
                    diagnosticos.Add(numeroConsulta);
                    foreach (var item1 in consultasDiagnosticos)
                    {
                        if(item.idConsulta == item1.id_consulta)
                        {
                            var itemDiagnóstico = item1.id_diagnostico;
                            diagnosticos.Add(itemDiagnóstico);
                        }
                    }
                    if (diagnosticos.Last().Contains("Número de consulta"))
                    {
                        diagnosticos.Add("No se ingreso un nuevo diagnóstico o se había ingresado uno ya existente.");
                    }
                }
            }

            


            var listaDiagnostico = diagBo.listarDiagnostico();
            foreach (var item in listaDiagnostico)
            {
                items = new SelectListItem();
                items.Text = item.Codigo;
                items.Value = item.Codigo;
                listaItemsDiagnostico.Add(items);
            }
            ViewBag.ItemDiagnostico = listaItemsDiagnostico.ToList();
            if (numeroConsultas > 0 ) {
                ViewBag.ItemNumeroSesion = numeroConsultas;
            }

            if (informesSesión != null) {
                ViewBag.Diagnosticos = diagnosticos;
            }

            ViewBag.ItemNumHC = paciente.numeroHistoriaClinica;
            consulta.id_ingresoClinica = ingreso.idIngresoClinica;
            return View();
        }


        //Método con el cual se guarda el informe de sesión.
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult guardarInformeSesion(Consulta consulta, string NumeroHCP, string Diag) {
            HC = new HistoriaClinicaBO();

            var IngresoCl = (from item in HC.listarIngresoClinica() where item.id_paciente == NumeroHCP select item).LastOrDefault();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item.Id).FirstOrDefault();

            var ingresoCl = long.Parse(IngresoCl.idIngresoClinica.ToString());
            consulta.id_ingresoClinica = IngresoCl.idIngresoClinica;
            consulta.id_User = usuario;
            HC.agregarConsulta(consulta);
            if (Diag != "") {
                HC.crearDiagnosticosInformeSesion(ingresoCl, Diag);
            }
            
            
            return View();
        }


        //Método con el cual se genera la inasistencia.
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult generarInasistenciaClinica(string id, string motivoIn, DateTime Fecha)
        {
            Inasistencias inasistencia = new Inasistencias();
            HC = new HistoriaClinicaBO();
            var listaPaciente = HC.listarPaciente();
            var listaIngresos = HC.listarIngresoClinica();
            var listarInasistencia = HC.listarInasistencias();
            
            var paciente = (from item in listaPaciente where item.numeroHistoriaClinica == id select item).FirstOrDefault();
            var ingresoCl = (from item in listaIngresos where paciente.numeroHistoriaClinica == item.id_paciente select item).LastOrDefault();
            var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var usuario = (from item in HC.listarUsuario() where item.Id == user select item).FirstOrDefault();
            if (ingresoCl != null)
            {
                inasistencia.fechaInasistencia = Fecha;
                inasistencia.motivo = motivoIn;
                inasistencia.id_ingresoClinica = ingresoCl.idIngresoClinica;
                inasistencia.usuario = usuario.Id;
            }
            HC.agregarInasistencia(inasistencia);
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult consultarInasistencias(string id) {
            ViewBag.nhc = id;
            return View();
        }


        public JsonResult consultarInasistenciasPost(string hc)
        {

            HC = new HistoriaClinicaBO();

            var listaInasistencias = HC.listarInasistencias();
            var listaIngreso = HC.listarIngresoClinica();
            var ingresoUsuario = (from item in listaIngreso where item.id_paciente == hc select item).LastOrDefault();
            var listaInasistenciasUsuario = (from item in listaInasistencias where item.id_ingresoClinica == ingresoUsuario.idIngresoClinica select item).ToList();
            return Json(listaInasistenciasUsuario, JsonRequestBehavior.AllowGet);
        }


        //Consultar los departamentos según los paises
        public JsonResult consultarDepartamentosPorPais(string pais)
        {
            List<SelectListItem> listaItemsDepartamento = new List<SelectListItem>();
            SelectListItem items;
            SelectListItem seleccion = new SelectListItem();
            HC = new HistoriaClinicaBO();
            var listaDepartamentos = (from item in HC.listarDepartamentos() where item.id_pais == pais select item).ToList();

            seleccion.Text = "Seleccione";
            seleccion.Value = "";

            listaItemsDepartamento.Add(seleccion);

            foreach (var item in listaDepartamentos)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idDepartamento;
                listaItemsDepartamento.Add(items);
            }
            ViewBag.ItemDepartamento = listaItemsDepartamento;
            return Json(listaItemsDepartamento, JsonRequestBehavior.AllowGet);
        }


        //Consultar las ciudades según los paises
        public JsonResult consultarCiudadesPorPais(string departamento)
        {
            List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
            SelectListItem items;
            HC = new HistoriaClinicaBO();
            var listaCiudades= (from item in  HC.listarCiudades() where item.id_Departamento == departamento select item).ToList();

            
            foreach (var item in listaCiudades)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idCiudad;
                listaItemsCuidades.Add(items);
            }
            ViewBag.ItemCiudades = listaCiudades;
            return Json(listaCiudades, JsonRequestBehavior.AllowGet);
        }



        //Consultar los barrios según la localidad
        public JsonResult consultarBarriosLocalidad(string localidad)
        {
            List<SelectListItem> listaItemsCuidades = new List<SelectListItem>();
            SelectListItem items;
            HC = new HistoriaClinicaBO();
            var listaBarrios = (from item in HC.listarBarrios() where item.id_localidad == localidad select item).ToList();


            foreach (var item in listaBarrios)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idBarrio;
                listaItemsCuidades.Add(items);
            }
            ViewBag.ItemCiudades = listaBarrios;
            return Json(listaBarrios, JsonRequestBehavior.AllowGet);
        }


        //Listar usuarios asignados a la historia clínica
        [Authorize(Roles = "Administrador")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult listarUsuariosAsignadosDeHC(string id)
        {
            ViewBag.nhc = id;
            return View();
        }

        //Solo puede ver el listado de los usuarios asignados el rol de administrador
        [Authorize(Roles = "Administrador")]
        public JsonResult listarUsuariosAsignadosHC(string id)
        {
            List<PermisosUsuarioPacienteVM> listaPermisosUsuarioPaciente = new List<PermisosUsuarioPacienteVM>();
            PermisosUsuarioPacienteVM usuarioPermisos;
            HC = new HistoriaClinicaBO();
            var listaPermisosId = (from item in HC.permisosUsuariosPac() where item.id_paciente == id select item).ToList();
            
            foreach (var item in listaPermisosId) {
                usuarioPermisos = new PermisosUsuarioPacienteVM();
                var usuarioNombre = (from itemUsuarioL in HC.listarUsuario() where itemUsuarioL.Id == item.id_aplicationUser select itemUsuarioL).FirstOrDefault();
                usuarioPermisos.usuarioAsignado = usuarioNombre.Email;
                usuarioPermisos.numeroHistoriaClinica = id;
                listaPermisosUsuarioPaciente.Add(usuarioPermisos);
            }

            return Json(listaPermisosUsuarioPaciente, JsonRequestBehavior.AllowGet);
        }


        //[Authorize(Roles = "Administrador")]
        //[HttpPost]
        //public JsonResult eliminarUsuarioAsignado(string nhc, string UsuarioAsig)
        //{
        //    HC = new HistoriaClinicaBO();
        //    var usuarioAsignado = (from item in HC.permisosUsuariosPac() where item.id_aplicationUser == UsuarioAsig && item.id_paciente == nhc select item).FirstOrDefault();
        //    HC.eliminarUsuarioAsignado(usuarioAsignado);
        //    return Json("Ok", JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Roles = "Administrador")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult eliminarUsuarioAsig(string nhc, string UsuarioAsig)
        {
            HC = new HistoriaClinicaBO();
            var usuarioId = (from item in HC.listarUsuario() where item.Email == UsuarioAsig select item.Id).FirstOrDefault();
            var usuarioAsignado = (from item in HC.permisosUsuariosPac() where item.id_aplicationUser == usuarioId && item.id_paciente == nhc select item).FirstOrDefault();
            HC.eliminarUsuarioAsignado(usuarioAsignado);
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult ElementosConsultarHistoriaClinicaInactivas(string gifs, string cnp)
        {
            List<Consulta> listaConsultasIngreso = new List<Consulta>();
            List<Inasistencias> listaInasistencias = new List<Inasistencias>();
            List<CategorizacionHC> listaCategorizacionHC;
            List<Consulta> listaConsulta;
            List<Consulta> listaConsultaGeneralPorPaciente = new List<Consulta>();
            List<CategorizacionHC> listaCategorizacion;
            Dictionary<string, string> diccionarioConsultasDiagnostico;
            Dictionary<string, string> diccionarioCategorizacionCAP;
            Dictionary<string, string> diccionarioRemisiones;
            Dictionary<string, string> diccionarioCierres;
            Remitido remitido;

            string diagnosticoConsultas = "";
            string categorizacionesHC = "";

            MemoryStream workStream = new MemoryStream();
            Document document = new Document(PageSize.LETTER);
            PdfWriter.GetInstance(document, workStream).CloseStream = false;

            HC = new HistoriaClinicaBO();
            diagBo = new DiagnosticoBO();

            var estadosConsulta = gifs.Split(';');
            string fechaRemisionRemitente = "";
            string sexoConsultante = "";
            var paciente = (from item in HC.listarPaciente() where item.numeroHistoriaClinica == cnp select item).FirstOrDefault();
            var ingresoClinica = (from item in HC.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica && item.estadoHC == true select item).ToList();
            var consultas = HC.listarConsultas();
            var listaEntidadesRemitentes = HC.listarRemisiones();
            var listaRemitidos = HC.listarRemitido();
            var inasistencias = HC.listarInasistencias();
            var consultasDiagnósticos = HC.listarConsultaDiagnosticos();
            var categorizacionHC = HC.listarCetegorizacionesHC();
            var categorizacionesNombre = HC.listarCategorizacion();


            var sexo = (from item in HC.listarSexo() where item.id_Sexo == paciente.id_sexo select item.sexo).FirstOrDefault();
            var ciudad = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item.nombre).FirstOrDefault();
            var ciudadObject = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item).FirstOrDefault();
            var departamento = (from item in HC.listarDepartamentos() where item.idDepartamento == ciudadObject.id_Departamento select item).FirstOrDefault();
            //var pais = (from item in HC.listarCiudades() where item.idCiudad == paciente.id_ciudad select item.id_pais).FirstOrDefault();
            var nombrePais = (from item in HC.listarPaises() where item.idPais == departamento.id_pais select item.nombrePais).FirstOrDefault();

            document.Open();
            Paragraph title = new Paragraph();
            Paragraph informacionParrafo = new Paragraph();
            Paragraph subtitulos = new Paragraph();
            title.Alignment = Element.ALIGN_CENTER;
            title.Font = FontFactory.GetFont("Arial", 13);
            informacionParrafo.Font = FontFactory.GetFont("Arial", 9);
            subtitulos.Font = FontFactory.GetFont("Arial", 11);

            title = new Paragraph();
            title.Alignment = Element.ALIGN_CENTER;
            title.Font = FontFactory.GetFont("Arial", 13);
            title.Add("\nHistoria clínica" + " - " + paciente.nombre + " " + paciente.apellido + "\n\n");
            document.Add(title);

            int ingr = 0;
            foreach (var ingrClinica in ingresoClinica)
            {

                listaConsulta = new List<Consulta>();
                remitido = new Remitido();
                diccionarioConsultasDiagnostico = new Dictionary<string, string>();
                diccionarioCategorizacionCAP = new Dictionary<string, string>();
                listaCategorizacionHC = new List<CategorizacionHC>();
                listaCategorizacion = new List<CategorizacionHC>();
                diagnosticoConsultas = "";
                categorizacionesHC = "";

                var nivelEscolaridad = (from item in HC.listarNivelEscolaridad() where item.idNivelEscolaridad == ingrClinica.id_NivelEscolaridad select item.nombre).FirstOrDefault();
                var ocupacion = (from item in HC.listarOcupacion() where item.id_Ocupacion == ingrClinica.id_ocupacion select item.nombre).FirstOrDefault();
                var barrio = (from item in HC.listarBarrios() where item.idBarrio == ingrClinica.id_barrio select item.nombre).FirstOrDefault();
                var barrioNombre = (from item in HC.listarBarrios() where item.idBarrio == ingrClinica.id_barrio select item).FirstOrDefault();
                var localidad = (from item in HC.listarLocalidades() where item.idLocalidad == barrioNombre.id_localidad select item.nombre).FirstOrDefault();
                var eps = (from item in HC.listarEps() where item.IdEPS == ingrClinica.id_Eps select item.nombre).FirstOrDefault();
                var entidadRemitente = (from item in HC.listarRemitido() where item.id_ingresoCl == ingrClinica.idIngresoClinica select item).LastOrDefault();
                var consultante = (from item in HC.listarConsultante() where item.cedula == ingrClinica.id_Consultante select item).LastOrDefault();
                var estrategiasIngreso = (from item in HC.listarIngresoEstrategiasEvaluacion() where item.id_ingreso == ingrClinica.idIngresoClinica select item).LastOrDefault();
                var remisiones = (from item in HC.listarRemisiones() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).ToList();
                var nombreUsuario = (from item in HC.listarUsuario() where item.Id == ingrClinica.idUser select item.nombreUsuario).FirstOrDefault();
                var numeroConsultas = (from item in HC.listarConsultas() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).Count();
                var numeroInasistencias = (from item in HC.listarInasistencias() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).Count();


                var fechIngreso = (ingrClinica.fechaIngreso).ToString();
                var fechnStIngreso = DateTime.Parse(fechIngreso);
                string format = "yyyy-MM-dd";
                var fecha = fechnStIngreso.ToString(format);

                var fechNacimiento = (paciente.fechaNacimiento).ToString();
                var fechnStNacimiento = DateTime.Parse(fechNacimiento);
                string formatNafechnStNacimientocimiento = "yyyy-MM-dd";
                var fechaNacimiento = fechnStNacimiento.ToString(formatNafechnStNacimientocimiento);

                var validarSexoConsultante = (consultante != null) ? (from item in HC.listarSexo() where item.id_Sexo == consultante.id_sexo select item.sexo).FirstOrDefault() : "No tiene sexo";

                foreach (var itemInasistencias in inasistencias)
                {
                    if (ingrClinica.idIngresoClinica == itemInasistencias.id_ingresoClinica)
                    {
                        listaInasistencias.Add(itemInasistencias);
                    }
                }

                foreach (var item in listaRemitidos)
                {
                    if (item.id_ingresoCl == ingrClinica.idIngresoClinica)
                    {
                        remitido = item;
                    }
                }

                if (remitido != null)
                {
                    var fechRemitente = (remitido.fechaRemision).ToString();
                    var fechnRemitente = DateTime.Parse(fechRemitente);
                    string formatNafechnRemitente = "yyyy-MM-dd";
                    fechaRemisionRemitente = fechnRemitente.ToString(formatNafechnRemitente);
                }
                
            if (gifs.Contains("1") || gifs.Contains("4"))
            {
                

                //--------------------------Creación de la recepción de caso o documento general
                
                if (ingr > 0)
                {
                    document.NewPage();
                }
                   
                //document.Add(new Paragraph(300f,DateTime.Now.ToString()));
                
                    PdfPTable table = new PdfPTable(3);
                    PdfPCell cell = new PdfPCell(new Phrase("Información general de la historia clínica", title.Font));
                    Paragraph texto = new Paragraph();

                    texto.Font = FontFactory.GetFont("Arial", 9);
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("Fecha de recepción: " + fecha, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Número de historia clínica: " + paciente.numeroHistoriaClinica, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Usuario que generó el documento: " + nombreUsuario, texto.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    foreach (var item in consultas) {
                        if (item.id_ingresoClinica == ingrClinica.idIngresoClinica) {
                            //listaConsulta.Add(item);
                            listaConsultaGeneralPorPaciente.Add(item);
                        }
                    }


                    var consultaDocumentoGeneral = (from item in consultas where item.id_ingresoClinica == ingrClinica.idIngresoClinica && item.numeroSesion == 1 select item).ToList();

                    if (consultaDocumentoGeneral.Count > 0)
                    {
                        foreach (var item in consultaDocumentoGeneral)
                        {
                                listaConsulta.Add(item);
                        }
                    }



                    foreach (var item in listaConsulta)
                    {
                        foreach (var item1 in consultasDiagnósticos)
                        {
                            if (item.idConsulta == item1.id_consulta)
                            {
                                //diagnosticoConsultas += diagnosticoConsultas + item1.id_diagnostico+",";
                                if (!(diccionarioConsultasDiagnostico.ContainsKey(item1.id_diagnostico)))
                                {
                                    diccionarioConsultasDiagnostico.Add(item1.id_diagnostico, item1.id_diagnostico);
                                }
                                //break;
                            }
                        }
                    }

                    if (diccionarioConsultasDiagnostico != null)
                    {
                        foreach (var item in diccionarioConsultasDiagnostico)
                        {
                            var nombreDiagnostico = (from item1 in diagBo.listarDiagnostico() where item1.Codigo == item.Key select item1.Nombre).FirstOrDefault();
                            diagnosticoConsultas += item.Value + "-" + nombreDiagnostico + ",";
                        }

                    }



                    cell = new PdfPCell(new Phrase("Diagnósticos", texto.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var validacionDiagnosticosConsultas = (diagnosticoConsultas != "") ? diagnosticoConsultas : "No se ingresó diagnosticos";
                    cell = new PdfPCell(new Paragraph(validacionDiagnosticosConsultas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    foreach (var item in categorizacionHC)
                    {
                        if (item.id_IngresoClinica == ingrClinica.idIngresoClinica)
                        {
                            listaCategorizacion.Add(item);
                        }
                    }

                    foreach (var item in categorizacionesNombre)
                    {
                        foreach (var item1 in listaCategorizacion)
                        {
                            if (item.id_CategorizacionCAP == item1.id_Categorizacion)
                            {
                                if (!(diccionarioCategorizacionCAP.ContainsKey(item.nombre)))
                                {
                                    diccionarioCategorizacionCAP.Add(item.nombre, item.nombre);
                                    //break;
                                }
                               
                            }
                        }
                    }


                    if (diccionarioCategorizacionCAP != null)
                    {
                        foreach (var item in diccionarioCategorizacionCAP)
                        {
                            categorizacionesHC += item.Value + ",";
                        }

                    }

                    var validacionCategorizacionConsultas = (categorizacionesHC != "") ? categorizacionesHC : "No se ingresó categorizaciones.";
                    cell = new PdfPCell(new Phrase("Categorización CAP", texto.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(validacionCategorizacionConsultas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Datos personales", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Nombre: " + paciente.nombre + " " + paciente.apellido);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Sexo: " + sexo);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Fecha de nacimiento: " + fechaNacimiento);
                    table.AddCell(texto);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Número de documento: " + ingrClinica.numeroDocumento);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Tipo de documento: " + ingrClinica.id_tipoDocumento);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Lugar de nacimiento: país:" + nombrePais + "- ciudad: " + ciudad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Edad: " + ingrClinica.edad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Estado civil: " + ingrClinica.estadoCivil);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Religion: " + ingrClinica.religion);
                    table.AddCell(texto);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Escolaridad: " + nivelEscolaridad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Profesión: " + ingrClinica.profesion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Ocupación: " + ocupacion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Dirección: " + ingrClinica.direccion);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Barrio: " + barrio);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Localidad: " + localidad);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Estrato: " + ingrClinica.id_estrato);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Teléfono: " + ingrClinica.telefono);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Email: " + ingrClinica.email);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("EPS: " + ingrClinica.tieneEps);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("EPC: " + ingrClinica.tieneEpc);
                    table.AddCell(texto);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Institución: " + eps);
                    table.AddCell(texto);

                    cell = new PdfPCell(new Phrase("Datos de remitido", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var entidadRemitenteNombre = (entidadRemitente != null) ? entidadRemitente.nombreEntidad : "No tiene";
                    cell = new PdfPCell(new Phrase("Institución que remite: " + entidadRemitenteNombre, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    var entidadRemitenteNombreRemitente = (entidadRemitente != null) ? entidadRemitente.nombreRemitente : "No tiene";
                    cell = new PdfPCell(new Phrase("Profesional que remite: " + entidadRemitenteNombreRemitente, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fecha de remisión: " + fechaRemisionRemitente, texto.Font));
                    cell.Colspan = 3;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Datos consultante", title.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultanteNombre = (consultante != null) ? consultante.nombre : "No se ingreso nombre";
                    var consultanteApellido = (consultante != null) ? consultante.apellido : " ni apellido";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Nombre: " + consultanteNombre + " " + consultanteApellido);
                    table.AddCell(texto);

                    //var sexoConsultanteValidacion = (validarSexoConsultante != "") ? sexoConsultante : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Sexo: " + validarSexoConsultante);
                    table.AddCell(texto);

                    var consultanteParentezco = (consultante != null) ? consultante.parentezco : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Parentesco: " + consultanteParentezco);
                    table.AddCell(texto);

                    var consultanteTelefono = (consultante != null) ? consultante.telefono : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Teléfono: " + consultanteTelefono);
                    table.AddCell(texto);

                    var consultanteTipoDocumento = (consultante != null) ? consultante.id_tipoDocumento : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Tipo de documento: " + consultanteTipoDocumento);
                    table.AddCell(texto);

                    var consultanteCedula = (consultante != null) ? consultante.cedula : "No tiene";
                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Número de documento: " + consultanteCedula);
                    table.AddCell(texto);

                    cell = new PdfPCell(new Phrase("Motivo Consulta", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.motivoConsulta, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Problemática", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.problematicaActual, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Historia personal", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.historiaPersonal, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Antecedentes", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.antecedentes, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Estrategias evaluación", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Pruebas psicométricas");
                    table.AddCell(texto);

                    var estrategiasIngPruebasPsico = (estrategiasIngreso != null) ? estrategiasIngreso.pruebasPsico : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngPruebasPsico, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Cuestionarios");
                    table.AddCell(texto);

                    var estrategiasIngCuestionarios = (estrategiasIngreso != null) ? estrategiasIngreso.cuestionarios : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngCuestionarios, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Pruebas proyectivas");
                    table.AddCell(texto);

                    var estrategiasIngPruebasProyectivas = (estrategiasIngreso != null) ? estrategiasIngreso.pruebasProyectivas : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngPruebasProyectivas, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);


                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Exámen mental");
                    table.AddCell(texto);

                    var estrategiasIngExamenMental = (estrategiasIngreso != null) ? estrategiasIngreso.examenMental : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngExamenMental, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    texto = new Paragraph();
                    texto.Font = FontFactory.GetFont("Arial", 9);
                    texto.Add("Entrevistas");
                    table.AddCell(texto);

                    var estrategiasIngEntrevistas = (estrategiasIngreso != null) ? estrategiasIngreso.entrevistas : " ";
                    cell = new PdfPCell(new Phrase(estrategiasIngEntrevistas, informacionParrafo.Font));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    var consultasResultadoEv = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().resultadoAutoevaluacion : " ";
                    cell = new PdfPCell(new Phrase("Resultados evaluación", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(consultasResultadoEv, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Historia familiar", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Paragraph(ingrClinica.historiaFamiliar, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Hipótesis psicológica", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasHipotesisPsicologica = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().hipotesisPsicologica : " ";
                    cell = new PdfPCell(new Paragraph(consultasHipotesisPsicologica, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Objetivos terapéuticos", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasObjetivosTerapeuticos = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().objetivosTerapeuticos : " ";
                    cell = new PdfPCell(new Paragraph(consultasObjetivosTerapeuticos, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Estrategias y técnicas terapéuticas", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasEstrategiasTecnicasTerapeuticas = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().estrategiasTecnicasTerapeuticas : " ";
                    cell = new PdfPCell(new Paragraph(consultasEstrategiasTecnicasTerapeuticas, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Logros alcanzados según consultante", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasLogrosAlcSegConsultante = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().logrosAlcanzadosSegunConsultante : " ";
                    cell = new PdfPCell(new Paragraph(consultasLogrosAlcSegConsultante, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Resúmen", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasResumen = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().resumen : " ";
                    cell = new PdfPCell(new Paragraph(consultasResumen, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Observaciones y recomendaciones", subtitulos.Font));
                    cell.Colspan = 3;
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    var consultasObservacionesRecomendaciones = (listaConsulta.Count != 0) ? listaConsulta.FirstOrDefault().observacionesRecomendaciones : " ";
                    cell = new PdfPCell(new Paragraph(consultasObservacionesRecomendaciones, informacionParrafo.Font));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    document.Add(table);
                    ingr++;
                }
            }
            //--------------------------Creación de las consultas
            //for (int i = 0; i <= numeroConsultas; i++) {
            PdfPCell cell1 = new PdfPCell();
            int i = 0;
            if (gifs.Contains("2"))
            {
                if (!(gifs.Contains("1") || gifs.Contains("4")))
                {
                    foreach (var item in consultas)
                    {
                        foreach (var ingrClinica in ingresoClinica)
                        {
                            if (item.id_ingresoClinica == ingrClinica.idIngresoClinica)
                            {
                                //listaConsulta.Add(item);
                                listaConsultaGeneralPorPaciente.Add(item);
                            }
                        }
                    }
                }

                foreach (var item in listaConsultaGeneralPorPaciente)
                {
                    string diagnosticosConsulta = "";
                    var nombreDiag = "";
                    var nombreUsuarioConsulta = (from item1 in HC.listarUsuario() where item1.Id == item.id_User select item1.nombreUsuario).FirstOrDefault();
                    var nombreUsrConsulta = nombreUsuarioConsulta != null ? nombreUsuarioConsulta : "";

                    if ((gifs.Contains("1") || gifs.Contains("4")))
                    {
                        document.NewPage();
                    }
                    else if (i > 0)
                    {
                        document.NewPage();
                    }

                    Paragraph textoConsulta;
                    PdfPTable tableConsultas = new PdfPTable(1);
                    cell1 = new PdfPCell(new Phrase("CONSULTA", title.Font));
                    //cell.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Sesión número" + " " + item.numeroSesion, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Usuario quien antendió la consulta" + ": " + nombreUsrConsulta, subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableConsultas.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Diagnósticos", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var consultaDiagnostico = (from item1 in HC.listarConsultaDiagnosticos() where item1.id_consulta == item.idConsulta select item1.id_diagnostico).ToList();
                    if (consultaDiagnostico != null)
                    {
                        foreach (var diagnostico in consultaDiagnostico)
                        {
                            nombreDiag = (from diagnos in diagBo.listarDiagnostico() where diagnos.Codigo == diagnostico select diagnos.Nombre).FirstOrDefault();
                            diagnosticosConsulta += diagnostico + "-" + nombreDiag + "*";
                        }
                    }

                    var diagnosticosSesion = (diagnosticosConsulta != "") ? diagnosticosConsulta : "No se ingresaron diagnósticos.";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(diagnosticosSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Objetivos de la sesión", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var objetivoConsultaSesion = (item.objetivoSesion != null) ? item.objetivoSesion : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(objetivoConsultaSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Ejercicios y eventos significativos", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var ejerciciosEventoSesion = (item.ejerciciosEvento != null) ? item.ejerciciosEvento : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(ejerciciosEventoSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Desarrollo y temas tratados", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var desarrolloTemasTratadosSesion = (item.desarrolloTemasTratados != null) ? item.desarrolloTemasTratados : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(desarrolloTemasTratadosSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Próxima sesión", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var tareasProximaSesionSesion = (item.tareasProximaSesion != null) ? item.tareasProximaSesion : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(tareasProximaSesionSesion);
                    tableConsultas.AddCell(textoConsulta);

                    cell1 = new PdfPCell(new Phrase("Observaciones", subtitulos.Font));
                    cell1.Colspan = 3;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableConsultas.AddCell(cell1);

                    var observacionesRecomendacionesSesion = (item.observacionesRecomendaciones != null) ? item.observacionesRecomendaciones : " ";
                    textoConsulta = new Paragraph();
                    textoConsulta.Font = FontFactory.GetFont("Arial", 9);
                    textoConsulta.Add(observacionesRecomendacionesSesion);
                    tableConsultas.AddCell(textoConsulta);

                    document.Add(tableConsultas);
                    i += 1;
                }
            }
            //}
            //--------------------------Creación de las inasistencias
            int j = 0;
            if (gifs.Contains("3"))
            {
                if ((gifs.Contains("1") || gifs.Contains("2") || gifs.Contains("4")))
                {
                    document.NewPage();
                }
                if (j > 0)
                {
                    document.NewPage();
                }
                //document.NewPage();
                foreach (var item in listaInasistencias)
                {
                    var nombreUsuarioInasistencia = (from item1 in HC.listarUsuario() where item1.Id == item.usuario select item1.nombreUsuario).FirstOrDefault();
                    var nombreUsrInasistencia = nombreUsuarioInasistencia != null ? nombreUsuarioInasistencia : "";

                    var fechInasistencia = (item.fechaInasistencia).ToString();
                    var fechnStInasistencia = DateTime.Parse(fechInasistencia);
                    string formatInasistencia = "yyyy-MM-dd";
                    var fechaInasistencia = fechnStInasistencia.ToString(formatInasistencia);

                    //document.NewPage();
                    Paragraph textoInasistencia = new Paragraph();
                    PdfPTable tableInasistencias = new PdfPTable(2);
                    cell1 = new PdfPCell(new Phrase("INASISTENCIA", title.Font));
                    cell1.Colspan = 2;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Fecha de la inasistencia" + " " + fechaInasistencia, subtitulos.Font));
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Phrase("Usuario quién generó la inasistencia" + ": " + nombreUsrInasistencia, subtitulos.Font));
                    cell1.Colspan = 2;
                    cell1.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                    tableInasistencias.AddCell(cell1);

                    cell1 = new PdfPCell(new Paragraph(item.motivo, subtitulos.Font));
                    cell1.Colspan = 3;
                    tableInasistencias.AddCell(cell1);
                    document.Add(tableInasistencias);
                    j += 1;
                }
            }
            //--------------------------Creación de las remisiones 
            int k = 0;
            if (gifs.Contains("5"))
            {
                foreach (var ingrClinica in ingresoClinica)
                {
                    diccionarioRemisiones = new Dictionary<string, string>();
                    if ((gifs.Contains("1") || gifs.Contains("4") || gifs.Contains("2") || gifs.Contains("3")))
                    {
                        document.NewPage();
                    }
                    if (k > 0)
                    {
                        document.NewPage();
                    }
                    var remisiones = (from item in HC.listarRemisiones() where item.id_ingresoClinica == ingrClinica.idIngresoClinica select item).ToList();
                    if (remisiones.Count > 0)
                    {
                        var fechaRemitido = "";
                        PdfPCell cell2 = new PdfPCell();
                        var concatRemisiones = "";
                        if (remisiones.Count > 0)
                        {
                            var fechRemitido = (remisiones.FirstOrDefault().fechaRemitido).ToString();
                            var fechNRemitido = DateTime.Parse(fechRemitido);
                            string formatRemitido = "yyyy-MM-dd";
                            fechaRemitido = fechNRemitido.ToString(formatRemitido);
                        }
                        else
                        {
                            fechaRemitido = "No tiene fecha";
                        }

                        Paragraph textoRemision = new Paragraph();
                        PdfPTable tableRemision = new PdfPTable(2);
                        cell2 = new PdfPCell(new Phrase("REMISIONES", title.Font));
                        cell2.Colspan = 2;
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Fecha de la remisión" + " " + fechaRemitido, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Factores de riesgo", title.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        cell2.Colspan = 3;
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        foreach (var item in remisiones)
                        {
                            foreach (var item1 in HC.listarMotivosRemisiones())
                            {
                                if (item1.idMotivoRemision == item.motivoRemision)
                                {
                                    if (!(diccionarioRemisiones.ContainsKey(item1.nombre)))
                                    {
                                        diccionarioRemisiones.Add(item1.nombre, item1.nombre);
                                    }
                                }
                            }
                        }

                        foreach (var item in diccionarioRemisiones)
                        {
                            concatRemisiones += item.Value + ",";
                        }

                        cell2 = new PdfPCell(new Paragraph(concatRemisiones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var institucionRemitida = remisiones.Count > 0 ? remisiones.FirstOrDefault().nombreInsitucionRemitida : "";
                        cell2 = new PdfPCell(new Paragraph("Institución remitido: " + institucionRemitida, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var servicioRemitido = remisiones.Count > 0 ? remisiones.FirstOrDefault().servicioRemitido : "";
                        cell2 = new PdfPCell(new Paragraph("Servicio remitido: " + servicioRemitido, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var evolucionPaciente = remisiones.Count > 0 ? remisiones.FirstOrDefault().evolucionPaciente : "";
                        cell2 = new PdfPCell(new Paragraph("Evolución del paciente: " + evolucionPaciente, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var aspectosPositivos = remisiones.Count > 0 ? remisiones.FirstOrDefault().aspectosPositivos : "";
                        cell2 = new PdfPCell(new Paragraph("Aspectos positivos: " + aspectosPositivos, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var recomendaciones = remisiones.Count > 0 ? remisiones.FirstOrDefault().recomendaciones : "";
                        cell2 = new PdfPCell(new Paragraph("Recomendaciones: " + recomendaciones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        document.Add(tableRemision);
                        k += 1;
                    }
                }
            }

            //--------------------------Creación de los cierres
            int l = 0;
            if (gifs.Contains("6"))
            {
                foreach (var ingrClinica in ingresoClinica)
                {
                    diccionarioCierres = new Dictionary<string, string>();
                    if ((gifs.Contains("1") || gifs.Contains("4") || gifs.Contains("2") || gifs.Contains("3") || gifs.Contains("5")))
                    {
                        document.NewPage();
                    }
                    if (l > 0)
                    {
                        document.NewPage();
                    }
                    var cierres = (from item in HC.listarCierres() where item.id_ingresoClinica == ingrClinica.idIngresoClinica && ingrClinica.estadoRemision == false select item).ToList();
                    if (cierres.Count > 0)
                    {
                        List<MotivoCierreHistoriaClinica> motivosCierres = new List<MotivoCierreHistoriaClinica>();
                        var fechaIniPsico = "";
                        var fechaFinaliPsico = "";
                        PdfPCell cell2 = new PdfPCell();
                        var concatCierres = "";
                        if (cierres.Count > 0)
                        {
                            var fechIniPsico = (cierres.FirstOrDefault().fechaInicioPsicoterapia).ToString();
                            var fechNIniPsico = DateTime.Parse(fechIniPsico);
                            string formatIniPsico = "yyyy-MM-dd";
                            fechaIniPsico = fechNIniPsico.ToString(formatIniPsico);

                            var fechFinaliPsico = (cierres.FirstOrDefault().fechaFinalizaionPsicoterapia).ToString();
                            var fechNFinaliPsico = DateTime.Parse(fechFinaliPsico);
                            string formatFinaliPsico = "yyyy-MM-dd";
                            fechaFinaliPsico = fechNFinaliPsico.ToString(formatFinaliPsico);

                        }
                        else
                        {
                            fechaIniPsico = "No tiene fecha";
                            fechaFinaliPsico = "No tiene fecha";
                        }

                        Paragraph textoRemision = new Paragraph();
                        PdfPTable tableRemision = new PdfPTable(2);
                        cell2 = new PdfPCell(new Phrase("INFORME Y CIERRE DE HISTORIA CLÍNICA", title.Font));
                        cell2.Colspan = 2;
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Número de historia clínica" + " " + paciente.numeroHistoriaClinica, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);

                        cell2 = new PdfPCell(new Phrase("Fecha del cierre" + " " + fechaFinaliPsico, subtitulos.Font));
                        cell2.BackgroundColor = new iTextSharp.text.BaseColor(155, 194, 230);
                        tableRemision.AddCell(cell2);



                        foreach (var item in HC.listarMotivoCierreHistoriaClinicaBO())
                        {
                            foreach (var item1 in cierres)
                            {
                                if (item.id_Cierre == item1.idCierreHC)
                                {
                                    motivosCierres.Add(item);

                                }
                            }
                        }


                        foreach (var item in HC.listarMotivosCierre())
                        {
                            foreach (var item1 in motivosCierres)
                            {
                                if (item1.id_MotivoCierre == item.idMotivoCierre)
                                {
                                    if (!(diccionarioCierres.ContainsKey(item.Nombre)))
                                    {
                                        diccionarioCierres.Add(item.Nombre, item.Nombre);
                                        //break;
                                    }

                                }
                            }
                        }


                        if (diccionarioCierres != null)
                        {
                            foreach (var item in diccionarioCierres)
                            {
                                concatCierres += item.Value + ",";
                            }

                        }

                        cell2 = new PdfPCell(new Paragraph("Motivos de cierre: " + concatCierres, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var fechaInicio = cierres.Count > 0 ? cierres.FirstOrDefault().fechaInicioPsicoterapia.ToString() : "";
                        cell2 = new PdfPCell(new Paragraph("Fecha de inicio de la psicoterápia: " + fechaIniPsico, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var fechaFin = cierres.Count > 0 ? cierres.FirstOrDefault().fechaFinalizaionPsicoterapia.ToString() : "";
                        cell2 = new PdfPCell(new Paragraph("Fecha de finalización de la psicoterápia: " + fechaFinaliPsico, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var numeroCitas = cierres.FirstOrDefault().numeroCitasAsignadas != null ? cierres.FirstOrDefault().numeroCitasAsignadas : "No tuvo citas asignadas.";
                        cell2 = new PdfPCell(new Paragraph("Número de citas asignadas: " + numeroCitas, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var numeroSesiones = cierres.FirstOrDefault().numeroSesionesRealizadas != null ? cierres.FirstOrDefault().numeroSesionesRealizadas : "No tuvo sesiones realizadas.";
                        cell2 = new PdfPCell(new Paragraph("Número de sesiones realizadas: " + numeroSesiones, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var especificacionMotivoCierreHC = cierres.Count > 0 ? cierres.FirstOrDefault().especificacionMotivoCierre : "";
                        cell2 = new PdfPCell(new Paragraph("Especificación motivo del cierre: " + especificacionMotivoCierreHC, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var instrumentoEva = cierres.Count > 0 ? cierres.FirstOrDefault().instrumentosEvaluacion : "";
                        cell2 = new PdfPCell(new Paragraph("Instrumentos evaluación: " + instrumentoEva, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        var resultadosObtenidos = cierres.Count > 0 ? cierres.FirstOrDefault().resultadoObtenidoEvaluacion : "";
                        cell2 = new PdfPCell(new Paragraph("Resultados obtenidos: " + resultadosObtenidos, subtitulos.Font));
                        cell2.Colspan = 3;
                        tableRemision.AddCell(cell2);

                        document.Add(tableRemision);
                        l += 1;
                    }
                }
            }

            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", "HistoriaClínicaNúmero" + paciente.numeroHistoriaClinica + ".pdf");
        }

    }
}