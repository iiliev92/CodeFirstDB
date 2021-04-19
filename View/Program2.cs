using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Controller.Enums;
using Controller.Events;

namespace View
{
    class Program2
    {
        static BrandEventsContext brandEventsContext;

        //Change the name to "Main" if you want to use this version:
        static void Main2(string[] args)
        {
            try
            {
                brandEventsContext = new BrandEventsContext();

                //CreateBrand();
                ReadBrand();
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

        private static void CreateBrand()
        {
            string name;

            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
            } while (!DBManager2.ValidateString(name));

            DBManager2.Create(ModelType.BRAND, name);

            Console.WriteLine("Brand created successfully!");
        }

        private static void ReadBrand()
        {
            int key;

            do
            {
                Console.Write("ID: ");
                key = int.Parse(Console.ReadLine());
            } while (key <= 0);

            brandEventsContext.OnBrandReadCompleted += BrandEventsContext_OnBrandReadCompleted;

            DBManager2.Read(ModelType.BRAND, key, brandEventsContext);
        }

        private static void ReadAll()
        {
            brandEventsContext.OnBrandReadAllCompleted += BrandEventsContext_OnBrandReadAllCompleted;

            DBManager2.ReadAll(ModelType.BRAND, brandEventsContext);
        }

        private static void BrandEventsContext_OnBrandReadAllCompleted(List<BrandEventsArgs> brandEventsArgs)
        {
            foreach (BrandEventsArgs item in brandEventsArgs)
            {
                Console.Write("Name: {0}", item.Name);

            }
        }

        private static void BrandEventsContext_OnBrandReadCompleted(BrandEventsArgs brandEventsArgs)
        {
            Console.WriteLine("Name: {0}", brandEventsArgs.Name);
        }
    }
}
