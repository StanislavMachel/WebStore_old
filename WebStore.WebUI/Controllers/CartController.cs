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

        public CartController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        public ViewResult Index(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            Cart cart = GetCart();
            return View(cart);
        }
        public RedirectResult AddToCart(int productId, string returnUrlQuery, int quantity = 1)
        {
            Product product = repository.GetProductById(productId);

            if(product != null)
            {
                GetCart().AddProduct(product, quantity); //TODO: add more that 1 product
            }

            return Redirect(returnUrlQuery);
        }

        public RedirectResult RemoveFromCart(int productId, string returnUrlQuery)
        {
            Product product = repository.GetProductById(productId);

            if (product != null)
            {
                GetCart().RemoveProduct(product); //TODO: add more that 1 product
            }

            return Redirect(returnUrlQuery);
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
    }
}