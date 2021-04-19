using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class ProductContext : IDB<Product, string>
    {
        private readonly Context dbContext;

        public ProductContext(Context context)
        {
            dbContext = context;
        }

        public void Create(Product item)
        {
            Brand brandFromDB = dbContext.Brands.Find(item.Brand.ID);

            //Check if the foreign keys exists in the DB
            if (brandFromDB != null)
            {
                item.Brand = brandFromDB;
            }

            dbContext.Products.Add(item);
            dbContext.SaveChanges();
        }

        public Product Read(string key)
        {
            Product productFromDB = dbContext.Products.Find(key);

            if (productFromDB == null)
            {
                throw new ArgumentNullException("There is no product with that id!");
            }

            // Explicit loading of navigation property (!)
            // Use Collection(...) instead of Reference(...) for ICollection properties
            dbContext.Entry(productFromDB).Reference(p => p.Brand).Load();
            dbContext.Entry(productFromDB).Collection(p => p.Users).Load();

            return productFromDB;
        }

        public ICollection<Product> ReadAll()
        {
            // Eager loading => using Include(...)
            ICollection<Product> productsFromDB = dbContext.Products.Include(p => p.Brand).Include(p => p.Users).ToList();

            if (productsFromDB == null)
            {
                throw new ArgumentNullException("There are no products in the database!");
            }

            return productsFromDB;
        }

        public void Update(Product item)
        {
            Product productFromDB = dbContext.Products.Find(item.Barcode);

            // Set foreign keys:
            if (item.Users != null)
            {
                List<User> updatedUsers = new List<User>();

                foreach (User user in item.Users)
                {
                    User userFromDB = dbContext.Users.Find(user.ID);

                    if (userFromDB != null)
                    {
                        dbContext.Entry<User>(userFromDB).CurrentValues.SetValues(user);
                        updatedUsers.Add(userFromDB);
                    }
                }

                productFromDB.Users = updatedUsers;
            }


            if (item.Brand != null)
            {
                Brand brandFromDB = dbContext.Brands.Find(item.Brand.ID);

                if (brandFromDB != null)
                {
                    dbContext.Entry<Brand>(brandFromDB).CurrentValues.SetValues(item.Brand);
                }

                productFromDB.Brand = brandFromDB;
            }

            dbContext.Entry<Product>(productFromDB).CurrentValues.SetValues(item);

            dbContext.SaveChanges();
            
        }

        public void Delete(string key)
        {
            Product productFromDB = dbContext.Products.Find(key);
                
            dbContext.Products.Remove(productFromDB);

            dbContext.SaveChanges();
            
        }

        public ICollection<Product> Find(string index)
        {
            ICollection<Product> productsFromDB = dbContext.Products.Where(product => product.Name == index).ToList();
            
            if (productsFromDB == null)
            {
                throw new ArgumentNullException("There are no brands with that value!");
            }

            return productsFromDB;
        }
    }
}
