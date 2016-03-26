using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(string q);
        IEnumerable<Product> GetProducts();
        Product GetProductById(int? id);
        ObservableCollection<Product> ProductsInMemory();
        void AddNewProduct(Product product);
        void DeleteProduct(int id);
        void UpdateProduct(Product product);
        void Dispose();
       
        void Save();
    }
}
