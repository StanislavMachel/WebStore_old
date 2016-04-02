using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Controllers;
using System.Web.Mvc;

namespace WebStore.UnitTests
{
    /// <summary>
    /// Summary description for CartControllerUnitTests
    /// </summary>
    [TestClass]
    public class CartControllerUnitTests
    {
       [TestMethod]
       public void cannot_checkout_empty_cart()
        {
            //Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();

            ShippingDetails shippingDetails = new ShippingDetails();

            CartController target = new CartController(null, mock.Object);

            //Act

            ViewResult result = target.Checkout(cart, shippingDetails);

            //Assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void cannot_checkout_invalid_shippingdetails()
        {
            //arrage
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddProduct(new Product(), 1);

            CartController target = new CartController(null, mock.Object);

            target.ModelState.AddModelError("error", "error");

            //act

            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //assert
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }

        [TestMethod]
        public void can_checkout_and_submit_order()
        {
            //arrage
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            Cart cart = new Cart();
            cart.AddProduct(new Product(), 1);

            CartController target = new CartController(null, mock.Object);

            //act
            ViewResult result = target.Checkout(cart, new ShippingDetails());

            //assert

            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
