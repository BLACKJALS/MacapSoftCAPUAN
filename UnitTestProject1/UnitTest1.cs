using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.ModelsVM;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private DiagnosticosDALC bd;
        [TestMethod]
        public void TestMethodCrearDiagnostico()
        {
            bd = new DiagnosticosDALC();
            var diagnostico = new DiagnosticoVM();

            string codigo = "Prueba";
            string nombre = "Enfermedad prueba";
            string destacado = "No";

            diagnostico.Nombre = nombre;
            diagnostico.Codigo = codigo;
            diagnostico.Destacado = destacado;
            try
            {
                bd.CrearDiagnostico(codigo, nombre, destacado);
            }
            catch (Exception e)
            {
                string message = e.Message;
                Assert.IsTrue(message != "");
            }

            codigo = "Prueba";
            nombre = "Enfermedad prueba";
            destacado = "No";

            diagnostico.Nombre = nombre;
            diagnostico.Codigo = codigo;
            diagnostico.Destacado = destacado;
            try
            {
                bd.CrearDiagnostico(codigo, nombre, destacado);
            }
            catch (Exception e)
            {
                string message = e.Message;
                Assert.IsTrue(message != "");
            }
        }


        [TestMethod]
        public void TestMethodListarDiagnosticos()
        {
            bd = new DiagnosticosDALC();
            var diagnostico = new DiagnosticoVM();

            var lista = bd.ListarDiagnosticos();
            Assert.IsTrue(!(String.IsNullOrEmpty(lista.ToString())));
        }


        [TestMethod]
        public void TestMethodBuscarDiagnostico()
        {
            bd = new DiagnosticosDALC();
            var diagnostico = new DiagnosticoVM();
            var codigoDiagnostico = "Prueba";

            var lista = bd.ListarDiagnosticos();
            foreach (var item in lista)
            {
                if (item.Codigo == codigoDiagnostico)
                {
                    Assert.AreEqual(codigoDiagnostico, item.Codigo);
                    break;
                }
            }

        }


        [TestMethod]
        public void TestMethodModificarDiagnostico()
        {
            bd = new DiagnosticosDALC();
            var diagnostico = new DiagnosticoVM();
            string codigo = "Prueba";
            string nombre = "Enfermedad anorexia";
            string destacado = "NO";

            try
            {
                bd.ModificarDiagnostico(codigo, nombre, destacado);
            }
            catch (Exception e)
            {
                string message = e.Message;
                Assert.IsTrue(message != "");
            }

        }


        [TestMethod]
        public void TestMethodInformacionNoIngresada()
        {
            bd = new DiagnosticosDALC();
            var diagnostico = new DiagnosticoVM();

            string codigo = "";
            string nombre = "Enfermedad anorexia";
            string destacado = "No";

            diagnostico.Nombre = nombre;
            diagnostico.Codigo = codigo;
            diagnostico.Destacado = destacado;
            try
            {
                bd.CrearDiagnostico(codigo, nombre, destacado);
            }
            catch (Exception e)
            {
                string message = e.Message;
                Assert.IsTrue(message != "");
            }
        }
    }
}
