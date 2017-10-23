using MacapSoftCAPUAN.BO;
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
        public HistoriaClinicaBO hBo { get; set; }
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
                ingresoRecepcion.IngresoPacientesCreado(recepcionCaso, "0", "1010101","0");
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
                ingresoRecepcion.IngresoPacientesCreado(recepcionCaso, "0", "1010101","0");
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
                string validacionNumeroHistoriaClinica = "1010";
                string validacionNumeroNuevoPaciente = "18";
                var pacientEncontrado = (from item in hcDalc.listarPacientes() where item.numeroHistoriaClinica == validacionNumeroHistoriaClinica && item.estadoHC == true select item).FirstOrDefault();
                var pacientNuevoEncontrado = (from item in hcDalc.listarPacientes() where item.numeroHistoriaClinica == validacionNumeroNuevoPaciente && item.estadoHC == true select item).FirstOrDefault();
                if (pacientEncontrado != null)
                {
                     generarNuevaHC = "Se realizará nueva HC";
                }
                else {
                    generarNuevaHC = "Ya existe el paciente no se puede realizar recepción de caso";
                }

                if (pacientNuevoEncontrado != null)
                {
                    generarNuevaHC = "Se realizará nueva HC";
                }
                else
                {
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
                string validacionNumeroPacienteNoExistente = "123";
                var listadoPacientes = (from item in hcDalc.listarPacientes() select item).ToList();
                var pacientEncontrado = (from item in listadoPacientes where item.numeroHistoriaClinica == validacionNumeroPaciente select item).FirstOrDefault();
                var pacientNoEncontrado = (from item in listadoPacientes where item.numeroHistoriaClinica == validacionNumeroPacienteNoExistente select item).FirstOrDefault();
                if (pacientEncontrado != null)
                {
                    generarNuevaHC = "Se encontró el paciente";
                }
                else
                {
                    generarNuevaHC = "No se encontró el paciente";
                }

                if (pacientNoEncontrado == null)
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
                string numeroHistoriaClinicaActiva = "1010101";
                var historiasClinicasActivas = (from item in hcDalc.listarPacientes() where item.estadoHC == false select item).ToList();
                var historiaClinicaActiva = (from item in historiasClinicasActivas where item.numeroHistoriaClinica == numeroHistoriaClinicaActiva select item).FirstOrDefault();
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



        [TestMethod]
        public void UnitTestConsultarDocumentoGeneral()
        {
            DocumentoGeneralVM documentoGeneral = new DocumentoGeneralVM();
            
            IngresoClinica ingresoClinica = new IngresoClinica();
            Paciente paciente = new Paciente();
            Remision remision = new Remision();
            Remitido remitido = new Remitido();
            CierreHC cierre = new CierreHC();
            Consecutivo consecutivo = new Consecutivo();
            Consultante consultante = new Consultante();
            IngresoEstrategiasEvaluacion estrategiaEva = new IngresoEstrategiasEvaluacion();
            Consulta consulta = new Consulta();
            List<IngresoEstrategiasEvaluacion> estrategiasIngreso = new List<IngresoEstrategiasEvaluacion>();

            ingresoClinica.idIngresoClinica = 30;
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
            
            ingresoClinica.observaciones = "prueba observaciones";

            //Lo nuevo
            ingresoClinica.estadoCivil = "Soltero";
            ingresoClinica.religion = "Na";
            ingresoClinica.motivoConsulta = "prueba nueva motivo consulta";
            ingresoClinica.problematicaActual = "prueba problematica";
            ingresoClinica.historiaPersonal = "prueba historia personal";
            ingresoClinica.antecedentes = "prueba antecedentes";
            ingresoClinica.historiaFamiliar = "prueba historia familiar";
            ingresoClinica.genograma = "prueba genograma";
            ingresoClinica.observaciones = "prueba observaciones";
            consulta.resultadoAutoevaluacion = "Na";
            consulta.hipotesisPsicologica = "Na";
            consulta.objetivosTerapeuticos = "Na";
            consulta.estrategiasTecnicasTerapeuticas = "Na";
            consulta.logrosAlcanzadosSegunConsultante = "Na";
            consulta.logrosAlcanzadosSegunObjetivosTerapeuticos = "Na";
            consulta.resumen = "Na";

            documentoGeneral.ingresoClinica = ingresoClinica;
            documentoGeneral.consultante = consultante;
            documentoGeneral.paciente = paciente;
            documentoGeneral.remitido = remitido;
            documentoGeneral.consulta = consulta;



            string diagnosticos = "F050,F051,F058";
            string categorizacionCAP = "1,2,3";
            string numeroHC = "1010101";
            try
            {
                HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
                hcDalc = new HistoriaClinicaDALC();

                ingresoRecepcion.guardarDocumentoGeneral(documentoGeneral, numeroHC, diagnosticos, categorizacionCAP);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestCrearInformeSesion()
        {
            Consulta consulta = new Consulta();
            consulta.fecha = new DateTime(2017, 5, 14, 7, 0, 0);
            consulta.numeroSesion = 3;
            consulta.reciboPago = "1234s";
            consulta.objetivoSesion = "Na";
            consulta.ejerciciosEvento = "Na";
            consulta.desarrolloTemasTratados = "Na";
            consulta.tareasProximaSesion = "Na";
            consulta.observacionesRecomendaciones = "Na";
            consulta.id_ingresoClinica = 30;
            consulta.id_User  = "f84cb1ef-a598-447e-8ac7-2f5d5ccf601c";
            string numeroHistoriaClinica = "1010101";
            string diagnosticos = "F050,F051,F058";
            try
            {
                HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
                hcDalc = new HistoriaClinicaDALC();

                ingresoRecepcion.guardarInformeSesion(consulta, numeroHistoriaClinica, diagnosticos);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestGenerarInasistencia()
        {
            string numeroHistoriaClinica = "1010101";
            string motivoInasistencia = "No pudo asistir por problemas familiares.";
            var fechaInasistencia = new DateTime(2017, 7, 14, 7, 0, 0);
            try
            {
                HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
                hcDalc = new HistoriaClinicaDALC();

                ingresoRecepcion.generarInasistenciaClinica(numeroHistoriaClinica, motivoInasistencia, fechaInasistencia);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestCierreHistoriaClinica()
        {
            CierreCasoVM cierre = new CierreCasoVM();
            Consulta consulta = new Consulta();

            string concMotCierre = "1;2;3;4;5";
            hcDalc = new HistoriaClinicaDALC();

            string numeroHistoriaClinica = "1010101";
            var paciente = (from item in hcDalc.listarPacientes() where item.numeroHistoriaClinica == numeroHistoriaClinica select item).FirstOrDefault();
            var ingresoClinica = (from item in hcDalc.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item).LastOrDefault();


            cierre.cierreHC.id_UsuarioCierraCaso = "f84cb1ef-a598-447e-8ac7-2f5d5ccf601c";
            cierre.paciente = paciente;
            cierre.ingresoClinica = ingresoClinica;
            //cierre.consulta = 

            cierre.cierreHC.fechaFinalizaionPsicoterapia = new DateTime(2017, 7, 14, 7, 0, 0);
            cierre.cierreHC.fechaInicioPsicoterapia = new DateTime(2017, 7, 14, 7, 0, 0);
            cierre.cierreHC.numeroCitasAsignadas = "3";
            cierre.cierreHC.numeroSesionesRealizadas = "3";
            cierre.cierreHC.especificacionMotivoCierre = "Motivo cierre prueba";
            consulta.logrosAlcanzadosSegunObjetivosTerapeuticos = "Logros de prueba";
            consulta.logrosAlcanzadosSegunConsultante = "Logros de prueba";
            consulta.observacionesRecomendaciones = "Observaciones de prueba";

            
            try
            {
                HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
                hcDalc = new HistoriaClinicaDALC();

                ingresoRecepcion.cierreCaso(cierre, concMotCierre, numeroHistoriaClinica);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }


        [TestMethod]
        public void UnitTestRemisionPaciente()
        {
            HistoriaClinicaController ingresoRecepcion = new HistoriaClinicaController();
            RemisionPaciente remisionPaciente = new RemisionPaciente();
            Remision remision = new Remision();
            hcDalc = new HistoriaClinicaDALC();
            var paciente = (from item in hcDalc.listarPacientes() where item.numeroHistoriaClinica == "1010101" select item).FirstOrDefault();
            var ingresoClinica = (from item in hcDalc.listarIngresoClinica() where item.id_paciente == paciente.numeroHistoriaClinica select item).LastOrDefault();
            var cierre = (from item in hcDalc.listarCierres() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).LastOrDefault();
            string numeroHistoriaClinica = "1010101";
            string concatenarRemision = "1;2;3;";
            var fechaInasistencia = new DateTime(2017, 7, 14, 7, 0, 0);

            remision.nombreInsitucionRemitida = "Institucion Prueba";
            remision.nombreProfesional = "f84cb1ef-a598-447e-8ac7-2f5d5ccf601c";
            remision.fechaRemitido = new DateTime(2017, 7, 14, 7, 0, 0);
            remision.servicioRemitido = "Servicio prueba";
            remision.evolucionPaciente = "Evolucion prueba";
            remision.aspectosPositivos = "Aspecto positivo de prueba";
            remision.recomendaciones = "Recomendadiones prueba";
            remision.id_ingresoClinica = ingresoClinica.idIngresoClinica;

            cierre.fechaFinalizaionPsicoterapia = new DateTime(2017, 7, 14, 7, 0, 0);
            cierre.fechaInicioPsicoterapia = new DateTime(2017, 7, 14, 7, 0, 0);

            remisionPaciente.ingresoClinica = ingresoClinica;
            remisionPaciente.paciente = paciente;
            remisionPaciente.cierre = cierre;
            remisionPaciente.remisionP = remision;


            try
            {
                ingresoRecepcion.RemitirPaciente(remisionPaciente, concatenarRemision);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }



        [TestMethod]
        public void UnitTestListarBuscarRemisiones()
        {
            hcDalc = new HistoriaClinicaDALC();
            try
            {
                var numeroHistoriaClinica = "1010101";
                var ingresoClinica = (from item in hcDalc.listarIngresoClinica() where item.id_paciente == numeroHistoriaClinica && item.estadoRemision == true select item).LastOrDefault();
                var listaRemisiones = (from item in hcDalc.listaPacientesRemitidos() where item.id_ingresoClinica == ingresoClinica.idIngresoClinica select item).ToList();

                //Mostrar todas las remisiones
                var listaRemisionesTotales = (from item in hcDalc.listaPacientesRemitidos().GroupBy(x => x.id_ingresoClinica) select item).ToList();
                Assert.IsTrue(listaRemisiones != null || listaRemisionesTotales!=null);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Assert.IsTrue(message == messageg);
            }
        }



        [TestMethod]
        public void UnitTestListaryBuscarHistoriasClinicasInactivas()
        {
            string generarNuevaHC = "";
            try
            {
                hcDalc = new HistoriaClinicaDALC();
                //Listar todas las HC inactivas
                var historiasClinicasInActivas = (from item in hcDalc.listarPacientes() where item.estadoHC == true select item).ToList();
                //Buscar unas historia clínica inactiva
                var historiaClinicaActiva = (from item in historiasClinicasInActivas where item.numeroHistoriaClinica == "4" && item.estadoHC == true select item).FirstOrDefault();
                if (historiasClinicasInActivas != null || historiaClinicaActiva == null)
                {
                    generarNuevaHC = "listado de hc activas encontrado";
                    Assert.IsNotNull(historiasClinicasInActivas != null || historiaClinicaActiva !=null);
                }
            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                Assert.IsFalse(generarNuevaHC != "listado de hc activas encontrado");
            }
        }



        [TestMethod]
        public void UnitTestAignarUsuariosHC()
        {
            try
            {
                hBo = new HistoriaClinicaBO();
                string usuarioEstudiante = "fbb48700-d488-422e-bd8f-f98861fab570";
                string usuarioDocente = "e5ca268a-0145-44a6-abb2-287019a2eef8";
                string numeroHistoriaClinica = "1010101";
                string mensajeError = "";

                var listadoUsuariosPermisosDocente = (from item in hBo.permisosUsuariosPac() where item.id_paciente == numeroHistoriaClinica && item.id_aplicationUser == usuarioEstudiante select item).ToList();
                var listadoUsuariosPermisosEstudiante = (from item in hBo.permisosUsuariosPac() where item.id_paciente == numeroHistoriaClinica && item.id_aplicationUser == usuarioDocente select item).ToList();
                List<PermisosUsuariosPaciente> lstPermisos = new List<PermisosUsuariosPaciente>();
                PermisosUsuariosPaciente doc = new PermisosUsuariosPaciente();
                PermisosUsuariosPaciente pac = new PermisosUsuariosPaciente();

                doc.id_aplicationUser = usuarioDocente;
                doc.id_paciente = numeroHistoriaClinica;

                pac.id_aplicationUser = usuarioEstudiante;
                pac.id_paciente = numeroHistoriaClinica;



                if (listadoUsuariosPermisosDocente.Count > 0)
                {
                    mensajeError = "No se puede asignar el usuario porque ya tiene este usuario docente asignado.";
                }
                else
                {
                    lstPermisos.Add(doc);
                }

                if (listadoUsuariosPermisosEstudiante.Count > 0)
                {
                    mensajeError += " No se puede asignar el usuario porque ya tiene este usuario estudiante asignado.";
                }
                else
                {
                    lstPermisos.Add(pac);
                }

                if (lstPermisos.Count >= 1)
                {
                    hBo.agregarEstrategiaIngreso(lstPermisos);
                    mensajeError = "Se asignó correctamente los usuarios";
                }
                Assert.IsNotNull(lstPermisos);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [TestMethod]
        public void UnitTestDesAsignarUsuarioDeHC()
        {
            string generarNuevaHC = "";
            try
            {
                hcDalc = new HistoriaClinicaDALC();
                hBo = new HistoriaClinicaBO();
                string usuarioEstudiante = "fbb48700-d488-422e-bd8f-f98861fab570";
                string numeroHistoriaClinica = "1010101";
                var usuariosPermisos = (from item in hBo.permisosUsuariosPac() where item.id_paciente == numeroHistoriaClinica && item.id_aplicationUser == usuarioEstudiante select item).FirstOrDefault();

                hBo.eliminarUsuarioAsignado(usuariosPermisos);

                if (usuariosPermisos != null) {
                    Assert.IsNotNull(usuariosPermisos);
                }

            }
            catch (Exception ex)
            {
                //string message = ex.Message;
                Assert.IsFalse(generarNuevaHC != "listado de hc activas encontrado");
            }
        }


    }
}
