using Controller;
using Controller.Enums;
using Controller.Validation;
using Controller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.ViewManagers
{
    internal class BrandViewManager
    {
        internal List<BrandViewModel> BVMList { get; set; }

        internal DBManager DBManager { get; set; }

        internal BrandViewManager()
        {
            BVMList = new List<BrandViewModel>();
            DBManager = new DBManager();
        }

        internal void CreateBrand()
        {
            string name;

            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
            } while (!ValidationManager.ValidateString(name));

            DBManager.Create(ModelType.BRAND, name);

            Console.WriteLine("Brand created successfully!");
        }

        internal void ReadBrand()
        {
            int key;

            do
            {
                Console.Write("ID: ");
                key = int.Parse(Console.ReadLine());
            } while (key <= 0);

            BrandViewModel bvm = new BrandViewModel();

            DBManager.Read(ModelType.BRAND, key, bvm);

            Program.ShowBrandInfo(bvm);
        }

        internal void ReadAllBrands()
        {
            BVMList = new List<BrandViewModel>();

            DBManager.ReadAll(ModelType.BRAND, BVMList);

            Program.ShowBrandsInfo(BVMList);
        }

        internal void UpdateBrand()
        {
            ReadAllBrands();

            int choice;

            do
            {
                Console.WriteLine("Select brand [1..{0}]", BVMList.Count);
                choice = int.Parse(Console.ReadLine());
            } while (choice < 0 || choice > BVMList.Count);

            BrandViewModel brandViewModel = BVMList[choice - 1];

            Console.Write("Do you want to change the name for this brand? [Y/y or N/n] => ");
            string changeBrand = Console.ReadLine().ToLower();
            if (changeBrand == "y")
            {
                Console.Write("New Name: ");
                brandViewModel.Name = Console.ReadLine();
            }

            string changeProduct = string.Empty;

            if (brandViewModel.Products.Count != 0)
            {
                Console.Write("Do you want to update the products for this brand? [Y/y or N/n]  => ");
                changeProduct = Console.ReadLine().ToLower();

                if (changeProduct == "y")
                {
                    Program.ShowProductsInfo(brandViewModel.Products.ToList(), true);
                    int productChoice;

                    do
                    {
                        Console.Write("Select product [1..{0}]", brandViewModel.Products.Count);
                        productChoice = int.Parse(Console.ReadLine());
                    } while (productChoice < 0 || productChoice > brandViewModel.Products.Count);

                    ProductViewModel productViewModel = brandViewModel.Products.ToList()[choice - 1];

                    do
                    {
                        Console.WriteLine("1) Update existing products");
                        Console.WriteLine("2) Delete existing products");
                        Console.Write("Your choice: ");
                        choice = int.Parse(Console.ReadLine());
                    } while (choice < 1 || choice > 2);

                    switch (choice)
                    {
                        case 1:

                            Console.Write("Do you want to change the name for this product? [Y/y or N/n]  => ");
                            string updateProduct = Console.ReadLine().ToLower();
                            if (updateProduct == "y")
                            {
                                Console.Write("New Name: ");
                                productViewModel.Name = Console.ReadLine();
                            }

                            Console.Write("Do you want to change the price for this product? [Y/y or N/n]  => ");
                            updateProduct = Console.ReadLine().ToLower();
                            if (updateProduct == "y")
                            {
                                Console.Write("New Price: ");
                                productViewModel.Price = decimal.Parse(Console.ReadLine());
                            }

                            Console.Write("Do you want to change the quantity for this product? [Y/y or N/n]  => ");
                            updateProduct = Console.ReadLine().ToLower();
                            if (updateProduct == "y")
                            {
                                Console.Write("New Quantity: ");
                                productViewModel.Quantity = int.Parse(Console.ReadLine());
                            }

                            changeBrand = "y";

                            break;
                        case 2:

                            if (changeBrand == "y")
                            {
                                DBManager.Update(ModelType.BRAND, brandViewModel);

                                Console.WriteLine("Brand updated successfully!");
                            }

                            ProductViewManager pvm = new ProductViewManager();
                            pvm.DBManager.Delete(ModelType.PRODUCT, productViewModel.Barcode);

                            Console.WriteLine("Product deleted successfully!");

                            return;

                        default:
                            break;
                    }
                }

                if (changeBrand == "y")
                {
                    DBManager.Update(ModelType.BRAND, brandViewModel);

                    Console.WriteLine("Brand updated successfully!");
                }
                
            }

        }

        internal void DeleteBrand()
        {
            BVMList = new List<BrandViewModel>();

            DBManager.ReadAll(ModelType.BRAND, BVMList);

            Program.ShowBrandsInfoAdmin(BVMList);

            int maxBrandID = BVMList.Max(brand => brand.ID);
            int brandID;

            do
            {
                Console.WriteLine("Select brand ID!", BVMList.Count);
                brandID = int.Parse(Console.ReadLine());
            } while (brandID < 0 || brandID > maxBrandID);

            DBManager.Delete(ModelType.BRAND, brandID);

            Console.WriteLine("Brand deleted successfully!");
        }

    }
}
