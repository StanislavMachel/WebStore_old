using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using MvcContrib.Pagination;
using PagedList;

namespace WebStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        // GET: Product
        public ActionResult List(int? page, string q, string category)
        {


            //var products = repository.GetProducts(q).AsPagination(page ?? 1, 10);
            var products = repository.GetProducts()
                .OrderBy(p => p.ProductID)
                .Where(p => p.Name.StartsWith(q) || q == null)
                .Where(p => category == null || p.Category == category)
                .ToPagedList(page ?? 1, 2);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductTable", products);
            }
            return View(products);
        }

        // GET: Product/Details/5
    }
}
