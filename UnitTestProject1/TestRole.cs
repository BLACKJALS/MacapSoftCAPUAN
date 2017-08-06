using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MacapSoftCAPUAN.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using MacapSoftCAPUAN.Models;

namespace UnitTestProject1
{
    public class TestRole : Controller
    {
        public LoginViewModel login { get; set; }
        //public LoginViewModel login { get; set; }

        public TestRole(LoginViewModel login1) {
            login = login1;
        }

        [TestMethod]
        public void ShouldNotAcceptInvalidUser()
        {
            // Arrange
            var principalMock = new Mock<LoginViewModel>();
            principalMock.Setup(x => x.Email).Returns("");
            var httpContextMock = new Mock<HttpContextBase>();
            //httpContextMock.Setup(x => x.User).Returns(principalMock.Object());
        }
    }
}
