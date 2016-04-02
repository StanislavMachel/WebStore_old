using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Domain.Abstract;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace WebStore.Domain.Concrete
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
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    ctx.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    ctx.Entry(ctx.Products.Local[0]).State = EntityState.Modified;
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

        }

        public void UpdateProduct(Product product)
        {
            using (var ctx = new WebStoreDbContext())
            {
                ctx.Entry(product).State = EntityState.Modified;
                ctx.SaveChanges();
            } 
        }
    }
}
