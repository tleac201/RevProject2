using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2.Controllers;
namespace Project2.Tests.Controllers
{
	[TestClass]
	public class ShoppingCartControllerTest
	{
		private ICollection<ShoppingCartItem> _SCI;
		private ShoppingCartItem _TestData;
		private User _user;
		private ShoppingCartController _controller;

		public ShoppingCartControllerTest()
		{
			//_controller.
			_user = new User
			{
				UserId = 1,
				Active = true,
				OpenDate = DateTime.Now
			};

			_SCI = new HashSet<ShoppingCartItem>();
			

			_TestData = new ShoppingCartItem
			{
				Standard = true,
				Id = 1,
				DatePlaced = DateTime.Now,
				Quantity = 1,
				ProductId = 1,
				UserId = 1,
				User = _user
			};
			_user.ShoppingCartItems = _SCI;
		}

		[TestMethod]
		public void Post()
		{
			// Arrange

			// Act
			// _controller.PostShoppingCartItem(_TestData);
			// Assert

		}

		[TestMethod]
		public void Get()
		{
			// Arrange
			
			// Act
			
			// Assert
			
		}

		[TestMethod]
		public void Put()
		{
			// Arrange

			// Act

			// Assert

		}

		[TestMethod]
		public void Delete()
		{
			// Arrange

			// Act

			// Assert

		}
	}
}
