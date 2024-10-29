using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Cars.Command;

public class DeleteCarCommand: IRequest<Result<ResponseType<CarDTO>>>
{
    public required Guid Id { get; set; }
}

public sealed class DeleteCarCommandHandler(ICarRepository carRepository) : IRequestHandler<DeleteCarCommand, Result<ResponseType<CarDTO>>>
{
    public async Task<Result<ResponseType<CarDTO>>> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var result = carRepository.Delete(request.Id);

        if (result.IsSuccess)
        {
            return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
            {
                IsSuccess = true,
                Message = result.Message
            });
        }
        
        return Result<ResponseType<CarDTO>>.Success(new ResponseType<CarDTO>
        {
            IsSuccess = false,
            Message = result.Message
        });
    }
}