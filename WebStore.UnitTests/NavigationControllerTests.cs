using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using WebStore.WebUI.Controllers;
using System.Web.Mvc;
namespace WebStore.UnitTests
{
    [TestClass]
    public class NavigationControllerTests
    {
        [TestMethod]
        public void can_create_categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts())
                .Returns((new List<Product>()
                    {
                        new Product {ProductID = 1, Name = "P1", Category = "C1" },
                        new Product {ProductID = 1, Name = "P2", Category = "C1" },
                        new Product {ProductID = 1, Name = "P3", Category = "C2" },
                        new Product {ProductID = 1, Name = "P4", Category = "C3" }
                    }).AsQueryable());

            NavigationController testController = new NavigationController(mock.Object);
            IEnumerable<string> result = (IEnumerable<string>)testController.Menu().Model;

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result.ElementAt(0), "C1");
            Assert.AreEqual(result.ElementAt(1), "C2");
            Assert.AreEqual(result.ElementAt(2), "C3");
        }
    }
}
