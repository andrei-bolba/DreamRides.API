using DreamRides.Database.Enums;

namespace DreamRides.Database.Model
{
    public class Car
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

        public ICollection<Favorite> Favorites { get; set; }
    }

}
