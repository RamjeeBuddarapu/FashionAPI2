using FashionHexa.Entities;
namespace FashionHexa.Services

{
    public interface IBrandService
    {
        List<Brand> GetAllBrands(); //Done
        Brand BrandById(string brandId); //Done
        void AddBrand(Brand brand); //Done
        void UpdateBrand(Brand brand); //Done
        void DeleteBrand(string brandId); //Done
    }

}
