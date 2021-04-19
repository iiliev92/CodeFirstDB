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
    internal class UserViewManager
    {
        internal List<UserViewModel> UVMList { get; set; }

        internal DBManager DBManager { get; set; }

        internal UserViewManager()
        {
            UVMList = new List<UserViewModel>();
            DBManager = new DBManager();
        }

        internal void CreateUser()
        {
            //CreateSimpleCase();
            //CreateComplicatedCase();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            int? age = null;
            Console.Write("Do you want to enter age? [Y/y or N/n] => ");
            string choice = Console.ReadLine().ToLower();
            if (choice == "y")
            {
                Console.Write("Age: ");
                age = int.Parse(Console.ReadLine());
            }

            ProductViewManager pvm = new ProductViewManager();
            pvm.ReadAllProducts();

            Program.ShowProductsInfo(pvm.PVMList, true);

            Console.WriteLine("Enter choice [1..{0}] to add products to user in a format: 1, 2, 3, ...", pvm.PVMList.Count);
            string[] productsChoice = Console.ReadLine().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int[] productChoices = new int[productsChoice.Length];

            for (int i = 0; i < productsChoice.Length; i++)
            {
                productChoices[i] = int.Parse(productsChoice[i]) - 1;
            }

            List<ProductViewModel> productViewModels = new List<ProductViewModel>(productsChoice.Length);

            for (int i = 0; i < productChoices.Length; i++)
            {
                productViewModels.Add(pvm.PVMList[productChoices[i]]);
            }

            UserViewModel userViewModel = new UserViewModel();

            userViewModel.Name = name;
            userViewModel.Age = age;
            userViewModel.Products = productViewModels;

            DBManager.Create(ModelType.USER, userViewModel);

            Console.WriteLine("User created successfully!");
        }

        internal void ReadUser()
        {
            int key;
            do
            {
                Console.Write("Enter User ID: ");
                key = int.Parse(Console.ReadLine());
            } while (!ValidationManager.ValidatePositiveNumber(key));

            UserViewModel uvm = new UserViewModel();

            DBManager.Read(ModelType.USER, key, uvm);

            Program.ShowUserInfo(uvm);

        }

        internal void ReadAllUsers()
        {
            DBManager.ReadAll(ModelType.USER, UVMList);

            Program.ShowUsersInfo(UVMList);
        }


        private void CreateSimpleCase()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.Name = "John Doe";

            uvm.Products = new List<ProductViewModel>();

            // We need to add validation for emtpy collection! See Create()!

            DBManager.Create(ModelType.USER, uvm);

            Console.WriteLine("User created successfully!");
        }

        private void CreateComplicatedCase()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.Name = "John Snow";

            uvm.Products = new List<ProductViewModel>();

            ProductViewModel existingPVM = new ProductViewModel();
            existingPVM.Barcode = "1234567890";

            ProductViewModel newPVM = new ProductViewModel();
            newPVM.Barcode = "234567890";
            newPVM.Name = "Unknown Object";

            newPVM.Brand = new BrandViewModel();
            newPVM.Brand.Name = "New Brand";

            newPVM.Price = 1000;
            newPVM.Quantity = 100;

            uvm.Products.Add(existingPVM);
            uvm.Products.Add(newPVM);

            DBManager.Create(ModelType.USER, uvm);

            Console.WriteLine("User created successfully!");
        }

    }

}
