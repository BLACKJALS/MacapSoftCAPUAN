using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MacapSoftCAPUAN.DALC;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        public UsuarioDALC accCon;

        [TestMethod]
        public void TestMethod1()
        {
            accCon = new UsuarioDALC();
            var listausuarios = accCon.listarUsuarios();
        }
    }
}
