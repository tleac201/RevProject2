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
using System.Web.Http;
using System.Net;

namespace Project2.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        UserRepoTest UserRepo { get; set; }
		AccountController controller { get; set; }
		
		public AccountControllerTest()
        {
			UserRepo = new UserRepoTest();
			controller = new AccountController(UserRepo);
			var user1 = new User
            {
                UserId = 1,
                FirstName = "J",
                LastName = "M",
                Email = "jm@e.e",
                Phone = null,
                Active = true,
                OpenDate = DateTime.Now,
                CloseDate = null
            };

            UserRepo.Insert(user1);
        }

		[TestMethod]
		public void InvalidUserPut()
		{
			//Test that a user that does not exist cannot edit
			
			// Arrange

			var user = new User {
				UserId = 3,
				FirstName = "Test",
				LastName = "Tester", 
				Active = true,
			};
			var expected = typeof(System.Web.Http.Results.BadRequestResult);

			// Act

			var result = controller.Put(user);

			// Assert
			Assert.IsInstanceOfType(result, expected);
        }

		[TestMethod]
		public void EditEmail()
		{
			// Email is not an editable attribute. Should remain the same.
			// Arrange
			
			var user = UserRepo.RetrieveById(1);
			var expected = user.Email;
			var editedUser = new User
			{
				UserId = user.UserId,
				FirstName = "J",
				LastName = "M",
				Email = "swiggity.swooty@comin4.booty",
				Phone = null,
				Active = true,
				OpenDate = DateTime.Now,
				CloseDate = null
			};

			// Act

			controller.Put(user);
			var result = UserRepo.RetrieveById(user.UserId).Email;

			// Assert

			Assert.AreEqual(expected, result);
		}
        
        /*
        Would not advise testing with Identity functions. Behavior is wonky
        in debug mode.
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
