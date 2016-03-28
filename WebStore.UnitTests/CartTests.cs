using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace WebStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void car_add_new_products()
        {
            Cart testCart = new Cart();

            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            testCart.AddProduct(p1, 1);
            testCart.AddProduct(p2, 1);

            IEnumerable<ProductQuantity> result = testCart.ProductQuantityCollection;


            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result.ElementAt(0).Product, p1);
            Assert.AreEqual(result.ElementAt(1).Product, p2);
        }

        [TestMethod]
        public void can_add_quantity_for_existing_product_quantity()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart testCart = new Cart();

            testCart.AddProduct(p1, 1);
            testCart.AddProduct(p2, 1);
            testCart.AddProduct(p2, 10);

            IEnumerable<ProductQuantity> results = testCart.ProductQuantityCollection;

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results.ElementAt(0).Product, p1);
            Assert.AreEqual(results.ElementAt(1).Product, p2);
            Assert.AreEqual(results.ElementAt(0).Quantity, 1);
            Assert.AreEqual(results.ElementAt(1).Quantity, 11);

        }

        [TestMethod]
        public void can_remove_product()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart testCart = new Cart();

            testCart.AddProduct(p1, 1);
            testCart.AddProduct(p2, 2);
            testCart.AddProduct(p3, 3);
            testCart.AddProduct(p2, 2);

            testCart.RemoveProduct(p2);

            IEnumerable<ProductQuantity> results = testCart.ProductQuantityCollection;

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results.Where(p => p.Product == p2).Count(), 0);

        }
        [TestMethod]
        public void calculate_total_cost()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 450M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart testCart = new Cart();

            testCart.AddProduct(p1, 1);
            testCart.AddProduct(p2, 2);
            testCart.AddProduct(p2, 2);

            decimal result = testCart.ComputeTotalCost();

            Assert.AreEqual(result, 650M);

        }
        [TestMethod]
        public void can_clear_cart()
        {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 450M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart testCart = new Cart();

            testCart.AddProduct(p1, 1);
            testCart.AddProduct(p2, 2);
            testCart.AddProduct(p2, 2);

            testCart.Clear();

            Assert.AreEqual(testCart.ProductQuantityCollection.Count(), 0);

        }
    }
}
