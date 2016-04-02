using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;

namespace WebStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor processor;

        public CartController(IProductRepository productRepository, IOrderProcessor orderProcessor)
        {
            repository = productRepository;
            processor = orderProcessor;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {

            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl, int quantity = 1)
        {
            Product product = repository.GetProductById(productId);

            if(product != null)
            {
                cart.AddProduct(product, quantity); //TODO: add more that 1 product
            }
            return RedirectToAction("Index", new { returnUrl });
            //return Redirect(returnUrlQuery);
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.GetProductById(productId);

            if (product != null)
            {
                cart.RemoveProduct(product); //TODO: add more that 1 product
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult Summary (Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {

            if (cart.ProductQuantityCollection.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, you cart is empty!");
            }
            if (ModelState.IsValid)
            {
                processor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}