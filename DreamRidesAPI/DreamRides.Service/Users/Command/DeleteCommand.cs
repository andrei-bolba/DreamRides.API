using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Users.Command;

public class DeleteCommand: IRequest<Result<ResponseType<UserDTO>>>
{
    public Guid Id { get; set; }
}

public sealed class DeleteCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteCommand, Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var result = userRepository.Delete(request.Id);

        if (result.IsSuccess)
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = true,
                Message = result.Message
            });
        }
        
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
        {
            IsSuccess = false,
            Message = result.Message
        });
    }
}