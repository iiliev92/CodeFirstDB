using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class Context : DbContext
    {
        public Context()
            : base("CodeFirstDB")
            // : base("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;MultipleActiveResultSets=true;");
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
