using MacapSoftCAPUAN.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MacapSoftCAPUAN.Controllers
{
    [Authorize]
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class DiagnosticoController : Controller
    {
        private DiagnosticosDALC diag;
        private Mapeo map { get; set; }
        // GET: Diagnostico
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarDiagnosticos()
        {
            return View();
        }


        public JsonResult ListarDiagnosticosCAP()
        {
            diag = new DiagnosticosDALC();
            var lista = diag.ListarDiagnosticos();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult CrearDiagnostico()
        {
            diagnosticosPrincipales();
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult CrearDiagnosticoCAP(string Codigo, string Nombre, string Destacado)
        {
            //Maper map = new Maper();
            try
            {
                diag = new DiagnosticosDALC();
                diag.CrearDiagnostico(Codigo, Nombre, Destacado);
                return View();
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                return View("ErrorCrearDiagnosticoView");
                throw;
            }
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult ModificarDiagnostico()//string Codigo)
        {

            diagnosticosPrincipales();
            return View();
            //diag = new DiagnosticosDALC();
            //map = new Mapeo();
            //var diagnostico = diag.mostrarDiagnostico(Codigo);
            //var mapeoDiag = map.MapeoDiagnostico(diagnostico);
            //diagnosticosPrincipales();
            //return View(mapeoDiag);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult ModificarDiagnosticoCAP(string idCodigo, string nombreEditado, string casoEditado)
        {
            diag = new DiagnosticosDALC();
            try
            {
                diag.ModificarDiagnostico(idCodigo, nombreEditado, casoEditado);
                return RedirectToAction("Index", "HistoriaClinica");
            }
            catch (Exception e)
            {
                return View("ErrorModificarDiagnostico");
                throw;
            }

        }



        public void diagnosticosPrincipales()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            SelectListItem Cabecera = new SelectListItem()
            {
                Text = "--Seleccione--",
                Value = "Seleccione"
            };

            SelectListItem diagnosticoImportante = new SelectListItem()
            {
                Text = "Si",
                Value = "Si"
            };

            SelectListItem diagnosticoNoImportante = new SelectListItem()
            {
                Text = "No",
                Value = "No"
            };

            lista.Add(Cabecera);
            lista.Add(diagnosticoImportante);
            lista.Add(diagnosticoNoImportante);

            ViewBag.ItemTypes = lista.ToList();
        }

    }
}