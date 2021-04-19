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

namespace Controller
{
    
    public static class DBManager2
    {
        private readonly static BrandContext brandContext;
        private readonly static ProductContext productContext;
        private readonly static UserContext userContext;

        private readonly static Context context;

        static DBManager2()
        {
            context = new Context();
            brandContext = new BrandContext();
            productContext = new ProductContext(context);
            userContext = new UserContext(context);
        }

        public static void Create(ModelType modelType, params object[] args)
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
                    break;

                case ModelType.USER:
                    break;
                default:
                    break;
            }
        }

        public static void Read(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    int key = (int)args[0];

                    Brand brandFromDB = brandContext.Read(key);

                    BrandEventsContext brandEventsContext = (BrandEventsContext)args[1];

                    BrandEventsArgs brandEventsArgs = new BrandEventsArgs(brandFromDB.ID, brandFromDB.Name, brandFromDB.Products);

                    Delegate readDelegate = brandEventsContext.GetDelegate();
                    readDelegate.DynamicInvoke(brandEventsArgs);

                    break;

                case ModelType.PRODUCT:
                    break;

                case ModelType.USER:
                    break;

                default:
                    break;
            }
        }

        public static void ReadAll(ModelType modelType, params object[] args)
        {
            switch (modelType)
            {
                case ModelType.BRAND:

                    List<Brand> brandsFromDB = brandContext.ReadAll().ToList();

                    BrandEventsContext brandEventsContext = (BrandEventsContext)args[0];

                    List<BrandEventsArgs> brandEventsArgsList = new List<BrandEventsArgs>();

                    foreach (Brand brand in brandsFromDB)
                    {
                        BrandEventsArgs brandEventsArgs = new BrandEventsArgs(brand.ID, brand.Name, brand.Products);
                        brandEventsArgsList.Add(brandEventsArgs);
                    }

                    Delegate readDelegate = brandEventsContext.GetDelegate();
                    readDelegate.DynamicInvoke(brandEventsArgsList);


                    break;

                case ModelType.PRODUCT:
                    break;

                case ModelType.USER:
                    break;

                default:
                    break;
            }
        }


        public static bool ValidateString(string value)
        {
            return !(string.IsNullOrEmpty(value));
        }

    }
}
