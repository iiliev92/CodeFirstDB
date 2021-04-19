using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class BrandViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }

        public static BrandViewModel GetBrandViewModel(Brand brand, BrandViewModel bvm = null)
        {
            BrandViewModel brandViewModel;

            if (bvm == null)
            {
                brandViewModel = new BrandViewModel();
            }
            else
            {
                brandViewModel = bvm;
            }

            brandViewModel.ID = brand.ID;
            brandViewModel.Name = brand.Name;

            return brandViewModel;
        }

        public static List<BrandViewModel> GetBrandsViewModel(List<Brand> brands, List<BrandViewModel> bvmList = null)
        {
            List<BrandViewModel> brandViewModels;

            if (bvmList == null)
            {
                brandViewModels = new List<BrandViewModel>(brands.Count);
            }
            else
            {
                brandViewModels = bvmList;
            }

            for (int i = 0; i < brands.Count; i++)
            {
                BrandViewModel brandViewModel = GetBrandViewModel(brands[i]);
                brandViewModels.Add(brandViewModel);
            }

            return brandViewModels;
        }

        public static Brand GetBrand(BrandViewModel bvm)
        {
            Brand brand = new Brand();

            brand.ID = bvm.ID;
            brand.Name = bvm.Name;

            return brand;
        }

        public static List<Brand> GetBrands(List<BrandViewModel> bvmList)
        {
            List<Brand> brands = new List<Brand>();

            foreach (BrandViewModel brandViewModel in bvmList)
            {
                Brand brand = GetBrand(brandViewModel);
                brands.Add(brand);
            }

            return brands;
        }

    }
}
