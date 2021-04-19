using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class ProductViewModel
    {
        public string Barcode { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public BrandViewModel Brand { get; set; }

        public virtual ICollection<UserViewModel> Users { get; set; }

        public static ProductViewModel GetProductViewModel(Product product, ProductViewModel pvm = null)
        {
            ProductViewModel productViewModel;

            if (pvm == null)
            {
                productViewModel = new ProductViewModel();
            }
            else
            {
                productViewModel = pvm;
            }

            productViewModel.Barcode = product.Barcode;
            productViewModel.Name = product.Name;
            productViewModel.Price = product.Price;
            productViewModel.Quantity = product.Quantity;

            return productViewModel;
        }

        public static List<ProductViewModel> GetProductsViewModel(ICollection<Product> products, List<ProductViewModel> pvmList = null)
        {
            List<ProductViewModel> productsViewModel;

            if (pvmList == null)
            {
                productsViewModel = new List<ProductViewModel>();
            }
            else
            {
                productsViewModel = pvmList;
            }

            foreach (var pvm in products)
            {
                ProductViewModel productViewModel = GetProductViewModel(pvm);

                productsViewModel.Add(productViewModel);
            }

            //for (int i = 0; i < products.Count; i++)
            //{
            //    ProductViewModel productViewModel = GetProductViewModel(products[i]);

            //    productsViewModel.Add(productViewModel);
            //}

            return productsViewModel;
        }

        public static Product GetProduct(ProductViewModel productViewModel)
        {
            Product product = new Product();

            product.Barcode = productViewModel.Barcode;
            product.Name = productViewModel.Name;
            product.Price = productViewModel.Price;
            product.Quantity = productViewModel.Quantity;

            return product;
        }

        public static List<Product> GetProducts(List<ProductViewModel> pvmList)
        {
            List<Product> products = new List<Product>();

            for (int i = 0; i < pvmList.Count; i++)
            {
                Product product = GetProduct(pvmList[i]);

                products.Add(product);
            }

            return products;
        }
    }
}
