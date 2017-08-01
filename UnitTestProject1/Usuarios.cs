using MacapSoftCAPUAN.Controllers;
using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class Usuarios
    {
        public ApplicationDbContext bd;
        public AccountController userDalc { get; set; }


        public async Task TestMethodCrearUsuarios()
        {
            bd = new ApplicationDbContext();
            userDalc = new AccountController();
            RegisterViewModel model = new RegisterViewModel();
            model.Email = "prueba1@uan.edu.co";
            model.Password = "Prueba_1";
            model.ConfirmPassword = "Prueba_1";
            model.UserRoles = "Administrador";
            var actionResult = await userDalc.Register(model);
        }

        [TestMethod]
        public async void TestMethodLoguearUsuario()
        {
            bd = new ApplicationDbContext();
            userDalc = new AccountController();
            LoginViewModel model = new LoginViewModel();
            model.Email = "prueba3@uan.edu.co";
            model.Password = "Prueba_1";
            model.Active = false;
            model.RememberMe = false;
            await userDalc.Login(model, null);
        }

        [TestMethod]
        public void TestMethodCrearUsuario() {
            Usuarios usr = new Usuarios();
            usr.TestMethodCrearUsuarios();
        }

        [TestMethod]
        public void TestMethodLoginUsuario()
        {
            Usuarios usr = new Usuarios();
            userDalc = new AccountController();
            var lista = userDalc.ListarUsuarios();
        }

    }
}
