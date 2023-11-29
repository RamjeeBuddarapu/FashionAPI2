using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface ISellerService
    {
        List<Seller> GetAllSeller();  //Done
        Seller  GetSellerById(string sellerId); //Done
        void AddSeller(Seller seller); //Done
        void UpdaterSeller(Seller seller); //Done
        void Deleteseller(string sellerId); //Done

    }
}
