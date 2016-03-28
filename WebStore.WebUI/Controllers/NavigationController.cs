using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;

namespace WebStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IProductRepository repository;
        public NavigationController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        
        public string Menu()
        {
            return "Hello from NavigationController";
        }
    }
}