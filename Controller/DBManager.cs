using Model.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.Enums;
using Model.Model;
using Controller.Events;
using Controller.ViewModels;
using Controller.Validation;

namespace Controller
{
    
    public class DBManager
    {
        private BrandContext brandContext;
        private ProductContext productContext;
        private UserContext userContext;

        private readonly Context context;

        public DBManager()
        {
            context = new Context();
            brandContext = new BrandContext();
            productContext = new ProductContext(context);
            userContext = new UserContext(context);
        }

        public void Create(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:
                    
                    string name = args[0].ToString();

                    Brand brand = new Brand();
                    brand.Name = name;

                    brandContext.Create(brand);

                    break;

                case ModelType.PRODUCT:

                    ProductViewModel productViewModel = (ProductViewModel)args[0];

                    Product product = ProductViewModel.GetProduct(productViewModel);

                    product.Brand = BrandViewModel.GetBrand(productViewModel.Brand);

                    productContext.Create(product);

                    break;

                case ModelType.USER:

                    UserViewModel userViewModel = (UserViewModel)args[0];

                    User user = UserViewModel.GetUser(userViewModel);

                    List<Product> products = ProductViewModel.GetProducts(userViewModel.Products.ToList());

                    user.Products = products;

                    userContext.Create(user);

                    break;

                default:
                    break;
            }
        }

        public void Read(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    int key = (int)args[0];

                    Brand brandFromDB = brandContext.Read(key);

                    BrandViewModel brandViewModel = (BrandViewModel)args[1];
                    BrandViewModel.GetBrandViewModel(brandFromDB, brandViewModel);

                    // We already eager load them so they won't be null!
                    if (brandFromDB.Products.Count != 0) 
                    {
                        brandViewModel.Products = ProductViewModel.GetProductsViewModel(brandFromDB.Products.ToList());
                    }

                    break;

                case ModelType.PRODUCT:

                    string barcode = (string)args[0];

                    Product productFromDB = productContext.Read(barcode);

                    ProductViewModel productViewModel = (ProductViewModel)args[1];
                    ProductViewModel.GetProductViewModel(productFromDB, productViewModel);

                    if (productFromDB.Brand != null)
                    {
                        productViewModel.Brand = BrandViewModel.GetBrandViewModel(productFromDB.Brand);
                    }

                    break;

                case ModelType.USER:

                    int userID = (int)args[0];
                    UserViewModel uvm = (UserViewModel)args[1];

                    User userFromDB = userContext.Read(userID);

                    UserViewModel.GetUserViewModel(userFromDB, uvm);

                    if (ValidationManager.ValidateCollection<Product>(userFromDB.Products))
                    {
                        uvm.Products = ProductViewModel.GetProductsViewModel(userFromDB.Products.ToList());
                    }

                    break;

                default:
                    break;
            }
        }

        public void ReadAll(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    List<Brand> brandsFromDB = brandContext.ReadAll().ToList();

                    List<BrandViewModel> bvmsList = (List<BrandViewModel>)args[0];

                    BrandViewModel.GetBrandsViewModel(brandsFromDB, bvmsList);

                    for (int i = 0; i < brandsFromDB.Count; i++)
                    {
                        if (brandsFromDB[i].Products.Count != 0)
                        {
                            bvmsList[i].Products = ProductViewModel.GetProductsViewModel(brandsFromDB[i].Products.ToList());
                        }
                    }

                    break;

                case ModelType.PRODUCT:

                    List<Product> productsFromDB = productContext.ReadAll().ToList();

                    List<ProductViewModel> pvmsList = (List<ProductViewModel>)args[0];

                    ProductViewModel.GetProductsViewModel(productsFromDB, pvmsList);

                    for (int i = 0; i < productsFromDB.Count; i++)
                    {
                        if (productsFromDB[i].Brand != null)
                        {
                            pvmsList[i].Brand = BrandViewModel.GetBrandViewModel(productsFromDB[i].Brand);
                        }

                        if (productsFromDB[i].Users.Count != 0)
                        {
                            pvmsList[i].Users = UserViewModel.GetUsersViewModel(productsFromDB[i].Users.ToList());
                        }
                    }

                    break;

                case ModelType.USER:

                    ICollection<User> users = userContext.ReadAll();

                    List<UserViewModel> uvmsList = (List<UserViewModel>)args[0];

                    UserViewModel.GetUsersViewModel(users, uvmsList);

                    // Using ICollection and not List example:
                    int index = 0;
                    foreach (var user in users)
                    {
                        List<ProductViewModel> pvmsForUserList = ProductViewModel.GetProductsViewModel(user.Products);

                        uvmsList[index].Products = pvmsForUserList;
                        index++;
                    }

                    break;

                default:
                    break;
            }
        }

        public void Update(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    BrandViewModel brandViewModel = (BrandViewModel)args[0];
                    Brand updatedBrand = BrandViewModel.GetBrand(brandViewModel);

                    if (brandViewModel.Products != null)
                    {
                        updatedBrand.Products = ProductViewModel.GetProducts(brandViewModel.Products.ToList());
                    }

                    brandContext.Update(updatedBrand);

                    break;

                case ModelType.PRODUCT:

                    ProductViewModel productViewModel = (ProductViewModel)args[0];
                    Product updatedProduct = ProductViewModel.GetProduct(productViewModel);

                    if (productViewModel.Brand != null)
                    {
                        updatedProduct.Brand = BrandViewModel.GetBrand(productViewModel.Brand);
                    }

                    if (productViewModel.Users != null)
                    {
                        updatedProduct.Users = UserViewModel.GetUsers(productViewModel.Users.ToList());
                    }

                    productContext.Update(updatedProduct);

                    break;

                case ModelType.USER:
                    break;

                default:
                    break;
            }
        }

        public void Delete(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    int brandID = (int)args[0];
                    
                    brandContext.Delete(brandID);

                    break;
                case ModelType.PRODUCT:

                    string barcode = (string)args[0];

                    productContext.Delete(barcode);

                    break;

                case ModelType.USER:
                    break;

                default:
                    break;
            }
        }

    }
}
