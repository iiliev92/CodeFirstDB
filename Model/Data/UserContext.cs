using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class UserContext : IDB<User, int>
    {
        private readonly Context dbContext;
        public UserContext(Context context)
        {
            dbContext = context;
        }

        public void Create(User item)
        {
            //Check if the foreign keys exists in the DB
            List<Product> products = new List<Product>(item.Products.Count);

            foreach (Product product in item.Products)
            {
                Product productFromDB = dbContext.Products.Find(product.Barcode);

                if (productFromDB != null)
                {
                    products.Add(productFromDB);
                }
                else
                {
                    products.Add(product);
                }
            }

            item.Products = products;

            dbContext.Users.Add(item);
            dbContext.SaveChanges();
        
        }

        public User Read(int key)
        {
            User user = dbContext.Users.Find(key);

            if (user == null)
            {
                throw new ArgumentException("There is no user with that id!");
            }

            return user;
        }

        public ICollection<User> ReadAll()
        {
            return dbContext.Users.ToList();
        }

        public void Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> Find(string index)
        {
            throw new NotImplementedException();
        }

    }
}
