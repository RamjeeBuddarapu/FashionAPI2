using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface IProductService
    {
        List<Product> GetProducts(); //Done
        List<Product> GetProductsByPrice(double price);  //Done
        Product GetProductById(int productId); //Done
        void AddProduct(Product product); //Done
        void UpdateProduct(Product product); //Done
        void DeleteProduct(int productId); //Done
    }
}
