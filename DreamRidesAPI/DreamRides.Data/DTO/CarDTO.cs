using DreamRides.Database.Enums;
using DreamRides.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamRides.Data.DTO
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required int Year { get; set; }
        public required decimal Price { get; set; }
        public required string Color { get; set; }
        public required Transmision Transmission { get; set; }
        public required int Mileage { get; set; }
        public required FuelType FuelType { get; set; }
        public required string Description { get; set; }
        public required Chassis Chassis { get; set; }
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
            Mileage = car.Mileage;
            FuelType = car.FuelType;
            Description = car.Description;
            Chassis = car.Chassis;
            Favorite = car.Favorites.Where(f=>f.CarId == car.Id).Select(f=>f.User).ToList();
        }
    }
}
