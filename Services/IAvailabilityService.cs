using FashionHexa.Entities;
namespace FashionHexa.Services
{
    public interface IAvailabilityService
    {
        List<Availability> GetAvailabilityList();  //Done
        Availability GetAvailabilityById(string availabilityId); //Done
        void UpdateAvailability(Availability availability);
        void AddAvailability(Availability availability);
        void RemoveAvailability(string availabilityId);
    }
}
