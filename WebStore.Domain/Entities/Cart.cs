using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        private List<ProductQuantity> productQuantityCollection = new List<ProductQuantity>();

        public void AddProduct(Product product, int quantity)
        {
            ProductQuantity productQuantity = productQuantityCollection
                                            .Where(p => p.Product.ProductID == product.ProductID)
                                            .FirstOrDefault();

            if(productQuantity == null)
            {
                productQuantityCollection.Add(new ProductQuantity
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                productQuantity.Quantity += quantity;
            }
        }

        public void RemoveProduct(Product product)
        {
            productQuantityCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalCost()
        {
            return productQuantityCollection.Sum(pq => pq.Product.Price * pq.Quantity);
        }

        public void Clear()
        {
            productQuantityCollection.Clear();
        }

        public IEnumerable<ProductQuantity> ProductQuantityCollection
        {
            get
            {
                return productQuantityCollection;
            }
        }

    }
}
