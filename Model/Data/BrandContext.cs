using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; // (!) For Lambda expressions in Include(...)

namespace Model.Data
{
    public class BrandContext : IDB<Brand, int>
    {
        public void Create(Brand item)
        {
            using (Context dbContext = new Context())
            {
                dbContext.Brands.Add(item);
                dbContext.SaveChanges();
            }
        }

        public Brand Read(int key)
        {
            Brand brandFromDB;

            try
            {
                using (Context dbContext = new Context())
                {
                    //brandFromDB = dbContext.Brands.Find(key);
                    brandFromDB = dbContext.Brands.Include(b => b.Products).Single(b => b.ID == key);
                }
            }
            catch (InvalidOperationException)
            {
                ArgumentException ae = new ArgumentException();
                ae.Data.Add("Key", "There is no brand with that id!");
                throw ae;
            }

            // Uncomment if you are using Find(primary key)
            //if (brandFromDB == null)
            //{
            // throw new ArgumentException("There is no brand with that id!");
            //}

            return brandFromDB; 
        }

        public ICollection<Brand> ReadAll()
        {
            ICollection<Brand> brandsFromDB;

            using(Context dbContext = new Context())
	        {
                brandsFromDB = dbContext.Brands.Include(b => b.Products).ToList();
	        }

            if (brandsFromDB == null)
            {
                throw new ArgumentException("There are no brands in the database!");
            }

            return brandsFromDB;
        }

        public void Update(Brand item)
        {
            using (Context dbContext = new Context())
            {

                // I way:
                Brand brandFromDB = dbContext.Brands.Find(item.ID);

                // Set foreign key:
                if (item.Products != null)
                {
                    List<Product> updatedProducts = new List<Product>();

                    foreach (Product product in item.Products)
                    {
                        Product productFromDB = dbContext.Products.Find(product.Barcode);

                        if (productFromDB != null)
                        {
                            dbContext.Entry<Product>(productFromDB).CurrentValues.SetValues(product);
                            updatedProducts.Add(productFromDB);
                        }
                        else
                        {
                            throw new ArgumentException("This exception should never be raised! First update and then delete items!");
                        }
                    }

                    brandFromDB.Products = updatedProducts;
                }

                dbContext.Entry<Brand>(brandFromDB).CurrentValues.SetValues(item);


                //II way:
                //Brand brandFromDB = dbContext.Brands.Find(item.ID);
                //brandFromDB.Name = item.Name;
                //Again you should set the foreign key

                // III way:
                //dbContext.Entry<Brand>(item).State = EntityState.Modified;
                //Again you should set the foreign key


                dbContext.SaveChanges();
            }
        }

        public void Delete(int key)
        {
            using (Context dbContext = new Context())
            {
                // I way:
                Brand brandFromDB = new Brand();
                brandFromDB.ID = key;
                
                dbContext.Entry<Brand>(brandFromDB).State = EntityState.Deleted;

                // II way:
                //Brand brandFromDB = dbContext.Brands.Find(key);

                //if (brandFromDB == null)
                //{
                //    throw new ArgumentException("There is no brand with that id!");
                //}

                // dbContext.Brands.Remove(brandFromDB);


                dbContext.SaveChanges();
            }
        }

        public ICollection<Brand> Find(string index)
        {
            ICollection<Brand> brandsFromDB;

            using (Context dbContext = new Context())
            {
                brandsFromDB = dbContext.Brands.Include(b => b.Products).Where(brand => brand.Name == index).ToList();
            }

            if (brandsFromDB == null)
            {
                throw new ArgumentException("There are no brands with that value!");
            }

            return brandsFromDB;
        }
    }
}
