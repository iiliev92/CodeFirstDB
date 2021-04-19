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
    internal class ProductViewManager
    {
        internal List<ProductViewModel> PVMList { get; set; }

        internal DBManager DBManager { get; set; }

        internal ProductViewManager()
        {
            PVMList = new List<ProductViewModel>();
            DBManager = new DBManager();
        }

        internal void CreateProduct()
        {
            //CreateSimpleCase();
            //CreateComplicatedCase();

            ProductViewManager pvm = new ProductViewManager();

            // To do ... Console.ReadLine()...

            //DBManager.Create(ModelType.PRODUCT, pvm);

            //Console.WriteLine("Product created successfully!");
        }

        internal void ReadProduct()
        {
            string barcode;

            do
            {
                Console.Write("Barcode: ");
                barcode = Console.ReadLine();
            } while (!ValidationManager.ValidateString(barcode));

            ProductViewModel pvm = new ProductViewModel();

            DBManager.Read(ModelType.PRODUCT, barcode, pvm);

            Program.ShowProductInfo(pvm);
        }

        internal void ReadAllProducts()
        {
            PVMList = new List<ProductViewModel>();

            DBManager.ReadAll(ModelType.PRODUCT, PVMList);

            Program.ShowProductsInfo(PVMList);
        }

        internal void UpdateProduct()
        {
            //UpdateComplicatedCase();

            
            ReadAllProducts();

            int choice;

            do
            {
                Console.WriteLine("Select product [1..{0}]", PVMList.Count);
                choice = int.Parse(Console.ReadLine());
            } while (choice < 0 || choice > PVMList.Count);

            ProductViewModel productViewModel = PVMList[choice - 1];

            
        }

        internal void DeleteProduct()
        {
            PVMList = new List<ProductViewModel>();

            DBManager.ReadAll(ModelType.PRODUCT, PVMList);

            Program.ShowProductsInfo(PVMList);

            string barcode = string.Empty;

            do
            {
                Console.Write("Enter barcode: ");
                barcode = Console.ReadLine();
            } while (!ValidationManager.ValidateString(barcode));

            DBManager.Delete(ModelType.PRODUCT, barcode);

            Console.WriteLine("Product deleted successfully!");
        }

        private void CreateSimpleCase()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Barcode = "12345678902";
            pvm.Name = "nVidia 2080";
            pvm.Price = 100;
            pvm.Quantity = 10;

            // Existing brand
            pvm.Brand = new BrandViewModel();
            pvm.Brand.ID = 4;

            DBManager.Create(ModelType.PRODUCT, pvm);

            Console.WriteLine("Product created successfully!");
        }

        private void CreateComplicatedCase()
        {
            ProductViewModel pvm = new ProductViewModel();
            pvm.Barcode = "12345678902";
            pvm.Name = "nVidia 2080";
            pvm.Price = 100;
            pvm.Quantity = 10;

            // New brand, not existing one!
            pvm.Brand = new BrandViewModel();
            pvm.Brand.Name = "Apple";

            DBManager.Create(ModelType.PRODUCT, pvm);

            Console.WriteLine("Product created successfully!");
        }

        private void UpdateComplicatedCase()
        {
            ReadAllProducts();

            ProductViewModel selectedProductViewModel = PVMList[0];

            selectedProductViewModel.Name = "nVidia 2080 Ti";

            selectedProductViewModel.Brand.ID = 5;

            selectedProductViewModel.Users.ToList()[0].ID = 2;

            DBManager.Update(ModelType.PRODUCT, selectedProductViewModel);

            Console.WriteLine("Product updated successfully!");
        }

    }
}
