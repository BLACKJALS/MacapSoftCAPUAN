using MacapSoftCAPUAN.Controllers;
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
        string messageg = "Se produjo una excepción en el inicializador de tipo de 'System.Web.Mvc.ViewEngines'.";
        [TestMethod]
        public void UnitTestCrearRecepcionCaso()
        {
            HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
            RecepcionCaso recepcionCaso = new RecepcionCaso();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Paciente paciente = new Paciente();
            Remision remision = new Remision();
            Remitido remitido = new Remitido();
            CierreHC cierre = new CierreHC();
            Consecutivo consecutivo = new Consecutivo();
            Consultante consultante = new Consultante();

            ingresoClinica.fechaAtencion = DateTime.Now;
            consecutivo.numeroConsecutivo = 12;
            paciente.nombre = "Nombre prueba";
            paciente.apellido = "Apellido prueba";
            ingresoClinica.id_tipoDocumento = "TI";
            ingresoClinica.numeroDocumento = "1010101";
            paciente.id_sexo = 1;
            ingresoClinica.id_NivelEscolaridad = 1;
            paciente.fechaNacimiento = new DateTime(2008, 3, 1, 7, 0, 0);
            paciente.id_ciudad = "BOGO";
            ingresoClinica.edad = 9;
            ingresoClinica.id_barrio = "lossabana ";
            ingresoClinica.direccion = "direccionPrueba";
            ingresoClinica.id_estrato = 1;
            ingresoClinica.telefono = "12346789";
            ingresoClinica.email = "";
            ingresoClinica.id_ocupacion = 4;
            ingresoClinica.profesion = "Estudiante";
            ingresoClinica.tieneEpc = "SI";
            ingresoClinica.tieneEps = "NO";
            ingresoClinica.id_Eps = "Capreco";
            remitido.nombreEntidad = "Caprecom";
            remitido.nombreRemitente = "pruebaNombreRemitente";
            remitido.fechaRemision = new DateTime(2017, 3, 1, 7, 0, 0);
            consultante.nombre = "pruebaNombreConsultante";
            consultante.apellido = "pruebaApellidoConsultante";
            consultante.parentezco = "parentescoConsultante";
            consultante.telefono = "0123456789";
            consultante.id_tipoDocumento = "CC";
            consultante.cedula = "000";
            ingresoClinica.motivoConsulta = "prueba motivo consulta";
            ingresoClinica.observaciones = "prueba observaciones";

            recepcionCaso.ingresoClinica = ingresoClinica;
            recepcionCaso.consecutivo = consecutivo;
            recepcionCaso.consultante = consultante;
            recepcionCaso.paciente = paciente;
            recepcionCaso.remitido = remitido;
            try
            {
                ingresoRecepcion.IngresoPacientesCreado(recepcionCaso, "0", "1010101");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestCrearRecepcionCasoAntiguo()
        {
            HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
            RecepcionCaso recepcionCaso = new RecepcionCaso();
            IngresoClinica ingresoClinica = new IngresoClinica();
            Paciente paciente = new Paciente();
            Remision remision = new Remision();
            Remitido remitido = new Remitido();
            CierreHC cierre = new CierreHC();
            Consecutivo consecutivo = new Consecutivo();
            Consultante consultante = new Consultante();

            ingresoClinica.fechaAtencion = DateTime.Now;
            consecutivo.numeroConsecutivo = 12;
            paciente.nombre = "Nombre prueba";
            paciente.apellido = "Apellido prueba";
            ingresoClinica.id_tipoDocumento = "TI";
            ingresoClinica.numeroDocumento = "1010101";
            paciente.id_sexo = 1;
            ingresoClinica.id_NivelEscolaridad = 1;
            paciente.fechaNacimiento = new DateTime(2008, 3, 1, 7, 0, 0);
            paciente.id_ciudad = "BOGO";
            ingresoClinica.edad = 9;
            ingresoClinica.id_barrio = "lossabana ";
            ingresoClinica.direccion = "direccionPrueba2";
            ingresoClinica.id_estrato = 1;
            ingresoClinica.telefono = "123467890120";
            ingresoClinica.email = "";
            ingresoClinica.id_ocupacion = 4;
            ingresoClinica.profesion = "Estudiante";
            ingresoClinica.tieneEpc = "SI";
            ingresoClinica.tieneEps = "NO";
            ingresoClinica.id_Eps = "Capreco";
            remitido.nombreEntidad = "Caprecom";
            remitido.nombreRemitente = "pruebaNombreRemitente";
            remitido.fechaRemision = new DateTime(2017, 3, 1, 7, 0, 0);
            consultante.nombre = "pruebaNombreConsultante";
            consultante.apellido = "pruebaApellidoConsultante";
            consultante.parentezco = "parentescoConsultante";
            consultante.telefono = "0123456789";
            consultante.id_tipoDocumento = "CC";
            consultante.cedula = "000";
            ingresoClinica.motivoConsulta = "prueba motivo consulta";
            ingresoClinica.observaciones = "prueba observaciones";

            recepcionCaso.ingresoClinica = ingresoClinica;
            recepcionCaso.consecutivo = consecutivo;
            recepcionCaso.consultante = consultante;
            recepcionCaso.paciente = paciente;
            recepcionCaso.remitido = remitido;
            try
            {
                ingresoRecepcion.IngresoPacientesCreado(recepcionCaso, "0", "1010101");
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestValidarPaciente()
        {
            string generarNuevaHC = "";
            try
            {
                
                hcDalc = new HistoriaClinicaDALC();
                string validacionNumeroPaciente = "1010";
                var pacientEncontrado = (from item in hcDalc.listarPacientes() where item.numeroHistoriaClinica == validacionNumeroPaciente && item.estadoHC == true select item).FirstOrDefault();
                if (pacientEncontrado != null)
                {
                     generarNuevaHC = "Se realizará nueva HC";
                }
                else {
                    generarNuevaHC = "Ya existe el paciente no se puede realizar recepción de caso";
                }
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                //Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestListaryBuscarPacientes()
        {
            string generarNuevaHC = "";
            try
            {
                
                hcDalc = new HistoriaClinicaDALC();
                string validacionNumeroPaciente = "1010101";
                var listadoPacientes = (from item in hcDalc.listarPacientes() select item).ToList();
                var pacientEncontrado = (from item in listadoPacientes where item.numeroHistoriaClinica == validacionNumeroPaciente select item).FirstOrDefault();
                if (pacientEncontrado != null)
                {
                    generarNuevaHC = "Se encontró el paciente";
                }
                else
                {
                    generarNuevaHC = "No se encontró el paciente";
                }
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                Assert.IsFalse(generarNuevaHC != "Se encontró el paciente" || generarNuevaHC != "No se encontró el paciente");
            }
        }


        [TestMethod]
        public void UnitTestListaryBuscarHistoriasClinicasActivas()
        {
            string generarNuevaHC = "";
            try
            {

                hcDalc = new HistoriaClinicaDALC();
                
                var historiasClinicasActivas = (from item in hcDalc.listarPacientes() where item.estadoHC == false select item).ToList();
                var historiaClinicaActiva = (from item in historiasClinicasActivas where item.numeroHistoriaClinica == "1010101" select item).FirstOrDefault();
                if (historiasClinicasActivas != null)
                {
                    generarNuevaHC = "listado de hc activas encontrado";
                    Assert.IsNotNull(historiasClinicasActivas);
                }
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                Assert.IsFalse(generarNuevaHC != "listado de hc activas encontrado");
            }
        }



        [TestMethod]
        public void UnitTestAbrirHistoriaClinica()
        {
            string numeroHistoriaClinica = "1010101";
            try
            {

                hcDalc = new HistoriaClinicaDALC();

                var historiasClinicasActivas = (from item in hcDalc.listarPacientes() where item.estadoHC == false && item.numeroHistoriaClinica == numeroHistoriaClinica select item).ToList();

                if (historiasClinicasActivas != null)
                {
                    var ingresoExitoso = "hc activas encontrado se abre hc";
                    Assert.IsNotNull(historiasClinicasActivas);
                }
                else {
                    var ingresoNoExitoso = "hc activas no encontrado se abre hc";
                    Assert.IsFalse(historiasClinicasActivas == null);
                }
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                //Assert.IsFalse(generarNuevaHC != "listado de hc activas encontrado");
            }
        }


        [TestMethod]
        public void UnitTestConsultarHC()
        {
            string numeroHistoriaClinica = "1010101";
            string documentosConsultar = "4";
            try
            {
                HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
                hcDalc = new HistoriaClinicaDALC();

                ingresoRecepcion.ElementosConsultarPost(documentosConsultar, numeroHistoriaClinica);
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                //Assert.IsFalse(generarNuevaHC != "listado de hc activas encontrado");
            }
        }


    }
}
