using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Cars.Query;

public record GetAllCarsQuery : IRequest<Result<ResponseType<CarDTO>>>
{
}

public sealed class GetAllCarsQueryHandler(ICarRepository carRepository) : IRequestHandler<GetAllCarsQuery, Result<ResponseType<CarDTO>>>
{
    public async Task<Result<ResponseType<CarDTO>>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
    {
        var result = carRepository.GetAll();

        if (result is { IsSuccess: true, Collection: not null })
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>()
            {
                Collection = result.Collection.Select(c=> new CarDTO(c)),
                IsSuccess = true,
                Message = result.Message
            });
        }
        
        return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>()
        {
            IsSuccess = false,
            Message = result.Message
        });
    }
}