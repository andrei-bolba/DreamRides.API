using DreamRides.Database.Enums;

namespace DreamRides.Data.RequestData
{
    public class CarRequest
    {
        public required string? Brand { get; init; }
        public required string? Model { get; init; }
        public required int? Year { get; init; }
        public required decimal? Price { get; init; }
        public required string? Color { get; init; }
        public required Transmision? Transmission { get; init; }
        public required int? Mileage { get; init; }
        public required FuelType? FuelType { get; init; }
        public required string? Description { get; init; }
        public required Chassis? Chassis { get; init; }
    }
}
