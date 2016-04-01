using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class Stock
    {

        public Stock()
        {
            this.Products = new HashSet<Product>();
        }

        public int StockId { get; set; }
        public string Name { get; set;}

        public string Address { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
