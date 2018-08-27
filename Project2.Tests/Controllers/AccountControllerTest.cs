using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2.Controllers;
using Project2.Tests.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;

namespace Project2.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        UserRepoTest UserRepo = new UserRepoTest();
        /*
         * Would not advise testing with Identity functions. Behavior is wonky
         * in debug mode.
        [TestMethod]
        public async Task Register()
        {
            Debug.WriteLine("Test register");
            // Arrange
            AccountController controller = new AccountController(UserRepo);
            var vm = new Project2.Models.RegisterBindingModel
            {
                Email = "a@a.a",
                Password = "Password!123",
                ConfirmPassword = "Password!123"
            };
            
            // Act
            await controller.Register(vm);

            // Assert
            Debug.WriteLine($"{UserRepo.RetrieveAll().Count()}");
            Assert.AreEqual(1, UserRepo.RetrieveAll().Count());
        }
        */
    }
}
