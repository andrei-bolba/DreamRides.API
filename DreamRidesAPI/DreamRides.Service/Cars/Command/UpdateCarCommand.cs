using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Database.Model;
using DreamRides.Service.Utils;
using MediatR;

namespace DreamRides.Service.Cars.Command;

public class UpdateCarCommand : IRequest<Result<ResponseType<CarDTO>>>
{
    public required Guid CarId { get; set; }
    public required CarRequest CarRequest { get; set; }
}

public sealed class UpdateCarCommandCommandHandler(ICarRepository carRepository) : IRequestHandler<UpdateCarCommand, Result<ResponseType<CarDTO>>>
{
    public async Task<Result<ResponseType<CarDTO>>> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var requiredFields = ValidationHelperUtils.ValidateOneRequiredFields(request.CarRequest);
        if (!requiredFields.Key)
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                IsSuccess = false,
                Message = requiredFields.Value
            });
        }

        var car = carRepository.GetOne(request.CarId);
        if (car is not { IsSuccess: true, Object: not null })
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                IsSuccess = false,
                Message = car.Message
            });
        
        var editedCar = new Car
        {
            Brand = !string.IsNullOrWhiteSpace(request.CarRequest.Brand) ? request.CarRequest.Brand : car.Object.Brand,
            Model = !string.IsNullOrWhiteSpace(request.CarRequest.Model) ? request.CarRequest.Model : car.Object.Model,
            Year = request.CarRequest.Year ?? car.Object.Year,
            Price = request.CarRequest.Price?? car.Object.Price,
            Color = !string.IsNullOrWhiteSpace(request.CarRequest.Color) ? request.CarRequest.Color : car.Object.Color,
            Transmission = request.CarRequest.Transmission?? car.Object.Transmission,
            Mileage = request.CarRequest.Mileage?? car.Object.Year,
            FuelType = request.CarRequest.FuelType?? car.Object.FuelType,
            Description = !string.IsNullOrWhiteSpace(request.CarRequest.Description) ? request.CarRequest.Description : car.Object.Description,
            Chassis = request.CarRequest.Chassis?? car.Object.Chassis
        };
            
        var updateResult = carRepository.Update(editedCar);
        if (updateResult is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                Object = new CarDTO(updateResult.Object),
                IsSuccess = true,
                Message = updateResult.Message
            });
        }

        return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
        {
            IsSuccess = false,
            Message = updateResult.Message
        });

    }
}