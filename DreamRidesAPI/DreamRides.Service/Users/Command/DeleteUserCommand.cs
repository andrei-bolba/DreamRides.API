using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Users.Command;

public class DeleteUserCommand: IRequest<Result<ResponseType<UserDTO>>>
{
    public required Guid Id { get; set; }
}

public sealed class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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