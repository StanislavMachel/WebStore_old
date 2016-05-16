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
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        // GET: Product
        public ActionResult Index(int? page, string q, string category)
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Description,Price,Category")] Product product )
        {
            if (ModelState.IsValid)
            {
                repository.AddNewProduct(product);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = repository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Description,Price,Category")] Product product/*, HttpPostedFileBase image = null*/)
        {
            if (ModelState.IsValid)
            {
                //if (image != null)
                //{
                //    product.ImageMimeType = image.ContentType;
                //    product.ImageData = new byte[image.ContentLength];
                //    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                //}

                repository.UpdateProduct(product);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.GetProductById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteProduct(id);
            repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
