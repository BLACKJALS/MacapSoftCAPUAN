using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using MacapSoftCAPUAN.ModelsVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestHistoriaClinica
    {
        public HistoriaClinicaDALC hcDalc { get; set; }

        [TestMethod]
        public void UnitTestCrearRecepcionCaso()
        {
            RecepcionCaso recepcionCaso = new RecepcionCaso();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Paciente paciente = new Paciente();
            Remision remision = new Remision();
            Remitido remitido = new Remitido();
            CierreHC cierre = new CierreHC();
            Consecutivo consecutivo = new Consecutivo();
            Consultante consultante = new Consultante();

            //ingresoClinica.

        }

    }
}
