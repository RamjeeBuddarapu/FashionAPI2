using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface IUserRatingsService
    {
        void AddRating(UserRatings userRatings);
        List<UserRatings> GetProductRatings(string productId);
        void UpdateRating(UserRatings userRatings);
        void DeleteRating(string UserRatingsId);
    }
}
