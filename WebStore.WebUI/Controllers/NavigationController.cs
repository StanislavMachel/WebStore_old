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
        
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.GetProducts()
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p);
            return PartialView(categories);
        }
    }
}