using DreamRides.Database.Enums;
using DreamRides.Database.Model;

namespace DreamRides.Data.DTO
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public Transmision Transmission { get; set; }
        public int MileageKilometers { get; set; }
        public int MilageMiles { get; set; }
        public FuelType FuelType { get; set; }
        public string Description { get; set; }
        public Chassis Chassis { get; set; }
        public List<User> Favorite { get; set; }

        public CarDTO(Car car)
        {
            Id = car.Id;
            Brand = car.Brand;
            Model = car.Model;
            Year = car.Year;
            Price = car.Price;
            Color = car.Color;
            Transmission = car.Transmission;
            MileageKilometers = car.Mileage;
            MilageMiles = (int)Math.Round(car.Mileage * 0.62137);
            FuelType = car.FuelType;
            Description = car.Description;
            Chassis = car.Chassis;
            Favorite = car.Favorites != null ? car.Favorites.Where(f=>f.CarId == car.Id).Select(f=>f.User).ToList(): [];
        }
    }
}
