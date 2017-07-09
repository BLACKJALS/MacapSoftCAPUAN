using MacapSoftCAPUAN.BO;
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


        

        public ActionResult IngresoPacientes() {
            HC = new HistoriaClinicaBO();
            List<SelectListItem> listaItemsBarrios = new List<SelectListItem>();
            List<SelectListItem> listaItemsDocumento = new List<SelectListItem>();
            List<SelectListItem> listaItemsLocalidades = new List<SelectListItem>();
            List<SelectListItem> listaItemsEps = new List<SelectListItem>();
            List<SelectListItem> listaItemsValidation = new List<SelectListItem>();
            SelectListItem items;
            SelectListItem documento;
            SelectListItem validacion;
            var listaBarrio = HC.listarBarrios();
            foreach (var item in listaBarrio)
            {
                items = new SelectListItem();
                items.Text = item.nombre;
                items.Value = item.idBarrio;
                listaItemsBarrios.Add(items);
            }

            documento = new SelectListItem();
            documento.Text = "Seleccione";
            documento.Value = "";
            listaItemsDocumento.Add(documento);
            documento = new SelectListItem();
            documento.Text = "CC";
            documento.Value = "CC";
            listaItemsDocumento.Add(documento);
            documento = new SelectListItem();
            documento.Text = "TI";
            documento.Value = "TI";
            listaItemsDocumento.Add(documento);
            documento = new SelectListItem();
            documento.Text = "RC";
            documento.Value = "RC";
            listaItemsDocumento.Add(documento);
            documento = new SelectListItem();
            documento.Text = "CE";
            documento.Value = "CE";
            listaItemsDocumento.Add(documento);

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
            ViewBag.ItemLocalidades = listaItemsLocalidades.ToList();
            ViewBag.ItemBarrios = listaItemsBarrios.ToList();
            ViewBag.ItemDocumento = listaItemsDocumento.ToList();
            ViewBag.ItemEps = listaItemsEps.ToList();
            ViewBag.ItemValidacion = listaItemsValidation.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult IngresoPacientesCreado(RecepcionCaso model)
        {
            HC = new HistoriaClinicaBO();
            Paciente paciente = new Paciente();
            consultantePaciente consultantePa = new consultantePaciente();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Remitido remitido = new Remitido();
            Remision remision = new Remision();
            remision = model.remision;
            HC.agregarRemision(remision);
            remitido = model.remitido;
            HC.agregarRemitido(remitido);
            paciente = model.paciente;
            //AccountController acount = new AccountController();
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //paciente.idUser = user.UserName;
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            paciente.idUser = userId;
            //var user = acount.retornarUsuario();
            var a = model.paciente.numeroDocumento;
            //paciente.remitido.idRemitente = remitido.idRemitente;
            //paciente = model.paciente;
            HC.agregarpaciente(paciente);
            if (model.consultante.cedula != null) {
                //consultantePa.idConsultante.cedula = model.consultante.cedula;
            }
            if (model.paciente.numeroDocumento != null)
            {
                //consultantePa.idPaciente.numeroDocumento = model.paciente.numeroDocumento;
            }
            ingresoClinica = model.ingresoClinica;
            remision = model.remision;
            remitido = model.remitido;
            try
            {
                return View("IngresoPacienteCreado");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult listaHistoriasClinicas() {
            return View();
        }

        public ActionResult ingresoHistoriaClinica() {

            return View();
        }
    }
}