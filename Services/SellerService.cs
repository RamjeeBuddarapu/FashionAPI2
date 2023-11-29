using FashionHexa.Entities;
using FashionHexa.Database;

namespace FashionHexa.Services
{
    public class SellerService : ISellerService
    {
        private readonly MyContext context = null;
        public SellerService(MyContext Context)
        {
            context = Context;
        }
        public void AddSeller(Seller seller)
        {
            try
            {
                context.Sellers.Add(seller);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Deleteseller(string sellerId)
        {
            try
            {
                
                Seller seller = context.Sellers.SingleOrDefault(p => p.SellerId == sellerId); 
                context.Sellers.Remove(seller);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Seller> GetAllSeller()
        {
            try
            {
                return context.Sellers.ToList(); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Seller GetSellerById(string sellerId)
        {
            try
            {
                
                Seller seller = context.Sellers.SingleOrDefault(p => p.SellerId == sellerId);
                return seller;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdaterSeller(Seller seller)
        {
            try
            {
                context.Sellers.Update(seller);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
