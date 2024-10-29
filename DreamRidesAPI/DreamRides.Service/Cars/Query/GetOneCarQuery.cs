using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Cars.Query;

public record GetOneCarQuery : IRequest<Result<ResponseType<CarDTO>>>
{
    public required Guid CarId { get; set; }
}

public sealed class GetOneUserQueryHandler(ICarRepository carRepository) : IRequestHandler<GetOneCarQuery, Result<ResponseType<CarDTO>>>
{
    public async Task<Result<ResponseType<CarDTO>>> Handle(GetOneCarQuery request, CancellationToken cancellationToken)
    {
        var result = carRepository.GetOne(request.CarId);

        if (result is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>()
            {
                Object = new CarDTO(result.Object),
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