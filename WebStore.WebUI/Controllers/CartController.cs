using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;

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
        public ViewResult Index(string returnUrl)
        {
            if (returnUrl != null)
                ViewData["ReturnUrl"] = returnUrl;
            else
                ViewData["ReturnUrl"] = "/Product";

            Cart cart = GetCart();
            return View(cart);
        }
        public RedirectToRouteResult AddToCart(int productId, string returnUrl, int quantity = 1)
        {
            Product product = repository.GetProductById(productId);

            if(product != null)
            {
                GetCart().AddProduct(product, quantity); //TODO: add more that 1 product
            }
            return RedirectToAction("Index", new { returnUrl });
            //return Redirect(returnUrlQuery);
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.GetProductById(productId);

            if (product != null)
            {
                GetCart().RemoveProduct(product); //TODO: add more that 1 product
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
        public PartialViewResult Summary ()
        {

            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        //[HttpPost]
        //public ViewResult Checkout(Cart cart)
        //{
            
        //    if (cart.ProductQuantityCollection.Count() == 0)
        //    {
        //        ModelState.AddModelError("", "Sorry, you cart is empty!");
        //    }
        //    else if (ModelState.IsValid)
        //    {
        //        processor.ProcessOrder(cart, shippingDetails)
        //    }
        //}
    }
}