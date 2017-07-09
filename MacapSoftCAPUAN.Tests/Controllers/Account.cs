using MacapSoftCAPUAN.Controllers;
using MacapSoftCAPUAN.DALC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacapSoftCAPUAN.Tests.Controllers
{
    [TestClass]
    public class Account
    {
        public UsuarioDALC accCon;

        [TestMethod]
        public void TestMethodListarUsuarios()
        {
            accCon = new UsuarioDALC();
            var listausuarios = accCon.listarUsuarios();
        }
    }
}
