using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebStore.Domain.Entities;

namespace WebStore.DataLayer
{
    public class WebStoreDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
