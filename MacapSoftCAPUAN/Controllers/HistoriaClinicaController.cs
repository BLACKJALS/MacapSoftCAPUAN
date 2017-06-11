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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RecepcionCaso(string bussinesUnit)
        {
            return View();
        }
    }
}