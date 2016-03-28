using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class ProductQuantity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
