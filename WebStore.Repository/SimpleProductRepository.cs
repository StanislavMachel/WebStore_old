using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Abstract;
using WebStore.DataLayer;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace WebStore.Repository
{
    public class SimpleProductRepository : IProductRepository, IDisposable
    {
        private WebStoreDbContext ctx = new WebStoreDbContext();
      
        public IEnumerable<Product> GetProducts()
        {
            return ctx.Products.ToList().AsEnumerable();
        }
        public Product GetProductById (int? id)
        {
            return ctx.Products.Find(id);
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public void AddNewProduct(Product product)
        {
            ctx.Products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            var product = ctx.Products.Find(id);
            ctx.Products.Remove(product);
         
        }

        public ObservableCollection<Product> ProductsInMemory()
        {
            if (ctx.Products.Local.Count == 0)
            {
                GetProducts();
            }
            return ctx.Products.Local;
        }

        public void Save()
        {
            ctx.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            ctx.Entry(product).State = EntityState.Modified;
        }
    }
}
