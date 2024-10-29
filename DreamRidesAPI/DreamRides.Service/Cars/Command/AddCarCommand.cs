using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Database.Model;
using DreamRides.Service.Utils;
using MediatR;

namespace DreamRides.Service.Cars.Command;

public record AddCarCommand: IRequest<Result<ResponseType<CarDTO>>>
{
    public required CarRequest CarRequest { get; set; }
}

public sealed class AddCarCommandHandler(ICarRepository carRepository) : IRequestHandler<AddCarCommand, Result<ResponseType<CarDTO>>>
{
    public async Task<Result<ResponseType<CarDTO>>> Handle(AddCarCommand request, CancellationToken cancellationToken)
    {
        var requiredFields = ValidationHelperUtils.ValidateRequiredFields(request.CarRequest);
        if (!requiredFields.Key)
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                IsSuccess = false,
                Message = requiredFields.Value
            });
        }

        var newCar = new Car
        {
            Brand = request.CarRequest.Brand!,
            Model = request.CarRequest.Model!,
            Year = request.CarRequest.Year!.Value,
            Price = request.CarRequest.Price!.Value,
            Color = request.CarRequest.Color!,
            Transmission = request.CarRequest.Transmission!.Value,
            Mileage = request.CarRequest.Mileage!.Value,
            FuelType = request.CarRequest.FuelType!.Value,
            Description = request.CarRequest.Description!,
            Chassis = request.CarRequest.Chassis!.Value
        };

        var addResult = carRepository.Add(newCar);
        if (addResult is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                Object = new CarDTO(addResult.Object),
                IsSuccess = true,
                Message = addResult.Message
            }); 
        }
        
        return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
        {
            IsSuccess = false,
            Message = addResult.Message
        }); 
    }
}