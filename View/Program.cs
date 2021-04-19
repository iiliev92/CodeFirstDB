using Controller;
using Controller.Enums;
using Controller.Validation;
using Controller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.ViewManagers;

namespace View
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                BrandViewManager bvm = new BrandViewManager();
                //bvm.CreateBrand();
                //bvm.ReadBrand();
                //bvm.ReadAllBrands();
                //bvm.UpdateBrand();
                //bvm.DeleteBrand();


                ProductViewManager pvm = new ProductViewManager();
                //pvm.CreateProduct();
                //pvm.ReadProduct();
                //pvm.ReadAllProducts();
                //pvm.UpdateProduct();
                //pvm.DeleteProduct();

                UserViewManager uvm = new UserViewManager();
                //uvm.CreateUser();
                //uvm.ReadUser();
                uvm.ReadAllUsers();

            }
            catch (ArgumentException ane)
            {
                if (ane.Data.Contains("Key"))
                {
                    Console.WriteLine(ane.Data["Key"]);
                }
                else
                {
                    Console.WriteLine(ane.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
            
        }

        #region ShowInfo

        public static void ShowBrandInfo(BrandViewModel bvm, bool showOnlyBrandInfo = false)
        {
            if (showOnlyBrandInfo)
            {
                Console.WriteLine("Name: {0}", bvm.Name);
                Console.WriteLine();
                return;
            }
            else
            {
                Console.WriteLine("Name: {0}", bvm.Name);
                Console.WriteLine();
                

                if (bvm.Products != null)
                {
                    Console.WriteLine("### Products Info: ###");
                    Console.WriteLine();

                    foreach (ProductViewModel pvm in bvm.Products)
                    {
                        ShowProductInfo(pvm, true);
                    }
                }
            }
        }

        public static void ShowBrandsInfo(List<BrandViewModel> bvmList)
        {
            Console.WriteLine("### Brands Information: ###");
            Console.WriteLine();

            for (int i = 0; i < bvmList.Count; i++)
            {
                Console.WriteLine("#Brand [{0}]", i + 1);
                ShowBrandInfo(bvmList[i]);
                Console.WriteLine();
            }
        }

        // Showing the primary keys as well
        public static void ShowBrandsInfoAdmin(List<BrandViewModel> bvmList, bool showOnlyBrandInfo = false)
        {
            Console.WriteLine("### Brands Information: ###");
            Console.WriteLine();

            for (int i = 0; i < bvmList.Count; i++)
            {
                Console.WriteLine("#Brand [{0}]", i + 1);

                if (showOnlyBrandInfo)
                {
                    Console.WriteLine("ID: {0}", bvmList[i].ID);
                    Console.WriteLine("Name: {0}", bvmList[i].Name);
                    Console.WriteLine();
                    return;
                }
                else
                {
                    Console.WriteLine("ID: {0}", bvmList[i].ID);
                    Console.WriteLine("Name: {0}", bvmList[i].Name);
                    Console.WriteLine();

                    if (bvmList[i].Products != null)
                    {
                        Console.WriteLine("### Products Info: ###");
                        Console.WriteLine();

                        foreach (ProductViewModel pvm in bvmList[i].Products)
                        {
                            ShowProductInfo(pvm);
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        public static void ShowProductInfo(ProductViewModel pvm, bool showOnlyProductInfo = false)
        {
            Console.WriteLine("Barcode: {0}; Name: {1}; Price: {2}; Quantity: {3}",
                pvm.Barcode, pvm.Name, pvm.Price, pvm.Quantity);

            if (!showOnlyProductInfo)
            {
                ShowBrandInfo(pvm.Brand, true);
            }

            Console.WriteLine();
        }


        public static void ShowProductsInfo(List<ProductViewModel> pvmList, bool showOnlyProductInfo = false)
        {
            Console.WriteLine("### Products Information: ###");
            Console.WriteLine();

            for (int i = 0; i < pvmList.Count; i++)
            {
                Console.WriteLine("#Product [{0}]", i + 1);
                ShowProductInfo(pvmList[i], showOnlyProductInfo);
                Console.WriteLine();
            }
        }

        public static void ShowUserInfo(UserViewModel uvm, bool showOnlyUserInfo = false)
        {
            Console.WriteLine("Name: {0}", uvm.Name);

            if (uvm.Age != null)
            {
                Console.WriteLine("Age: {0}", uvm.Age);
            }

            if (showOnlyUserInfo)
            {
                return;
            }

            ShowProductsInfo(uvm.Products.ToList(), true);
        }

        public static void ShowUsersInfo(List<UserViewModel> uvmList, bool showOnlyUserInfo = false)
        {
            foreach (UserViewModel uvm in uvmList)
            {
                ShowUserInfo(uvm, showOnlyUserInfo);
            }
        }

        #endregion


    }
}
